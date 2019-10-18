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
            return _dbContext.Set<County>().Include(f => f.Municipalities).ThenInclude(k => k.CentralFkb).ToList<ICounty>();
        }

        public ICounty GetByMunicipalityId(int municipalityId)
        {
            Municipality municipality = _dbContext.Set<Municipality>().Where(k => k.Id == municipalityId).FirstOrDefault();

            return municipality.County;
        }
    }
}
