using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.DataSets
{
    class MunicipalityDataSet : DataSetBase<IMunicipality, Kommune>, IMunicipalityDataSet
    {
        public MunicipalityDataSet(FDV_Drift2Context dbContext) : base(dbContext)
        {

        }

        public List<IMunicipality> GetByCounty(string id)
        {
            return _dbContext.Set<Kommune>().Where(k => k.FylkeFylkesnr == id).Include(k => k.SentralFkb).AsEnumerable<IMunicipality>().ToList();
        }
    }
}
