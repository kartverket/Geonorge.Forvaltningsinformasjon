using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers
{
    public class Reports : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
