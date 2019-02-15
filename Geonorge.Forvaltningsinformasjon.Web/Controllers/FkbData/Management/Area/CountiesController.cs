using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.Area.FkbData.Management.Area
{
    [Route("fkb-data/management/area/counties")]
    public class CountiesController : Controller
    {
        private ICountyService _service;
        private IContextViewModelHelper _contextViewModelHelper;

        public CountiesController(ICountyService service, IContextViewModelHelper contextViewModelHelper)
        {
            _service = service;
            _contextViewModelHelper = contextViewModelHelper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            CountiesViewModel model = new CountiesViewModel()
            {
                Counties = _service.Get(),
            };
            model.DirectUpdateCount = model.Counties.Sum(c => c.DirectUpdateCount);

            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.Management;

            return View("Views/FkbData/Management/Area/Counties.cshtml", model);
        }
    }
}