using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.ActivityOverview
{
    [Route("fkb-data/management/activity-overview/activities")]
    public class ActivitiesController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Views/FkbData/Management/ActivityOverview/Activities.cshtml");
        }
    }
}