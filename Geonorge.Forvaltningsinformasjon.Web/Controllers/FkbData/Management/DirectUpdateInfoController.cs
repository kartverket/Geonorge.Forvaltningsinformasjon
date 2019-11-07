using System.ComponentModel;
using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.DirectUpdateInfo;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management
{
    [Route("fkb-data/management/direct-update-info")]
    public class DirectUpdateInfoController : Controller, IAdministrativeUnitController
    {
        private IMunicipalityService _municipalityService;
        private ICountyService _countyService;
        private IDataSetService _dataSetService;
        private IContextViewModelHelper _contextViewModelHelper;

        public DirectUpdateInfoController(
            IMunicipalityService municipalityService,
            ICountyService countyService,
            IDataSetService dataSetService,
            IContextViewModelHelper contextViewModelHelper)
        {
            _municipalityService = municipalityService;
            _countyService = countyService;
            _dataSetService = dataSetService;
            _contextViewModelHelper = contextViewModelHelper;
        }

        [HttpGet("")]
        public IActionResult Country()
        {
            CountiesViewModel model = new CountiesViewModel()
            {
                Counties = _countyService.Get(),
            };
            model.DirectUpdateCount = model.Counties.Sum(c => c.DirectUpdateCount);

            ViewBag.ContextViewModel = _contextViewModelHelper.Create();
            
            return View("Views/FkbData/Management/Aspects/DirectUpdateInfo/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ICounty county = _countyService.Get(id);

            MunicipalitiesViewModel model = new MunicipalitiesViewModel()
            {
                Municipalities = _municipalityService.GetByCounty(id),
                CountyName = county.Name,
                MapViewModel = new MapViewModel(county)
            };
            model.DirectUpdateCount = model.Municipalities.Where(m => m.IntroductionState == IntroductionState.Introduced).Count();

            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            return View("Views/FkbData/Management/Aspects/DirectUpdateInfo/County.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            IMunicipality municipality = _municipalityService.Get(id);

            MunicipalityViewModel model = new MunicipalityViewModel()
            {
                DataSets = _dataSetService.GetByMunicipality(id),
                Name = municipality.Name
            };

            switch (municipality.IntroductionState)
            {
                case IntroductionState.NotIntroduced:
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

            return View("Views/FkbData/Management/Aspects/DirectUpdateInfo/Municipality.cshtml", model);
        }
    }
}