using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Geonorge.Forvaltningsinformasjon.Models;

namespace Geonorge.Forvaltningsinformasjon.Controllers
{
    public class HomeController : Controller
    {
        private readonly FDV_Drift2Context _db;

        public HomeController(FDV_Drift2Context db) { _db = db; }
        
        public IActionResult Index()
        {
            var model = _db.Datasett.ToList();
            return View(model);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
