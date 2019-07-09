using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.TransactionData;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management
{
    [Route("fkb-data/management/transaction-data")]
    public class TransactionDataController : Controller, IAdministrativeUnitController
    {
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
                AdministrativeUnitName = "Norge"
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.GetByCounty(id),
                AdministrativeUnitName = _countyService.Get(id).Name
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/County.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.GetByMunicipality(id),
                AdministrativeUnitName = _municipalityService.Get(id).Name
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Municipality.cshtml", model);
        }
    }
}