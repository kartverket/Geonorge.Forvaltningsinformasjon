using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.Area
{
    [Route("fkb-data/management/area/municipalities")]
    public class MunicipalitiesController : Controller
    {
        private IMunicipalityService _service;
        private ICountyService _CountyService;

        public MunicipalitiesController(IMunicipalityService service, ICountyService countyService)
        {
            _service = service;
            _CountyService = countyService;
        }

        [HttpGet("by-county")]
        public IActionResult Index([FromQuery]string id)
        {
            MunicipalitiesViewModel model = new MunicipalitiesViewModel()
            {
                Municipalities = _service.GetByCounty(id),
                CountyName = _CountyService.Get(id).Name
            };
            model.DirectUpdateCount = model.Municipalities.Where(m => m.IntroductionState == IntroductionState.Introduced).Count();

            return View("Views/FkbData/Management/Area/Municipalities.cshtml", model);
        }
    }
}