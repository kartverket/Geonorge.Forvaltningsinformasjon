using System.ComponentModel;
using System.IO;
using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.DirectUpdateInfo;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management
{
    [Route("fkb-data/management/direct-update-info")]
    public class DirectUpdateInfoController : Controller, IAdministrativeUnitController
    {
        private const string _serviceType = "GEOJSON";
        private const string _layer = "sFkbStatus";

        private IMunicipalityService _municipalityService;
        private ICountyService _countyService;
        private IDataSetService _dataSetService;
        private IContextViewModelHelper _contextViewModelHelper;
        private IDirectUpdateInfoGeoJsonService _geoJsonService;
        private ApplicationSettings _applicationSettings;
        
        public DirectUpdateInfoController(
            IMunicipalityService municipalityService,
            ICountyService countyService,
            IDataSetService dataSetService,
            IContextViewModelHelper contextViewModelHelper,
            IDirectUpdateInfoGeoJsonService geoJsonService,
            ApplicationSettings applicationSettings)
        {
            _municipalityService = municipalityService;
            _countyService = countyService;
            _dataSetService = dataSetService;
            _contextViewModelHelper = contextViewModelHelper;
            _geoJsonService = geoJsonService;
            _applicationSettings = applicationSettings;
        }

        [HttpGet("")]
        public IActionResult Country()
        {
            string url = $"{_applicationSettings.UrlThematicGeoJson}/{_geoJsonService.GetFileName()}";
            MapViewModel mapViewModel = new MapViewModel();

            mapViewModel.AddService(_serviceType, url, _layer);

            CountiesViewModel model = new CountiesViewModel()
            {
                Counties = _countyService.Get(),
                MapViewModel = mapViewModel
            };
            model.DirectUpdateCount = model.Counties.Sum(c => c.DirectUpdateCount);

            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            
            return View("Views/FkbData/Management/Aspects/DirectUpdateInfo/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            string url = $"{_applicationSettings.UrlThematicGeoJson}/{_geoJsonService.GetFileNameCounty(id)}";
            ICounty county = _countyService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(county);

            mapViewModel.AddService(_serviceType, url, _layer);

            MunicipalitiesViewModel model = new MunicipalitiesViewModel()
            {
                Municipalities = _municipalityService.GetByCounty(id),
                CountyName = county.Name,
                MapViewModel = mapViewModel
            };
            model.DirectUpdateCount = model.Municipalities.Where(m => m.IntroductionState == IntroductionState.Introduced).Count();

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            return View("Views/FkbData/Management/Aspects/DirectUpdateInfo/County.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            IMunicipality municipality = _municipalityService.Get(id);

            MunicipalityViewModel model = new MunicipalityViewModel()
            {
                DataSets = _dataSetService.GetByMunicipality(id),
                Name = municipality.Name
            };

            switch (municipality.IntroductionState)
            {
                case IntroductionState.NotIntroduced:
                    model.Caption = "Ikke planlagt innføring av direkteoppdatering i Sentral FKB";
                    break;
                case IntroductionState.Planned:
                    model.Caption = "Direkteoppdatering i Sentral FKB planlagt innført {0}";
                    model.DateTime = municipality.PlannedIntroductionDate;
                    break;
                case IntroductionState.Introduced:
                    model.Caption = "Direkteoppdatering i Sentral FKB innført {0}";
                    model.DateTime = municipality.IntroductionDate;
                    break;
                case IntroductionState.Geosynch:
                    model.Caption = "Geosynkronisering innført";
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));

            return View("Views/FkbData/Management/Aspects/DirectUpdateInfo/Municipality.cshtml", model);
        }

        [HttpGet("geojson/{geoJsonFileName}")]
        public IActionResult GetGeoJson(string geoJsonFileName)
        {
            string geoJson;

            using (StreamReader reader = new StreamReader($"{_applicationSettings.LocalPathThematicGeoJson}\\{geoJsonFileName}"))
            {
                geoJson = reader.ReadToEnd();
            }
            return Content(geoJson);
        }
    }
}