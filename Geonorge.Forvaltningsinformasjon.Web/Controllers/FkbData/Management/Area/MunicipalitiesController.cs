using System.ComponentModel;
using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
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
        public IActionResult Index([FromQuery]int id)
        {
            MunicipalitiesViewModel model = new MunicipalitiesViewModel()
            {
                Municipalities = _service.GetByCounty(id),
                CountyName = _countyService.Get(id).Name,
            };
            model.DirectUpdateCount = model.Municipalities.Where(m => m.IntroductionState == IntroductionState.Introduced).Count();

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.Management;

            return View("Views/FkbData/Management/Area/Municipalities.cshtml", model);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IMunicipality municipality = _service.Get(id);
            MunicipalityViewModel model = new MunicipalityViewModel()
            {
                //DataSets = _
            };

            switch (municipality.IntroductionState)
            {
                case IntroductionState.NotPlanned:
                    model.Caption = "Ikke planlagt innføring av direkteoppdatering i Sentral FKB";
                    break;
                case IntroductionState.Planned:
                    model.Caption = "Direkteoppdatering i Sentral FKB planlagt innført {0}";
                    model.DateTime = municipality.PlannedIntroductionDate;
                    break;
                case IntroductionState.Introduced:
                    model.Caption = "Direkteoppdatering i Sentral FKB innført {0}";
                    model.DateTime = municipality.IntroductionDate;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));
            ViewBag.ContextViewModel.Aspect = ContextViewModel.EnumAspect.Management;

            return View("Views/FkbData/Management/Area/Municipality.cshtml", model);
        }
    }
}