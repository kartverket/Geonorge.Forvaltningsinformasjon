﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Models.MappingProjects.Geovekst;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.MappingProjects.Geovekst
{
    [Route("mapping-projects/geovekst/projects")]
    public class MappingProjectController : Controller
    {
        private IMappingProjectService _mappingProjectService;

        public MappingProjectController(IMappingProjectService mappingProjectService)
        {
            _mappingProjectService = mappingProjectService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            MappingProjectViewModel model = new MappingProjectViewModel()
            {
                MappingProjects = _mappingProjectService.Get()
            };
            return View("Views/MappingProjects/Geovekst/Projects/Projects.cshtml", model);
        }
    }
}
