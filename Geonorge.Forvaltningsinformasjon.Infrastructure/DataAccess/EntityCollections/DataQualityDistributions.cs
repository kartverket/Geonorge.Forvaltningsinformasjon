using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections
{
    class DataQualityDistributions : IDataQualityDistributions
    {
        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public List<IDataQualityDistribution> Get()
        {
            throw new NotImplementedException();
        }

        public IDataQualityDistribution Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
