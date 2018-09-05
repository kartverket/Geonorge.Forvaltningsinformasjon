using Geonorge.Forvaltningsinformasjon.Core.Models;
using Geonorge.Forvaltningsinformasjon.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Controllers
{
    [Route("sentralfkb")]
    public class SentralFkbController : Controller
    {
        private readonly ISentralFkbService _sentralFkbService;

        public SentralFkbController(ISentralFkbService sentralFkbService)
        {
            _sentralFkbService = sentralFkbService;
        }
        
        [HttpGet("fylke/{fylkesnummer}")]
        public IActionResult Fylke(string fylkesnummer)
        {
            SentralFkbFylkeSummary model = _sentralFkbService.GetFylkeSummary(fylkesnummer);

            return View(model);
        }
    }
}