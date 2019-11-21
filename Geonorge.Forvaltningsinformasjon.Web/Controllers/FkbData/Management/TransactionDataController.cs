using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.TransactionData;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management
{
    [Route("fkb-data/management/transaction-data")]
    public class TransactionDataController : Controller, IAdministrativeUnitController
    {
        private const string _url = "http://wms.geonorge.no/skwms1/wms.sfkb-transaksjoner?request=GetCapabilities&service=WMS";
        private const string _layer = "bygning";

        private IContextViewModelHelper _contextViewModelHelper;
        private ITransactionDataService _transactionDataService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;

        public TransactionDataController(
            IContextViewModelHelper contextViewModelHelper, 
            ITransactionDataService transactionDataService,
            ICountyService countyService,
            IMunicipalityService municipalityService)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _transactionDataService = transactionDataService;
            _countyService = countyService;
            _municipalityService = municipalityService;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.Get(),
                AdministrativeUnitName = "Norge",
                MapViewModel = new MapViewModel
                {
                    Url = _url,
                    Layer = _layer
                }
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ICounty county = _countyService.Get(id);

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.GetByCounty(id),
                AdministrativeUnitName = county.Name,
                MapViewModel = new MapViewModel(county)
                {
                    Url = _url,
                    Layer = _layer
                }
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/County.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            IMunicipality municipality = _municipalityService.Get(id);

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.GetByMunicipality(id),
                AdministrativeUnitName = municipality.Name,
                MapViewModel = new MapViewModel(municipality)
                {
                    Url = _url,
                    Layer = _layer
                }
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Municipality.cshtml", model);
        }
    }
}