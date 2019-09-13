using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Georef;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.Georef
{
    class DataQualityClassifications : Entities<IDataQualityClassification, DataQualityClassification>, IDataQualityClassifications
    {
        public DataQualityClassifications(GeorefContext dbContext) : base(dbContext)
        {

        }

        public override List<IDataQualityClassification> Get()
        {
            return GetListOfSums(_dbContext.Set<DataQualityClassification>());
        }

        public List<IDataQualityClassification> GetByCounty(int id)
        {
            string strId = string.Format("{0:D2}", id);

            return GetListOfSums(_dbContext.Set<DataQualityClassification>().Where(c => c.MunicipalityNumber.StartsWith(strId)));
        }

        public List<IDataQualityClassification> GetByMunicipality(int id)
        {
            string strId = string.Format("{0:D4}", id);

            return GetListOfSums(_dbContext.Set<DataQualityClassification>().Where(c => c.MunicipalityNumber == strId));
        }

        private List<IDataQualityClassification> GetListOfSums(IQueryable<DataQualityClassification> dataQualityClassifications)
        {
            return dataQualityClassifications
                .GroupBy(c => c.Class)
                .Select(s => new DataQualityClassification
                {
                    Class = s.First().Class,
                    Area = s.Sum(c => c.Area)
                })
                .OrderBy(c => c.Class)
                .ToList<IDataQualityClassification>();
        }
    }
}
