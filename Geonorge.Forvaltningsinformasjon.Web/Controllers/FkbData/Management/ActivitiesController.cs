using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management
{
    [Route("fkb-data/management/activities")]
    public class ActivitiesController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;

        public ActivitiesController(IContextViewModelHelper contextViewModelHelper)
        {
            _contextViewModelHelper = contextViewModelHelper;
        }

        [HttpGet("")]
        public IActionResult Country([FromQuery] int id, [FromQuery]bool isCounty)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, isCounty));
            return View("Views/FkbData/Management/Aspects/ActivityOverview/Activities.cshtml");
        }
    }
}