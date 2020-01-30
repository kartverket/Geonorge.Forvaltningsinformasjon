using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    internal class Counties : Entities<ICounty, County>, ICounties
    {
        public Counties(KosContext dbContext) : base(dbContext)
        {

        }

        public override List<ICounty> Get()
        {
            return _dbContext.Set<County>().Where(c => c.Active > 0).Include(c => c.Municipalities).ThenInclude(m => m.CentralFkb).ToList<ICounty>();
        }

        public override ICounty Get(int id)
        {
            string strId = string.Format("{0:D2}", id);
            return _dbContext.Set<County>().Where(e => e.Active > 0 && e.Number == strId).First();
        }

        public ICounty GetByMunicipalityId(int municipalityId)
        {
            string strId = string.Format("{0:D4}", municipalityId);
            Municipality municipality = _dbContext.Set<Municipality>().Where(m => m.Active > 0 && m.Number == strId).FirstOrDefault();

            return municipality.County;
        }
    }
}
