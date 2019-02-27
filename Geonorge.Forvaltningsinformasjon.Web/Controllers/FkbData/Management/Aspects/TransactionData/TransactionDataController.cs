using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Aspects.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.TransactionData;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.TransactionData.Aspects
{
    [Route("fkb-data/management/transaction-data/transaction-data")]
    public class TransactionDataController : Controller
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

        public IActionResult Index()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.TransactionData;

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.Get(),
                AdministrativeUnitName = "Norge"
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Country.cshtml", model);
        }

        [HttpGet("by-county")]
        public IActionResult GetByCounty([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.TransactionData;

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.GetByCounty(id),
                AdministrativeUnitName = _countyService.Get(id).Name
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/County.cshtml", model);
        }

        [HttpGet("by-municipality")]
        public IActionResult GetByMunicipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.TransactionData;

            TransactionDataViewModel model = new TransactionDataViewModel
            {
                TransactionData = _transactionDataService.GetByMunicipality(id),
                AdministrativeUnitName = _municipalityService.Get(id).Name
            };
            return View("Views/FkbData/Management/Aspects/TransactionData/Municipality.cshtml", model);
        }
    }
}