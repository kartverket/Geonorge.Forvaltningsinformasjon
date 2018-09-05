using Geonorge.Forvaltningsinformasjon.Core.Models;
using Geonorge.Forvaltningsinformasjon.Core.Services;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Database;

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
            return new SentralFkbSummary();
        }
    }
}