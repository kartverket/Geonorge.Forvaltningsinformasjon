using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    internal class DataQualityClassifications : IDataQualityClassifications
    {
        private KosContext _dbContext;

        public DataQualityClassifications(KosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IDataQualityClassification> Get()
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
            DataQualityClassification sum = new DataQualityClassification();

            dataQualityClassifications.ToList().ForEach(dc =>
            {
                sum.AreaA += dc.AreaA;
                sum.AreaB += dc.AreaB;
                sum.AreaC += dc.AreaC;
                sum.AreaD += dc.AreaD;
            });

            List<IDataQualityClassification> resultList = new List<IDataQualityClassification>();

            resultList.Add(new Entities.Custom.DataQualityClassification
            {
                Class = "A",
                Area = sum.AreaA
            });

            resultList.Add(new Entities.Custom.DataQualityClassification
            {
                Class = "B",
                Area = sum.AreaB
            });

            resultList.Add(new Entities.Custom.DataQualityClassification
            {
                Class = "C",
                Area = sum.AreaC
            });

            resultList.Add(new Entities.Custom.DataQualityClassification
            {
                Class = "D",
                Area = sum.AreaD
            });

            return resultList;
        }
    }
}
