using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections
{
    class DataQualityClassifications : IDataQualityClassifications
    {
        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public List<IDataQualityClassification> Get()
        {
            throw new NotImplementedException();
        }

        public IDataQualityClassification Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
