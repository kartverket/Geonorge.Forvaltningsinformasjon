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
            return _dbContext.Set<Municipality>().Where(k => k.County.Id == id).Include(k => k.CentralFkb).AsEnumerable<IMunicipality>().ToList();
        }

        public override IMunicipality Get(int id)
        {
            return _dbContext.Set<Municipality>().Where(k => k.Id == id).Include(k => k.CentralFkb).Include(k => k.CoordinateSystemObject).First();
        }

        public override List<IMunicipality> Get()
        {
            return _dbContext.Set<Municipality>().Include(k => k.CentralFkb).AsEnumerable<IMunicipality>().ToList();
        }
    }
}
