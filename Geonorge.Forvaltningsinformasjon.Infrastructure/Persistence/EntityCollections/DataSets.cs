using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.EntityCollections
{
    class DataSets : Entities<IDataSet, Fdvdatasett>, IDataSets
    {
        public DataSets(FDV_Drift2Context dbContext) : base(dbContext)
        {

        }
    }
}
