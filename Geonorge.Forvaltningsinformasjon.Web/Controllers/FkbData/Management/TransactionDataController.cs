using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.TransactionData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management
{
    [Route("fkb-data/management/transaction-data")]
    public class TransactionDataController : Controller, IAdministrativeUnitController
    {
        private const string _serviceType = "OGC:WMS";
        private const string _layer = "bygning";
        private string _url;
        private string _styleUrl;
        private string _urlAdminUnits;
        private List<string> _layersAdminUnits = new List<string> { "fylker_gjel", "kommuner_gjel" };

        private IContextViewModelHelper _contextViewModelHelper;
        private ITransactionDataService _transactionDataService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;
        private Dictionary<string, string> _dataSetToLayerMap;

        public TransactionDataController(
            IContextViewModelHelper contextViewModelHelper, 
            ITransactionDataService transactionDataService,
            ICountyService countyService,
            IMunicipalityService municipalityService,
            ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _transactionDataService = transactionDataService;
            _countyService = countyService;
            _municipalityService = municipalityService;
            _dataSetToLayerMap = applicationSettings.DataSetToLayerMap;
            _url = applicationSettings.ExternalUrls.TransactionData;
            _styleUrl = applicationSettings.ExternalUrls.TransactionDataStyle;
            _urlAdminUnits = applicationSettings.ExternalUrls.AdministrativeUnits;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            MapViewModel mapViewModel = new MapViewModel();
            List<ITransactionData> transactionData = _transactionDataService.Get();

            mapViewModel.AddService(_serviceType, _url, _layer);

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = transactionData,
                AdministrativeUnitName = "Norge",
                LayerStyles = _transactionDataService.GetLayerStyles(transactionData),
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ICounty county = _countyService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(county);
            List<ITransactionData> transactionData = _transactionDataService.GetByCounty(id);

            mapViewModel.AddService(_serviceType, _url, _layer);

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = transactionData,
                AdministrativeUnitName = county.Name,
                LayerStyles = _transactionDataService.GetLayerStyles(transactionData),
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/County.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            IMunicipality municipality = _municipalityService.Get(id);
            MapViewModel mapViewModel = new MapViewModel(municipality);
            List<ITransactionData> transactionData = _transactionDataService.GetByMunicipality(id);

            mapViewModel.AddService(_serviceType, _url, _layer);

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = transactionData,
                AdministrativeUnitName = municipality.Name,
                LayerStyles = _transactionDataService.GetLayerStyles(transactionData),
                MapViewModel = mapViewModel
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Municipality.cshtml", model);
        }

        [HttpGet("update-map")]
        public IActionResult UpdateMap(
            [FromQuery]string dataSetNames,
            [FromQuery]Period period,
            [FromQuery]string jsonMapViewModel)
        {
            MapViewModel mapViewModel = JsonConvert.DeserializeObject<MapViewModel>(jsonMapViewModel);
            DateTime to = DateTime.UtcNow;
            DateTime from;

            switch (period)
            {
                case Period.Week:
                    from = to.AddDays(-7).Date;
                    break;
                case Period.Month:
                    from = to.AddMonths(-1).Date;
                    break;
                case Period.Year:
                default:
                    from = to.AddYears(-1).Date;
                    break;
            }

            string time = $"{from.ToString("yyyy-MM-dd")}/{to.ToString("yyyy-MM-dd")}";

            Dictionary<string, string> customParameters = new Dictionary<string, string>
            {
                { "TIME", time }
            };

            mapViewModel.Services.Clear();

            if (dataSetNames != null)
            {
                foreach (string name in dataSetNames.Split(','))
                {
                    mapViewModel.AddService(_serviceType, _url, _dataSetToLayerMap[name], customParameters);
                }
            }

            mapViewModel.AddService(_serviceType, _urlAdminUnits, _layersAdminUnits);

            return PartialView("Views/Common/Map.cshtml", mapViewModel);
        }
    }
}