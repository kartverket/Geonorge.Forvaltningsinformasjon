using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Core.Models;
using Geonorge.Forvaltningsinformasjon.Core.Services;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Geonorge.Forvaltningsinformasjon.Models;

namespace Geonorge.Forvaltningsinformasjon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISentralFkbService _sentralFkbService;

        public HomeController(ISentralFkbService sentralFkbService)
        {
            _sentralFkbService = sentralFkbService;
        }
        
        public IActionResult Index()
        {
            SentralFkbSummary model = _sentralFkbService.GetCountrySummary();
            return View(model);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
