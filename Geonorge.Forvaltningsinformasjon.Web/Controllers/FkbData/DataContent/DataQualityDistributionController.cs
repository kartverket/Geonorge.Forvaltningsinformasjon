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
using Newtonsoft.Json;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
    [Route("fkb-data/data-content/data-quality-distribution")]
    public class DataQualityDistributionController : Controller
    {
        private const string _ServiceType = "OGC:WMS";
        private const string _CountyAdminUnitLayer = "fylker_gjel";
        private const string _MunicipalityAdminUnitLayer = "kommuner_gjel";

        private string _urlAdminUnits;

        private IContextViewModelHelper _contextViewModelHelper;
        private IDataQualityDistributionService _dataQualityDistributionService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;
        private ApplicationSettings _applicationSettings;

        public DataQualityDistributionController(
            IContextViewModelHelper contextViewModelHelper,
            IDataQualityDistributionService dataQualityDistributionService,
            ICountyService countyService,
            IMunicipalityService municipalityService,
            ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _dataQualityDistributionService = dataQualityDistributionService;
            _countyService = countyService;
            _municipalityService = municipalityService;
            _applicationSettings = applicationSettings;
            _urlAdminUnits = _dataQualityDistributionService.GetAdminstrativeUnitsWmsUrl();
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            MapViewModel mapViewModel = new MapViewModel();

            DataQualityDistributionViewModel model = new DataQualityDistributionViewModel(
                _dataQualityDistributionService.Get(), 
                _applicationSettings.QualityCategoryColors,
                _applicationSettings.DataQualityDataSetToLayerMap)
            {
                Type = AdministrativeUnitType.Country,
                AdministrativeUnitName = "Norge",
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataQualityDistribution,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ICounty county = _countyService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(county);

            DataQualityDistributionViewModel model = new DataQualityDistributionViewModel(
                _dataQualityDistributionService.GetByCounty(id), 
                _applicationSettings.QualityCategoryColors,
                _applicationSettings.DataQualityDataSetToLayerMap)
            {
                AdministrativeUnitName = county.Name,
                Type = AdministrativeUnitType.County,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataQualityDistribution,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            IMunicipality municipality = _municipalityService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(municipality);

            DataQualityDistributionViewModel model = new DataQualityDistributionViewModel(
                _dataQualityDistributionService.GetByMunicipality(id), 
                _applicationSettings.QualityCategoryColors,
                _applicationSettings.DataQualityDataSetToLayerMap)
            {
                AdministrativeUnitName = municipality.Name,
                Type = AdministrativeUnitType.Municipality,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataQualityDistribution,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution.cshtml", model);
        }

        [HttpGet("update-map")]
        public IActionResult UpdateMap(
            [FromQuery] string layerName,
            [FromQuery] string jsonMapViewModel)
        {
            MapViewModel mapViewModel = JsonConvert.DeserializeObject<MapViewModel>(jsonMapViewModel);

            mapViewModel.Services.Clear();
            mapViewModel.AddService(_ServiceType, _dataQualityDistributionService.GetWmsUrl(), layerName);

            AddAdminUnitsToServices(mapViewModel);

            return PartialView("Views/Common/Map.cshtml", mapViewModel);
        }

        private void AddAdminUnitsToServices(MapViewModel mapViewModel)
        {
            string sld = _dataQualityDistributionService.GetAdministrativeUnitSld();
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