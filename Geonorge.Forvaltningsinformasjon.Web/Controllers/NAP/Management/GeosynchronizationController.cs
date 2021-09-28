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
using Microsoft.Data.SqlClient;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers
{
    [Route("nap/info")]
    public class GeosynchronizationController : Controller, IAdministrativeUnitController
    {
        private KosContext _dbContext;
        private IContextViewModelHelper _contextViewModelHelper;
        private IMunicipalityService _municipalityService;
        private ICountyService _countyService;

        public GeosynchronizationController(
            KosContext kosContext,
            IContextViewModelHelper contextViewModelHelper,
            IMunicipalityService municipalityService,
            ICountyService countyService
            )
        {
            _dbContext = kosContext;
            _contextViewModelHelper = contextViewModelHelper;
            _countyService = countyService;
            _municipalityService = municipalityService;
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
                            command2.CommandText = "SELECT COUNT(PlanInfo.Kommune_Kommunenr) AS GeosynchIntroducedCount FROM PlanInfo INNER JOIN Kommune ON PlanInfo.Kommune_Kommunenr = Kommune.Kommunenr INNER JOIN Fylke ON Kommune.Fylke_Fylkesnr = Fylke.Fylkesnr WHERE(PlanInfo.GeosynkInnfort IS NOT NULL) AND (Kommune.Aktiv = 1) AND (Fylke.Fylkesnr = '" + county.Number + "')";
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

            ViewBag.ContextViewModel = _contextViewModelHelper.Create();

            return View("Views/NAP/Management/Aspects/Geosynchronization/Country.cshtml", model);
        }

        [HttpGet("county")]
        public IActionResult County([FromQuery] int id)
        {
            ICounty county = _countyService.Get(id);

            MunicipalitiesViewModel model = new MunicipalitiesViewModel()
            {
                CountyName = county.Name
            };

            _dbContext.Database.OpenConnection();

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT COUNT(PlanInfo.Kommune_Kommunenr) AS TotalKommunerCount FROM PlanInfo INNER JOIN Kommune ON PlanInfo.Kommune_Kommunenr = Kommune.Kommunenr INNER JOIN Fylke ON Kommune.Fylke_Fylkesnr = Fylke.Fylkesnr WHERE (Kommune.Aktiv = 1) AND (Fylke.Fylkesnr = @fylkesnr)";
                command.Parameters.Add(new SqlParameter("@fylkesnr", id));
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        result.Read();
                        model.Count = result.GetInt32(0);
                    }
                }
            }

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT COUNT(PlanInfo.Kommune_Kommunenr) AS GeosynchIntroducedCount FROM PlanInfo INNER JOIN Kommune ON PlanInfo.Kommune_Kommunenr = Kommune.Kommunenr INNER JOIN Fylke ON Kommune.Fylke_Fylkesnr = Fylke.Fylkesnr WHERE(PlanInfo.GeosynkInnfort IS NOT NULL) AND (Kommune.Aktiv = 1) AND (Fylke.Fylkesnr = @fylkesnr)";
                command.Parameters.Add(new SqlParameter("@fylkesnr", id));
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        result.Read();
                        model.CountGeosynch = result.GetInt32(0);
                    }
                }
            }

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT DISTINCT FDVProsjekt.Kommune_Kommunenr, Kommune.Kommunenavn, FDVDatasettForvaltningstype.Type FROM FDVDatasett INNER JOIN FDVProsjekt ON FDVDatasett.FDVProsjekt_Id = FDVProsjekt.Id INNER JOIN Kommune ON FDVProsjekt.Kommune_Kommunenr = Kommune.Kommunenr LEFT OUTER JOIN FDVDatasettForvaltningstype ON FDVDatasett.FDVDatasettForvaltningstype_Id = FDVDatasettForvaltningstype.Id WHERE Kommune_Kommunenr LIKE '50%' AND Kommune.Aktiv = 1 AND[FDVProsjekt].Aktiv = 1 AND FDVDatasett.Id IN( SELECT MAX(FDVDatasett.id) FROM FDVDatasett INNER JOIN FDVProsjekt ON FDVDatasett.FDVProsjekt_Id = FDVProsjekt.Id  GROUP BY FDVProsjekt.Kommune_Kommunenr) group by FDVProsjekt.Kommune_Kommunenr, FDVDatasettForvaltningstype.Type,Kommune.Kommunenavn ORDER BY 1";
                command.Parameters.Add(new SqlParameter("@fylkesnr", "%" + id));
                using (var result = command.ExecuteReader())
                {
                    model.Municipalities = new List<GeosynchInfo>();
                    while (result.Read())
                    {
                        GeosynchInfo geosynchInfo = new GeosynchInfo();
                        geosynchInfo.MunicipalityNumber = result.GetString(0);
                        geosynchInfo.MunicipalityName = result.GetString(1);
                        geosynchInfo.UpdateType = result.GetString(2);
                        model.Municipalities.Add(geosynchInfo);
                    }
                }
            }


            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            return View("Views/NAP/Management/Aspects/Geosynchronization/County.cshtml", model);
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery] int id)
        {
            return View("Views/NAP/Management/Aspects/Geosynchronization/Municipality.cshtml");
        }

    }
}