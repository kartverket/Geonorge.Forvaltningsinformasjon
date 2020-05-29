using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.DataContent
{
    [Route("fkb-data/data-content/data-age-distribution")]
    public class DataAgeDistributionController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;
        private IDataAgeDistributionService _dataAgeDistributionService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;
        private ApplicationSettings _applicationSettings;

        public DataAgeDistributionController(
            IContextViewModelHelper contextViewModelHelper,
            IDataAgeDistributionService dataAgeDistributionService,
            ICountyService countyService,
            IMunicipalityService municipalityService,
            ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _dataAgeDistributionService = dataAgeDistributionService;
            _countyService = countyService;
            _municipalityService = municipalityService;
            _applicationSettings = applicationSettings;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();

            DataAgeDistributionViewModel model = new DataAgeDistributionViewModel(_dataAgeDistributionService.Get(), _applicationSettings.AgeCategoryColors)
            {
                Type = AdministrativeUnitType.Country,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataAgeDistribution
            };
            return View("Views/FkbData/DataContent/Aspects/DataAgeDistribution.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            DataAgeDistributionViewModel model = new DataAgeDistributionViewModel(_dataAgeDistributionService.GetByCounty(id), _applicationSettings.AgeCategoryColors)
            {
                AdministrativeUnitName = _countyService.Get(id).Name,
                Type = AdministrativeUnitType.County,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataAgeDistribution
            };
            return View("Views/FkbData/DataContent/Aspects/DataAgeDistribution.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));

            DataAgeDistributionViewModel model = new DataAgeDistributionViewModel(_dataAgeDistributionService.GetByMunicipality(id), _applicationSettings.AgeCategoryColors)
            {
                AdministrativeUnitName = _municipalityService.Get(id).Name,
                Type = AdministrativeUnitType.Municipality,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataAgeDistribution
            };
            return View("Views/FkbData/DataContent/Aspects/DataAgeDistribution.cshtml", model);
        }
    }
}