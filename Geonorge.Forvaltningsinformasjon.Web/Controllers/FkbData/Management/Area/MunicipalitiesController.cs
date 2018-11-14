using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;
using Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.Area
{
    [Route("fkb-data/management/area/municipalities")]
    public class MunicipalitiesController : Controller
    {
        private IMunicipalityService _service;

        public MunicipalitiesController(IMunicipalityService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult Index([FromQuery]string countyNumber)
        {
            MunicipalitiesViewModel model = new MunicipalitiesViewModel()
            {
                Municipalities = _service.GetAllByCounty(countyNumber)
            };
            model.DirectUpdateCount = model.Municipalities.Where(m => m.IntroductionState == IntroductionState.Introduced).Count();

            return View("Views/FkbData/Management/Area/Municipalities.cshtml", model);
        }
    }
}