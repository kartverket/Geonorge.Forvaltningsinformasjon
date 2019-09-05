using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
    [Route("fkb-data/data-content/data-quality-classification")]
    public class DataQualityClassificationController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;
        private IDataQualityClassificationService _service;

        public DataQualityClassificationController(
            IContextViewModelHelper contextViewModelHelper,
            IDataQualityClassificationService service)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _service = service;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            DataQualityClassificationModel model = new DataQualityClassificationModel
            {
                Classifications = _service.Get()
            };

            return View("Views/FkbData/DataContent/Aspects/DataQualityClassification/Country.cshtml", model);
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