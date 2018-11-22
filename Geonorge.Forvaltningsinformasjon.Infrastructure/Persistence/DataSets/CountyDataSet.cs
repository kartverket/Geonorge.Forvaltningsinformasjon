using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.DataSets
{
    class CountyDataSet : DataSetBase<ICounty, Fylke>, ICountyDataSet
    {
        public CountyDataSet(FDV_Drift2Context dbContext) : base(dbContext)
        {
        }

        public override List<ICounty> Get()
        {
            return _dbContext.Set<Fylke>().Include(f => f.Kommune).ThenInclude(k => k.SentralFkb).ToList<ICounty>();
        }
    }
}
