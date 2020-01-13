using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
   [Route("fkb-data/data-content/data-quality-classification")]
    public class DataQualityClassificationController : Controller
    {
        private const string _serviceType = "OGC:WMS";
        private const string _url = "https://wms.geonorge.no/skwms1/wms.georef2?request=GetCapabilities&service=WMS";
        private const string _layer = "Georef-ABCD";

        private IContextViewModelHelper _contextViewModelHelper;
        private IDataQualityClassificationService _dataQualityClassificationService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;

        public DataQualityClassificationController(
            IContextViewModelHelper contextViewModelHelper,
            IDataQualityClassificationService dataQualityClassificationService,
            ICountyService countyService,
            IMunicipalityService municipalityService)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _dataQualityClassificationService = dataQualityClassificationService;
            _countyService = countyService;
            _municipalityService = municipalityService;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            MapViewModel mapViewModel = new MapViewModel();

            mapViewModel.AddService(_serviceType, _url, _layer);

            DataQualityClassificationModel model = new DataQualityClassificationModel
            {
                Classifications = _dataQualityClassificationService.Get(),
                MapViewModel = mapViewModel
            };

            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ICounty county = _countyService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(county);

            mapViewModel.AddService(_serviceType, _url, _layer);

            DataQualityClassificationModel model = new DataQualityClassificationModel
            {
                Classifications = _dataQualityClassificationService.GetByCounty(id),
                AdministrativeUnitName = county.Name,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification/County.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            IMunicipality municipality = _municipalityService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(municipality);

            mapViewModel.AddService(_serviceType, _url, _layer);

            DataQualityClassificationModel model = new DataQualityClassificationModel
            {
                Classifications = _dataQualityClassificationService.GetByMunicipality(id),
                AdministrativeUnitName = municipality.Name,
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification/Municipality.cshtml", model);
        }
    }
}