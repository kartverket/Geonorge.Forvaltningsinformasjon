using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Geonorge.Forvaltningsinformasjon.Web.Models.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers
{
    public class Reports : Controller
    {
        private KosContext _dbContext;

        public Reports(KosContext kosContext) 
        {
            _dbContext = kosContext;
        }

        [HttpGet("/rapport")]
        public IActionResult Index(string rapport, string fnr, string k)
        {
            _dbContext.Database.OpenConnection();

            if (!string.IsNullOrEmpty(rapport) && !string.IsNullOrEmpty(fnr)) 
            {
                Report report = new Report();

                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT RAPType.Type, RAPFylke.Fylke_Fylkesnr,Fylke.Fylkesnavn, RAPFylke.Tidspunkt, HTMLRapport FROM RAPFylke INNER JOIN RAPType ON RAPFylke.RAPType_Id = RAPType.Id INNER JOIN Fylke ON RAPFylke.Fylke_Fylkesnr = Fylke.Fylkesnr WHERE(RAPFylke.Aktiv = 1) AND(RAPType.Type = @type) AND Fylke_Fylkesnr=@fylke";
                    command.Parameters.Add(new SqlParameter("@type", rapport));
                    command.Parameters.Add(new SqlParameter("@fylke", fnr));
                    using (var result = command.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            result.Read();
                            report.Type = result.GetString(0);
                            report.FylkesNummer = result.GetString(1);
                            report.FylkesNavn = result.GetString(2);
                            report.Tidspunkt = result.GetDateTime(3);
                            report.HTMLRapport = result.GetString(4);
                        }
                    }
                }

                return View("County", report);
            }
            else if (!string.IsNullOrEmpty(rapport) && !string.IsNullOrEmpty(k))
            {
                Report report = new Report();

                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT RAPType.Type, RAPKommune.Kommune_Kommunenr, Kommune.Kommunenavn, RAPKommune.Tidspunkt, RAPKommune.HTMLRapport FROM RAPType INNER JOIN RAPKommune ON RAPType.Id = RAPKommune.RAPType_Id INNER JOIN Kommune ON RAPKommune.Kommune_Kommunenr = Kommune.Kommunenr where Type = @type and Kommune_Kommunenr = @knr";
                    command.Parameters.Add(new SqlParameter("@type", rapport));
                    command.Parameters.Add(new SqlParameter("@knr", k));
                    using (var result = command.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            result.Read();
                            report.Type = result.GetString(0);
                            report.KommuneNummer = result.GetString(1);
                            report.KommuneNavn = result.GetString(2);
                            report.Tidspunkt = result.GetDateTime(3);
                            report.HTMLRapport = result.GetString(4);
                        }
                    }
                }
                return View("Municipality", report);
            }
            else if (!string.IsNullOrEmpty(rapport))
            {
                List<Report> reports = new List<Report>();

                if (string.IsNullOrEmpty(fnr)) 
                { 
                    using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "SELECT RAPType.Type, RAPLand.Tidspunkt, RAPLand.HTMLRapport, RAPType.Beskrivelse FROM RAPLand INNER JOIN RAPType ON RAPLand.RAPType_Id = RAPType.Id WHERE(RAPLand.Aktiv = 1) AND(RAPType.Type = @type)";
                        command.Parameters.Add(new SqlParameter("@type", rapport));
                        using (var result = command.ExecuteReader())
                        {
                            if (result.HasRows)
                            {
                                result.Read();
                                Report report = new Report();

                                report.Type = result.GetString(0);
                                report.Tidspunkt = result.GetDateTime(1);
                                report.HTMLRapport = result.GetString(2);

                                return View("Country", report);
                            }
                        }    
                    }
                }

                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT RAPType.Type, RAPFylke.Fylke_Fylkesnr,Fylke.Fylkesnavn, RAPFylke.Tidspunkt FROM RAPFylke INNER JOIN RAPType ON RAPFylke.RAPType_Id = RAPType.Id INNER JOIN Fylke ON RAPFylke.Fylke_Fylkesnr = Fylke.Fylkesnr WHERE(RAPFylke.Aktiv = 1) AND(RAPType.Type = @type) ORDER BY Fylkesnavn";
                    command.Parameters.Add(new SqlParameter("@type", rapport));
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Report report = new Report();

                            report.Type = result.GetString(0);
                            report.FylkesNummer = result.GetString(1);
                            report.FylkesNavn = result.GetString(2);
                            report.Tidspunkt = result.GetDateTime(3);

                            reports.Add(report);
                        }
                    }
                }


                return View("Reports", reports);
            }
            else 
            { 
                List<Report> reports = new List<Report>();

                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT  [Id],[Type],[Beskrivelse] FROM[KOS_Prod_Replika].[dbo].[RAPType] where aktiv = 1 and Nivaa='Hoved' and Fagfelt = 'FKB'  ORDER BY Type";
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Report report = new Report();

                            report.Id = result.GetInt32(0);
                            report.Type = result.GetString(1);
                            report.Beskrivelse = result.GetString(2);

                            reports.Add(report);
                        }
                    }
                }

                return View(reports);

            }
        }
    }
}
