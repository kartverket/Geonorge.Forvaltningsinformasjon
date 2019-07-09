using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
    [Route("fkb-data/data-content/data-quality-classification")]
    public class DataQualityClassificationController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;

        public DataQualityClassificationController(
            IContextViewModelHelper contextViewModelHelper)
        {
            _contextViewModelHelper = contextViewModelHelper;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();

            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification/Country.cshtml");
        }


        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification/County.cshtml");
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));

            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification/Municipality.cshtml");
        }
    }
}