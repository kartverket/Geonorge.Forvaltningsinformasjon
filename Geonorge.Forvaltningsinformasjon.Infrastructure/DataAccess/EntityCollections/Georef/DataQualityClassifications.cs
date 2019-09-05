using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Georef;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.Georef
{
    class DataQualityClassifications : Entities<IDataQualityClassification, DataQualityClassification>, IDataQualityClassifications
    {
        public DataQualityClassifications(GeorefContext dbContext) : base(dbContext)
        {

        }
    }
}
