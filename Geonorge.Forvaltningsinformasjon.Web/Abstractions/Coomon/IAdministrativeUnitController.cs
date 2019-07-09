using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common
{
    interface IAdministrativeUnitController
    {
        IActionResult Country();
        IActionResult County(int id);
        IActionResult Municipality(int id);
    }
}
