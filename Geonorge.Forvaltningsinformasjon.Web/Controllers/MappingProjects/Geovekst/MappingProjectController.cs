using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Geonorge.Forvaltningsinformasjon.Web.Models.MappingProjects.Geovekst;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.MappingProjects.Geovekst
{
    [Route("mapping-projects/geovekst/projects")]
    public class MappingProjectController : Controller
    {
        private IMappingProjectService _mappingProjectService;
        private IMunicipalityService _municipalityService;
        private IOfficeService _offices;
        private KosContext _dbContext;

        public MappingProjectController(
            IMappingProjectService mappingProjectService, 
            IMunicipalityService municipalityService,
            IOfficeService offices,
            KosContext kosContext)
        {
            _mappingProjectService = mappingProjectService;
            _municipalityService = municipalityService;
            _offices = offices;
            _dbContext = kosContext;
        }

        [HttpGet("")]
        public IActionResult Index(
            string municipalityNumber,
            int officeId,
            MappingProjectState state,
            RelevantMappingProjectDeliveryType deliveryType,
            int year)
        {
            MappingProjectViewModel model = new MappingProjectViewModel(
                    _mappingProjectService.Get(municipalityNumber, officeId, state, deliveryType, year),
                    _municipalityService.Get(),
                    _offices.Get(),
                    municipalityNumber,
                    officeId,
                    state,
                    deliveryType,
                    year);

            return View("Views/MappingProjects/Geovekst/Projects/Projects.cshtml", model);
        }

        [HttpGet("project-details")]
        public IActionResult GetDetails(int id)
        {
            MappingProjectDetailsViewModel model = new MappingProjectDetailsViewModel(_mappingProjectService.Get(id));

            return View("Views/MappingProjects/Geovekst/Projects/ProjectDetails.cshtml", model);
        }

        [HttpGet("georef")]
        public List<Georef> Georef(string name)
        {
            var georefs = new List<Georef>();

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT PRJProsjekt.Navn,PRJBoksGeoref.PRJLeveranseType_Id, PRJBoksGeoref.BB_SorVest_E, PRJBoksGeoref.BB_SorVest_N, PRJBoksGeoref.BB_NordOst_E, PRJBoksGeoref.BB_NordOst_N,PRJBoksGeoref.Koordsys_Koordsys FROM PRJBoksGeoref INNER JOIN PRJProsjekt ON PRJBoksGeoref.PRJProsjekt_Id = PRJProsjekt.Id where Navn = @name";
                command.Parameters.Add(new SqlParameter("@name", name));
                _dbContext.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        Georef georef = new Georef();

                        var BB_SorVest_E = result.GetInt32(2);
                        var BB_SorVest_N = result.GetInt32(3);
                        var BB_NordOst_E = result.GetInt32(4);
                        var BB_NordOst_N = result.GetInt32(5);

                        georef.BBOX = $"{BB_SorVest_E},{BB_SorVest_N},{BB_NordOst_E},{BB_NordOst_N}";

                        var leveranseType = result.GetInt32(1);
                        if (leveranseType == 1)
                            georef.LAYERS = "ortofoto_prosjekt_standard";
                        else if(leveranseType == 2)
                            georef.LAYERS = "laser_prosjekt_standard";
                        else
                            georef.LAYERS = "fkb_prosjekt_standard";

                        georef.pn = result.GetString(0).Split(' ').First();

                        georefs.Add(georef);
                    }
                }
            }

            return georefs;
        }
    }

    public class Georef
    {
        public string pn { get; set; }
        public string BBOX { get; set; }
        public string LAYERS { get; set; }
    }
}
