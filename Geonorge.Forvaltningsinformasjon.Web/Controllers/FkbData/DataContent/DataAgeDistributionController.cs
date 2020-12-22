using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
    [Route("fkb-data/data-content/data-age-distribution")]
    public class DataAgeDistributionController : Controller
    {
        private const string _ServiceType = "OGC:WMS";
        private const string _CountyAdminUnitLayer = "fylker_gjel";
        private const string _MunicipalityAdminUnitLayer = "kommuner_gjel";

        private string _urlAdminUnits;

        private IContextViewModelHelper _contextViewModelHelper;
        private IDataAgeDistributionService _dataAgeDistributionService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;
        private ApplicationSettings _applicationSettings;

        public DataAgeDistributionController(
            IContextViewModelHelper contextViewModelHelper,
            IDataAgeDistributionService dataAgeDistributionService,
            ICountyService countyService,
            IMunicipalityService municipalityService,
            ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _dataAgeDistributionService = dataAgeDistributionService;
            _countyService = countyService;
            _municipalityService = municipalityService;
            _applicationSettings = applicationSettings;
            _urlAdminUnits = _dataAgeDistributionService.GetAdminstrativeUnitsWmsUrl();
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            MapViewModel mapViewModel = new MapViewModel();

            AddAdminUnitsToServices(mapViewModel);

            DataAgeDistributionViewModel model = new DataAgeDistributionViewModel(_dataAgeDistributionService.Get(), _applicationSettings.AgeCategoryColors)
            {
                Type = AdministrativeUnitType.Country,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataAgeDistribution,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataAgeDistribution.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ICounty county = _countyService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(county);

            AddAdminUnitsToServices(mapViewModel);

            DataAgeDistributionViewModel model = new DataAgeDistributionViewModel(_dataAgeDistributionService.GetByCounty(id), _applicationSettings.AgeCategoryColors)
            {
                AdministrativeUnitName = county.Name,
                Type = AdministrativeUnitType.County,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataAgeDistribution,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataAgeDistribution.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            IMunicipality municipality = _municipalityService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(municipality);

            AddAdminUnitsToServices(mapViewModel);

            DataAgeDistributionViewModel model = new DataAgeDistributionViewModel(_dataAgeDistributionService.GetByMunicipality(id), _applicationSettings.AgeCategoryColors)
            {
                AdministrativeUnitName = municipality.Name,
                Type = AdministrativeUnitType.Municipality,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataAgeDistribution,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataAgeDistribution.cshtml", model);
        }

        private void AddAdminUnitsToServices(MapViewModel mapViewModel)
        {
            string sld = _dataAgeDistributionService.GetAdministrativeUnitSld();
            Dictionary<string, string> customParameters = new Dictionary<string, string>
            {
                {
                    "SLD_BODY", sld
                }
            };

            mapViewModel.AddService(_ServiceType, _urlAdminUnits, _CountyAdminUnitLayer, customParameters);
            mapViewModel.AddService(_ServiceType, _urlAdminUnits, _MunicipalityAdminUnitLayer, customParameters);
        }
    }
}