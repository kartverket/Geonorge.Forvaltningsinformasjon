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
    [Route("fkb-data/data-content/data-quality-distribution")]
    public class DataQualityDistributionController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;
        private IDataQualityDistributionService _dataQualityDistributionService;
        private ICountyService _countyService;
        private IMunicipalityService _municipalityService;
        private ApplicationSettings _applicationSettings;

        public DataQualityDistributionController(
            IContextViewModelHelper contextViewModelHelper,
            IDataQualityDistributionService dataQualityDistributionService,
            ICountyService countyService,
            IMunicipalityService municipalityService,
            ApplicationSettings applicationSettings)
        {
            _contextViewModelHelper = contextViewModelHelper;
            _dataQualityDistributionService = dataQualityDistributionService;
            _countyService = countyService;
            _municipalityService = municipalityService;
            _applicationSettings = applicationSettings;
        }

        public IActionResult Country()
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create();

            DataQualityDistributionViewModel model = new DataQualityDistributionViewModel(_dataQualityDistributionService.Get(), _applicationSettings.QualityCategoryColors)
            {
                Type = AdministrativeUnitType.Country,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataQualityDistribution
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            DataQualityDistributionViewModel model = new DataQualityDistributionViewModel(_dataQualityDistributionService.GetByCounty(id), _applicationSettings.QualityCategoryColors)
            {
                AdministrativeUnitName = _countyService.Get(id).Name,
                Type = AdministrativeUnitType.County,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataQualityDistribution
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery]int id)
        {
            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));

            DataQualityDistributionViewModel model = new DataQualityDistributionViewModel(_dataQualityDistributionService.GetByMunicipality(id), _applicationSettings.QualityCategoryColors)
            {
                AdministrativeUnitName = _municipalityService.Get(id).Name,
                Type = AdministrativeUnitType.Municipality,
                MetadataUrl = _applicationSettings.ExternalUrls.MetadataDataQualityDistribution
            };
            return View("Views/FkbData/DataContent/Aspects/DataQualityDistribution.cshtml", model);
        }
    }
}