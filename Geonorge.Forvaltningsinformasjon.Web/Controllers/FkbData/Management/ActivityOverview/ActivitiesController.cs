using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.ActivityOverview
{
    [Route("fkb-data/management/activity-overview/activities")]
    public class ActivitiesController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;

        public ActivitiesController(IContextViewModelHelper contextViewModelHelper)
        {
            _contextViewModelHelper = contextViewModelHelper;
        }

        [HttpGet("")]
        public IActionResult Index([FromQuery] int id, [FromQuery]bool isCounty)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, isCounty));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.ActivityOverview;
            return View("Views/FkbData/Management/ActivityOverview/Activities.cshtml");
        }
    }
}