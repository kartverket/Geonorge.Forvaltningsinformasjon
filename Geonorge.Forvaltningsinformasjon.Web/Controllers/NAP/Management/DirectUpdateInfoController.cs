using System.ComponentModel;
using System.IO;
using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.Plandata;
using Microsoft.AspNetCore.Mvc;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers
{
    [Route("nap/info")]
    public class DirectUpdateInfoController : Controller, IAdministrativeUnitController
    {
        private KosContext _dbContext;

        public DirectUpdateInfoController(
            KosContext kosContext)
        {
            _dbContext = kosContext;
        }

        [HttpGet("")]
        public IActionResult Country()
        {
            CountiesViewModel model = new CountiesViewModel();
            model.Counties = new List<County>();

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT COUNT(PlanInfo.Kommune_Kommunenr) AS antallKommunerTotalt, Fylke.Fylkesnr, Fylke.Fylkesnavn FROM PlanInfo INNER JOIN Kommune ON PlanInfo.Kommune_Kommunenr = Kommune.Kommunenr INNER JOIN Fylke ON Kommune.Fylke_Fylkesnr = Fylke.Fylkesnr WHERE (Kommune.Aktiv = 1) AND(Fylke.Aktiv = 1) GROUP BY Fylke.Fylkesnr, Fylke.Fylkesnavn";
                _dbContext.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read()) 
                    {
                        County county = new County();
                        county.MunicipalityCount = result.GetInt32(0);
                        county.Number = result.GetString(1);
                        county.Name = result.GetString(2);
                        county.GeosynchIntroducedCount = 0;

                        using (var command2 = _dbContext.Database.GetDbConnection().CreateCommand())
                        {
                            command2.CommandText = "SELECT COUNT(PlanInfo.Kommune_Kommunenr) AS GeosynchIntroducedCount FROM PlanInfo INNER JOIN Kommune ON PlanInfo.Kommune_Kommunenr = Kommune.Kommunenr INNER JOIN Fylke ON Kommune.Fylke_Fylkesnr = Fylke.Fylkesnr WHERE(PlanInfo.GeosynkInnfort IS NOT NULL) AND(Fylke.Fylkesnr = '" + county.Number + "')";
                            using (var result2 = command2.ExecuteReader()) 
                            {
                                if (result2.HasRows) {
                                    result2.Read();
                                    county.GeosynchIntroducedCount = result2.GetInt32(0);
                                }
                            }
                        }

                            model.Counties.Add(county);
                    }
                }
            }

            model.GeosynchIntroducedCount = model.Counties.Sum(c => c.GeosynchIntroducedCount);

            return View("Views/NAP/Management/Aspects/DirectUpdateInfo/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery] int id)
        {
            return View();
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery] int id)
        {
            return View();
        }

    }
}