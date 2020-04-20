using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    class DataQualityDistributions : IDataQualityDistributions
    {
        private KosContext _dbContext;

        public DataQualityDistributions(KosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IDataQualityDistribution> Get()
        {
            return GetDataQualityDistributions(_dbContext.Set<TransactionData>());
        }

        public List<IDataQualityDistribution> GetByCounty(int id)
        {
            string strId = string.Format("{0:D2}", id);

            return GetDataQualityDistributions(_dbContext.Set<TransactionData>().Where(c => c.MunicipalityNumber.StartsWith(strId)));
        }

        public List<IDataQualityDistribution> GetByMunicipality(int id)
        {
            string strId = string.Format("{0:D4}", id);

            return GetDataQualityDistributions(_dbContext.Set<TransactionData>().Where(c => c.MunicipalityNumber == strId));
        }

        // private 
        private List<IDataQualityDistribution> GetDataQualityDistributions(IQueryable<TransactionData> transactionData)
        {
            return transactionData
                .Include(d => d.DataSet)
                .AsEnumerable()
                .GroupBy(d => d.DataSetId)
                .Select(g => Create(g.First().DataSet.Name, g))
                .OrderBy(d => d.DataSetName)
                .ToList();
        }

        private IDataQualityDistribution Create(string name, IGrouping<int, TransactionData> grouping)
        {
            DataQualityDistribution distribution = new DataQualityDistribution
            {
                DataSetName = name,
                ObjectCount = grouping.Sum(s => s.ObjectCount ?? 0)
            };

            distribution.TransactionCounts.Add(QualityCategory.Measured, grouping.Sum(s => s.Measured ?? 0));
            distribution.TransactionCounts.Add(QualityCategory.PhotogrammetricB, grouping.Sum(s => s.PhotogrammetricB ?? 0));
            distribution.TransactionCounts.Add(QualityCategory.PhotogrammetricC, grouping.Sum(s => s.PhotogrammetricC ?? 0));
            distribution.TransactionCounts.Add(QualityCategory.DigitalizedM200, grouping.Sum(s => s.DigitalizedM200 ?? 0));
            distribution.TransactionCounts.Add(QualityCategory.DigitalizedS200, grouping.Sum(s => s.DigitalizedS200 ?? 0));
            distribution.TransactionCounts.Add(QualityCategory.NotMeasured, grouping.Sum(s => s.NotMeasured ?? 0));

            return distribution;
        }
    }
}
