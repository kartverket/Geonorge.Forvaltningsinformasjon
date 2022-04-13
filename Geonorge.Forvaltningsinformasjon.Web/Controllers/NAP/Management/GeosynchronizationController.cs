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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

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

            string idSql = id.ToString();
            if(idSql.Length == 1)
                idSql = "0" + idSql;

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
                command.CommandText = "SELECT DISTINCT FDVProsjekt.Kommune_Kommunenr, Kommune.Kommunenavn, FDVDatasettForvaltningstype.Type FROM FDVDatasett INNER JOIN FDVProsjekt ON FDVDatasett.FDVProsjekt_Id = FDVProsjekt.Id INNER JOIN Kommune ON FDVProsjekt.Kommune_Kommunenr = Kommune.Kommunenr INNER JOIN Datasett ON FDVDatasett.Datasett_Id = Datasett.Id LEFT OUTER JOIN FDVDatasettForvaltningstype ON FDVDatasett.FDVDatasettForvaltningstype_Id = FDVDatasettForvaltningstype.Id WHERE Datasett.Navn = 'Reguleringsplan - vedtatt' and(FDVProsjekt.Kommune_Kommunenr LIKE @fylkesnr) AND(Kommune.Aktiv = 1) AND(FDVProsjekt.Aktiv = 1) and FDVProsjekt.Ar = (select max(Ar) from FDVProsjekt where Kommune_Kommunenr LIKE @fylkesnr ) GROUP BY FDVProsjekt.Ar,FDVProsjekt.Kommune_Kommunenr, FDVDatasettForvaltningstype.Type, Kommune.Kommunenavn ORDER BY FDVProsjekt.Kommune_Kommunenr";
                command.Parameters.Add(new SqlParameter("@fylkesnr", idSql + "%"));
                using (var result = command.ExecuteReader())
                {
                    model.Municipalities = new List<GeosynchInfo>();
                    while (result.Read())
                    {
                        GeosynchInfo geosynchInfo = new GeosynchInfo();
                        geosynchInfo.MunicipalityNumber = result.GetString(0);
                        geosynchInfo.MunicipalityName = result.GetString(1);
                        geosynchInfo.UpdateType = result.GetString(2);
                        if (geosynchInfo.UpdateType == "SOSI originaldata")
                            geosynchInfo.UpdateType = "Periodisk ajourhold";
                        GetStatus(ref geosynchInfo);
                        model.Municipalities.Add(geosynchInfo);
                    }
                }
            }


            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, true));

            return View("Views/NAP/Management/Aspects/Geosynchronization/County.cshtml", model);
        }

        private void GetStatus(ref GeosynchInfo geosynchInfo)
        {
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                Dictionary<int, string> statuses = new Dictionary<int, string>();
                command.CommandText = "SELECT DISTINCT PlanDatasett.PlanDatasettStatus_Id, PlanDatasettStatus.Beskrivelse FROM PlanDatasett INNER JOIN PlanDatasettStatus ON PlanDatasett.PlanDatasettStatus_Id = PlanDatasettStatus.Id WHERE(PlanDatasett.Kommune_Kommunenr = "+ geosynchInfo.MunicipalityNumber + ")";
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        statuses.Add(result.GetInt32(0), result.GetString(1));
                    }
                }

                if (statuses.ContainsKey(4))
                {
                    geosynchInfo.Status = "red";
                    geosynchInfo.StatusDescription = statuses[4];
                }
                else if (statuses.ContainsKey(3))
                {
                    geosynchInfo.Status = "yellow";
                    geosynchInfo.StatusDescription = statuses[3];
                }
                else if (statuses.ContainsKey(2))
                {
                    geosynchInfo.Status = "green";
                    geosynchInfo.StatusDescription = statuses[2];
                }
                else if(statuses.ContainsKey(1))
                {
                    geosynchInfo.Status = "grey";
                    geosynchInfo.StatusDescription = statuses[1];
                }
                else if (statuses.ContainsKey(5))
                {
                    geosynchInfo.Status = "grey";
                    geosynchInfo.StatusDescription = statuses[5];
                }
            }
        }

        [HttpGet("municipality")]
        public IActionResult Municipality([FromQuery] int id)
        {
            IMunicipality municipality = _municipalityService.Get(id);

            MunicipalityViewModel model = new MunicipalityViewModel()
            {
                DataSets = GetDataSets(id),
                Name = municipality.Name
            };

            _dbContext.Database.OpenConnection();

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT [GeosynkInnfort] FROM [KOS_Prod_Replika].[dbo].[PlanInfo] where [Kommune_Kommunenr] = @kommunenr";
                command.Parameters.Add(new SqlParameter("@kommunenr", id));
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        result.Read();
                        var geosynkInnfort = !result.IsDBNull(0) ? result.GetString(0) :"";

                        if (!string.IsNullOrEmpty(geosynkInnfort))
                            model.StatusMessage = "Geosynkronisering av plandata innført: " + FormatDate(geosynkInnfort);
                        else
                            model.StatusMessage = "Geosynkronisering av plandata ikke innført";

                    }
                }
            }



            ViewBag.ContextViewModel = _contextViewModelHelper.Create(_contextViewModelHelper.Id2Key(id, false));


            return View("Views/NAP/Management/Aspects/Geosynchronization/Municipality.cshtml", model);
        }

        public List<DataSet> GetDataSets(int id)
        {
            var dataSets = new List<DataSet>();

            _dbContext.Database.OpenConnection();

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Datasett.Navn, FDVDatasettForvaltningstype.Type, PlanDatasett.SisteLeveranseKommune,PlanDatasett.FilGenerertGeonorge, PlanDatasett.OppdateringStatusKommentar,PlanDatasett.PlanDatasettStatus_Id, PlanDatasettStatus.Beskrivelse FROM FDVDatasett INNER JOIN FDVProsjekt ON FDVDatasett.FDVProsjekt_Id = FDVProsjekt.Id INNER JOIN Kommune ON FDVProsjekt.Kommune_Kommunenr = Kommune.Kommunenr INNER JOIN Datasett ON FDVDatasett.Datasett_Id = Datasett.Id INNER JOIN  PlanDatasett ON Datasett.Id = PlanDatasett.Datasett_Id INNER JOIN PlanDatasettStatus ON PlanDatasett.PlanDatasettStatus_Id = PlanDatasettStatus.Id LEFT OUTER JOIN  FDVDatasettForvaltningstype ON FDVDatasett.FDVDatasettForvaltningstype_Id = FDVDatasettForvaltningstype.Id WHERE PlanDatasett.SisteLeveranseKommune IS NOT NULL AND  (FDVProsjekt.Kommune_Kommunenr = @kommune) AND PlanDatasett.Kommune_Kommunenr = @kommune AND (Kommune.Aktiv = 1) AND (FDVProsjekt.Aktiv = 1) AND (Datasett.Aktiv = 1) AND (Datasett.Type = 'Plandata') and FDVProsjekt.Ar  = ( select max(Ar) from FDVProsjekt where Kommune_Kommunenr = @kommune ) GROUP BY FDVDatasettForvaltningstype.Type, Datasett.Type, PlanDatasett.SisteLeveranseKommune, Datasett.Navn, PlanDatasett.FilGenerertGeonorge, PlanDatasett.OppdateringStatusKommentar, PlanDatasett.PlanDatasettStatus_Id, PlanDatasettStatus.Beskrivelse ORDER BY Datasett.Navn DESC";
                command.Parameters.Add(new SqlParameter("@kommune", id));
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        var dataset = new DataSet();
                        dataset.Name = result.GetString(0);
                        dataset.UpdateTypeName = result.GetString(1);
                        if (dataset.UpdateTypeName == "SOSI originaldata")
                            dataset.UpdateTypeName = "Periodisk ajourhold";
                        DateTime? lastDelivery = null;
                        if (!result.IsDBNull(2))
                            lastDelivery = result.GetDateTime(2);
                        dataset.LastDeliveryFromMunicipality = lastDelivery;

                        DateTime? deliveredGeonorge = null;
                        if (!result.IsDBNull(3))
                            deliveredGeonorge = result.GetDateTime(3);
                        dataset.FileGeneratedGeonorge = deliveredGeonorge;

                        dataset.Comment = !result.IsDBNull(4) ?result.GetString(4) : "";

                        var status = !result.IsDBNull(5) ? result.GetInt32(5) : 1;
                        dataset.StatusDescription = !result.IsDBNull(6) ? result.GetString(6) : "";

                        if (status == 4)
                            dataset.Status = "red";
                        else if(status == 3)
                            dataset.Status = "yellow";
                        else if (status == 2)
                            dataset.Status = "green";
                        else if (status == 1 || status == 5)
                            dataset.Status = "";                  

                        dataSets.Add(dataset);
                    }
                }
            }

            return dataSets;
        }

        private string FormatDate(string date)
        {
            if (!string.IsNullOrEmpty(date) && date.Length == 8)
            {
                var dateString = date.Substring(6, 2) + "." + date.Substring(4, 2) + "." + date.Substring(0, 4);
                date = dateString;
            }

            return date;
        }

    }
}