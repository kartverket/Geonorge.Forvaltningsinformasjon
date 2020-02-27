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
        private const string _url = "http://wms.geonorge.no/skwms1/wms.sfkb-transaksjoner?request=GetCapabilities&service=WMS";
        private const string _styleUrl = "https://wms.geonorge.no/skwms1/wms.sfkb-transaksjoner?Service=wms&Request=GetStyles&Version=1.0.0&Layers=";
        private const string _layer = "bygning";
        private const string _urlAdminUnits = " http://wms.geonorge.no/skwms1/wms.adm_enheter2?request=GetCapabilities&service=WMS";
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
                LayerStyles = GetLayerStyles(transactionData),
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
                LayerStyles = GetLayerStyles(transactionData),
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
                LayerStyles = GetLayerStyles(transactionData),
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

        private Dictionary<string, LayerStyle> GetLayerStyles(List<ITransactionData> transactionData)
        {
            Dictionary<string, LayerStyle> layerStyles = new Dictionary<string, LayerStyle>();
            List<string> layerNames = new List<string>();

            transactionData.ForEach(td => layerNames.Add($"{_dataSetToLayerMap[td.DataSetName]}_N1"));

            XmlDocument xmlDocument = GetStyleXml(layerNames);
            XmlNodeList fillStyles = xmlDocument.GetElementsByTagName("Fill");
            XmlNodeList strokeStyles = xmlDocument.GetElementsByTagName("Stroke");

            for (int i = 0; i < transactionData.Count; ++i)
            {
                LayerStyle layerStyle = new LayerStyle();

                layerStyle.FillColor = fillStyles[i].SelectSingleNode("*[@name='fill']").InnerText;
                layerStyle.FillOpacity = fillStyles[i].SelectSingleNode("*[@name='fill-opacity']").InnerText;
                layerStyle.StrokeColor = strokeStyles[i].SelectSingleNode("*[@name='stroke']").InnerText;
                layerStyle.StrokeWidth = strokeStyles[i].SelectSingleNode("*[@name='stroke-width']").InnerText;

                layerStyles.Add(transactionData[i].DataSetName, layerStyle);
            }
            return layerStyles;
        }

        private XmlDocument GetStyleXml(List<string> layers)
        {
            XmlDocument xmlDocument = new XmlDocument();

            string styles;
            string url = _styleUrl + string.Join(',', layers);

            using (HttpClient client = new HttpClient())
            {
                styles = client.GetStringAsync(url).Result;
            }
            xmlDocument.LoadXml(styles);
            return xmlDocument;
        }
    }
}