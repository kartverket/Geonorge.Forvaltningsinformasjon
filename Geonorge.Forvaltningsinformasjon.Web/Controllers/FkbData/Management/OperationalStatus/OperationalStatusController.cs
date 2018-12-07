using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Geonorge.Forvaltningsinformasjon.Models;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.OperationalStatus
{
    [Route("fkb-data/management/operational-status/operationalstatus")]
    public class OperationalStatusController : Controller
    {
        private string _operationalStatus;
        private IContextViewModelHelper _contextViewModelHelper;

        public OperationalStatusController(IContextViewModelHelper contextViewModelHelper, ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _operationalStatus = applicationSettings.UrlOperationalStatus;
        }


        [HttpGet("")]
        public IActionResult Index([FromQuery] int id, [FromQuery]bool isCounty)
        {
            WebClient wclient = new WebClient();
            string RSSData = wclient.DownloadString(_operationalStatus);

            XDocument xml = XDocument.Parse(RSSData);

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, isCounty));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.OperationalStatus;
            return View("Views/FkbData/Management/OperationalStatus/OperationalStatus.cshtml");
        }
    }
}