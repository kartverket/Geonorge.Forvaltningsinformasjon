using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Geonorge.Forvaltningsinformasjon.Models;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.OperationalStatus;
using Microsoft.AspNetCore.Mvc;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Aspects.Helpers;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.OperationalStatus
{
    [Route("fkb-data/management/operational-status/operationalstatus")]
    public class OperationalStatusController : Controller
    {
        private readonly string  _operationalStatus;
        private IContextViewModelHelper _contextViewModelHelper;

        public OperationalStatusController(IContextViewModelHelper contextViewModelHelper, ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _operationalStatus = applicationSettings.UrlOperationalStatus;
        }


        [HttpGet("")]
        public IActionResult Index([FromQuery] int id, [FromQuery]bool isCounty)
        {
            XmlTextReader reader = new XmlTextReader(_operationalStatus);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            List<AtomFeedEntryViewModel> entries = new List<AtomFeedEntryViewModel>();

            foreach (SyndicationItem item in feed.Items ?? Enumerable.Empty<SyndicationItem>())
            {
                entries.Add(new AtomFeedEntryViewModel()
                {
                    Title = item.Title.Text,
                    Content = ((TextSyndicationContent)item.Content).Text,
                    Published = item.PublishDate.DateTime,
                    Updated = item.LastUpdatedTime.DateTime
                });
            }

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, isCounty));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.OperationalStatus;
            return View("Views/FkbData/Management/OperationalStatus/OperationalStatus.cshtml", entries);
        }
    }
}