using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    internal class Municipalities : Entities<IMunicipality, Municipality>, IMunicipalities
    {
        public Municipalities(KosContext dbContext) : base(dbContext)
        {

        }

        public List<IMunicipality> GetByCounty(int id)
        {
            string strId = string.Format("{0:D2}", id);

            return _dbContext.Set<Municipality>().Where(m => m.Active > 0 && m.CountyId == strId).Include(m => m.CentralFkb).ToList<IMunicipality>();
        }

        public override IMunicipality Get(int id)
        {
            string strId = string.Format("{0:D4}", id);
            return _dbContext.Set<Municipality>().Where(m => m.Active > 0 && m.Number == strId).Include(m => m.CentralFkb).Include(m => m.CoordinateSystemObject).First();
        }

        public override List<IMunicipality> Get()
        {
            return _dbContext.Set<Municipality>().Where(m => m.Active > 0).Include(k => k.CentralFkb).ToList<IMunicipality>();
        }
    }
}
