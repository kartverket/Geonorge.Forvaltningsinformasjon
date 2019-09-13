using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
    [Route("fkb-data/data-content/data-quality-distribution")]
    public class DataQualityDistributionController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;

        public DataQualityDistributionController(
            IContextViewModelHelper contextViewModelHelper)
        {
            _contextViewModelHelper = contextViewModelHelper;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();

            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution/Country.cshtml");
        }


        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution/County.cshtml");
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));

            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution/Municipality.cshtml");
        }
    }
}