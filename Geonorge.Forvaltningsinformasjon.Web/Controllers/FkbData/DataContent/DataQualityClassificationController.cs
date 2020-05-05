using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
   [Route("fkb-data/data-content/data-quality-classification")]
    public class DataQualityClassificationController : Controller
    {
        private const string _ServiceType = "OGC:WMS";
        private const string _Layer = "Georef-ABCD";
        private const string _CountyAdminUnitLayer = "fylker_gjel";
        private const string _MunicipalityAdminUnitLayer = "kommuner_gjel";

        private string _url;
        private string _urlAdminUnits;

        private IContextViewModelHelper _contextViewModelHelper;
        private IDataQualityClassificationService _dataQualityClassificationService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;

        public DataQualityClassificationController(
            IContextViewModelHelper contextViewModelHelper,
            IDataQualityClassificationService dataQualityClassificationService,
            ICountyService countyService,
            IMunicipalityService municipalityService,
            ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _dataQualityClassificationService = dataQualityClassificationService;
            _countyService = countyService;
            _municipalityService = municipalityService;
            _url = _dataQualityClassificationService.GetWmsUrl();
            _urlAdminUnits = _dataQualityClassificationService.GetAdminstrativeUnitsWmsUrl();
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            MapViewModel mapViewModel = new MapViewModel();
            string sld = _dataQualityClassificationService.GetSld();

            Dictionary<string, string> customParameters = new Dictionary<string, string>
            {
                {
                    "SLD_BODY", sld
                }
            };

            mapViewModel.AddService(_ServiceType, _url, _Layer, customParameters);

            AddAdminUnitsToServices(mapViewModel);

            DataQualityClassificationViewModel model = new DataQualityClassificationViewModel
            {
                Classifications = _dataQualityClassificationService.Get(),
                Type = AdministrativeUnitType.Country,
                LegendItemStyles = _dataQualityClassificationService.GetLegendItemStyles(),
                MapViewModel = mapViewModel
            };

            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ICounty county = _countyService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(county);
            string sld = _dataQualityClassificationService.GetSld();

            Dictionary<string, string> customParameters = new Dictionary<string, string>
            {
                {
                    "SLD_BODY", sld
                }
            };

            mapViewModel.AddService(_ServiceType, _url, _Layer, customParameters);

            AddAdminUnitsToServices(mapViewModel);

            DataQualityClassificationViewModel model = new DataQualityClassificationViewModel
            {
                Classifications = _dataQualityClassificationService.GetByCounty(id),
                AdministrativeUnitName = county.Name,
                Type = AdministrativeUnitType.County,
                LegendItemStyles = _dataQualityClassificationService.GetLegendItemStyles(),
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            IMunicipality municipality = _municipalityService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(municipality);
            string sld = _dataQualityClassificationService.GetSld();

            Dictionary<string, string> customParameters = new Dictionary<string, string>
            {
                {
                    "SLD_BODY", sld
                }
            };

            mapViewModel.AddService(_ServiceType, _url, _Layer, customParameters);

            AddAdminUnitsToServices(mapViewModel);

            DataQualityClassificationViewModel model = new DataQualityClassificationViewModel
            {
                Classifications = _dataQualityClassificationService.GetByMunicipality(id),
                AdministrativeUnitName = municipality.Name,
                Type = AdministrativeUnitType.Municipality,
                LegendItemStyles = _dataQualityClassificationService.GetLegendItemStyles(),
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification.cshtml", model);
        }

        private void AddAdminUnitsToServices(MapViewModel mapViewModel)
        {
            string sld = _dataQualityClassificationService.GetAdministrativeUnitSld();
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