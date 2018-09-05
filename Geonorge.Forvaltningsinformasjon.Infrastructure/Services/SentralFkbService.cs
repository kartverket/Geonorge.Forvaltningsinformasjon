using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Models;
using Geonorge.Forvaltningsinformasjon.Core.Services;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Database;
using Remotion.Linq.Clauses;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Services
{
    public class SentralFkbService : ISentralFkbService
    {
        private readonly FDV_Drift2Context _context;

        public SentralFkbService(FDV_Drift2Context context)
        {
            _context = context;
        }

        public SentralFkbSummary GetCountrySummary()
        {
            var queryable = _context.SentralFkb
                .Where(s => s.DirekteoppdateringInfort != null)
                .GroupBy(s => s.KommuneKommunenrNavigation.FylkeFylkesnrNavigation)
                .Select(g => new SentralFkbSummaryLine()
                {
                    Fylke = g.Key,
                    AntallDirekteOppdatering = g.Count(),
                    AntallKommunerTotalt = _context.Kommune.Count(k => k.FylkeFylkesnr == g.Key.Fylkesnr)
                });
            
            return new SentralFkbSummary()
            {
                Result = queryable.ToList(),
            };
        }

        public SentralFkbFylkeSummary GetFylkeSummary(string fylkesnummer)
        {
            var result = _context.SentralFkb
                .Where(s => s.KommuneKommunenrNavigation.FylkeFylkesnr == fylkesnummer)
                .Select(r => new SentralFkbKommuneViewModel()
                    {
                        Kommune = new KommuneViewModel()
                        {
                            Navn = r.KommuneKommunenrNavigation.Kommunenavn,
                            Nummer = r.KommuneKommunenr
                        },
                        DirekteoppdateringInfort = r.DirekteoppdateringInfort,
                        PlanlagtInnforing = r.PlanlagtInnforing
                    }
                ).ToList();

            return new SentralFkbFylkeSummary()
            {
                Result = result
            };
        }
    }
}