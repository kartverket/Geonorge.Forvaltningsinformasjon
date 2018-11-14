using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.Area.FkbData.Management.Area
{
    [Route("fkb-data/management/area/counties")]
    public class CountiesController : Controller
    {
        private ICountyService _service;

        public CountiesController(ICountyService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            CountiesViewModel model = new CountiesViewModel()
            {
                Counties = _service.GetAll(),
            };
            model.DirectUpdateCount = model.Counties.Sum(c => c.DirectUpdateCount);

            return View("Views/FkbData/Management/Area/Counties.cshtml", model);
        }
    }
}