using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management.Area
{
    [Route("fkb-data/management/area/municipalities")]
    public class MunicipalitiesController : Controller
    {
        private IMunicipalityService _service;
        private ICountyService _countyService;
        private IContextViewModelHelper _contextViewModelHelper;

        public MunicipalitiesController(IMunicipalityService service, ICountyService countyService, IContextViewModelHelper contextViewModelHelper)
        {
            _service = service;
            _countyService = countyService;
            _contextViewModelHelper = contextViewModelHelper;
        }

        [HttpGet("by-county")]
        public IActionResult Index([FromQuery]string id)
        {
            MunicipalitiesViewModel model = new MunicipalitiesViewModel()
            {
                Municipalities = _service.GetByCounty(int.Parse(id)),
                CountyName = _countyService.Get(int.Parse(id)).Name,
            };
            model.DirectUpdateCount = model.Municipalities.Where(m => m.IntroductionState == IntroductionState.Introduced).Count();

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.Management;

            return View("Views/FkbData/Management/Area/Municipalities.cshtml", model);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            //IMunicipality _service.Get(id);
            throw new System.Exception();
        }
    }
}