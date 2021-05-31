using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Models.MappingProjects.Geovekst;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.MappingProjects.Geovekst
{
    [Route("mapping-projects/geovekst/projects")]
    public class MappingProjectController : Controller
    {
        private IMappingProjectService _mappingProjectService;
        private IMunicipalityService _municipalityService;
        private IOfficeService _offices;

        public MappingProjectController(
            IMappingProjectService mappingProjectService, 
            IMunicipalityService municipalityService,
            IOfficeService offices)
        {
            _mappingProjectService = mappingProjectService;
            _municipalityService = municipalityService;
            _offices = offices;
        }

        [HttpGet("")]
        public IActionResult Index(
            string municipalityNumber,
            int officeId,
            MappingProjectState state,
            RelevantMappingProjectDeliveryType deliveryType,
            int year)
        {
            MappingProjectViewModel model = new MappingProjectViewModel(
                    _mappingProjectService.Get(municipalityNumber, officeId, state, deliveryType, year),
                    _municipalityService.Get(),
                    _offices.Get(),
                    municipalityNumber,
                    officeId,
                    state,
                    deliveryType,
                    year);

            return View("Views/MappingProjects/Geovekst/Projects/Projects.cshtml", model);
        }

        [HttpGet("project-details")]
        public IActionResult GetDetails(int id)
        {
            MappingProjectDetailsViewModel model = new MappingProjectDetailsViewModel(_mappingProjectService.Get(id));

            return View("Views/MappingProjects/Geovekst/Projects/ProjectDetails.cshtml", model);
        }
    }
}
