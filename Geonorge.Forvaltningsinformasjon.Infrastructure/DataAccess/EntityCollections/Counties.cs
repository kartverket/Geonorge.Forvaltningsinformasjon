using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections
{
    class Counties : Entities<ICounty, Fylke>, ICounties
    {
        public Counties(FDV_Drift2Context dbContext) : base(dbContext)
        {
        }

        public override List<ICounty> Get()
        {
            return _dbContext.Set<Fylke>().Include(f => f.Kommune).ThenInclude(k => k.SentralFkb).ToList<ICounty>();
        }

        public ICounty GetByMunicipalityId(int municipalityId)
        {
            Kommune kommune = _dbContext.Set<Kommune>().Where(k => k.Id == municipalityId).FirstOrDefault();

            return kommune.FylkeFylkesnrNavigation;
        }
    }
}
