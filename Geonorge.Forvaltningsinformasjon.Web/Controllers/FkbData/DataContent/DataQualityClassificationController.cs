using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
   [Route("fkb-data/data-content/data-quality-classification")]
    public class DataQualityClassificationController : Controller
    {
        private const string _serviceType = "OGC:WMS";
        private const string _layer = "Georef-ABCD";
        private string _url;
        private string _legendUrl;
        private string _urlAdminUnits;
        private List<string> _layersAdminUnits = new List<string> { "fylker_gjel", "kommuner_gjel" };

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
            _url = applicationSettings.ExternalUrls.Georef;
            _legendUrl = applicationSettings.ExternalUrls.GeorefLegend;
            _urlAdminUnits = applicationSettings.ExternalUrls.AdministrativeUnits;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            MapViewModel mapViewModel = new MapViewModel();

            mapViewModel.AddService(_serviceType, _url, _layer);
            mapViewModel.AddService(_serviceType, _urlAdminUnits, _layersAdminUnits);
            mapViewModel.LegendUrl = _legendUrl;

            DataQualityClassificationViewModel model = new DataQualityClassificationViewModel
            {
                Classifications = _dataQualityClassificationService.Get(),
                Type = AdministrativeUnitType.Country,
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

            mapViewModel.AddService(_serviceType, _url, _layer);
            mapViewModel.AddService(_serviceType, _urlAdminUnits, _layersAdminUnits);
            mapViewModel.LegendUrl = _legendUrl;

            DataQualityClassificationViewModel model = new DataQualityClassificationViewModel
            {
                Classifications = _dataQualityClassificationService.GetByCounty(id),
                AdministrativeUnitName = county.Name,
                Type = AdministrativeUnitType.County,
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

            mapViewModel.AddService(_serviceType, _url, _layer);
            mapViewModel.AddService(_serviceType, _urlAdminUnits, _layersAdminUnits);
            mapViewModel.LegendUrl = _legendUrl;

            DataQualityClassificationViewModel model = new DataQualityClassificationViewModel
            {
                Classifications = _dataQualityClassificationService.GetByMunicipality(id),
                AdministrativeUnitName = municipality.Name,
                Type = AdministrativeUnitType.Municipality,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification.cshtml", model);
        }
    }
}