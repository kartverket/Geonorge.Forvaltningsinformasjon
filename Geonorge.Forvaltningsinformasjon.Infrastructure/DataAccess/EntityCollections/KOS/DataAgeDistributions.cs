using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    class DataAgeDistributions : IDataAgeDistributions
    {
        private KosContext _dbContext;

        public DataAgeDistributions(KosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IDataAgeDistribution> Get()
        {
            return GetDataAgeDistributions(_dbContext.Set<TransactionData>());
        }

        public List<IDataAgeDistribution> GetByCounty(int id)
        {
            string strId = string.Format("{0:D2}", id);

            return GetDataAgeDistributions(_dbContext.Set<TransactionData>().Where(c => c.MunicipalityNumber.StartsWith(strId)));
        }

        public List<IDataAgeDistribution> GetByMunicipality(int id)
        {
            string strId = string.Format("{0:D4}", id);

            return GetDataAgeDistributions(_dbContext.Set<TransactionData>().Where(c => c.MunicipalityNumber == strId));
        }

        // private 
        private List<IDataAgeDistribution> GetDataAgeDistributions(IQueryable<TransactionData> transactionData)
        {
            return transactionData
                .GroupBy(d => d.DataSetId)
                .Select(g => Create(g.First().DataSet.Name, g))
                .OrderBy(d => d.DataSetName)
                .ToList<IDataAgeDistribution>();
        }

        private DataAgeDistribution Create(string name, IGrouping<int,TransactionData> grouping)
        {
            DataAgeDistribution distribution = new DataAgeDistribution
            {
                DataSetName = name
            };

            distribution.TransactionCounts.Add(AgeCategory.Year0, grouping.Sum(s => s.Year0 ?? 0));
            distribution.TransactionCounts.Add(AgeCategory.Year1, grouping.Sum(s => s.Year1 ?? 0));
            distribution.TransactionCounts.Add(AgeCategory.Year2, grouping.Sum(s => s.Year2 ?? 0));
            distribution.TransactionCounts.Add(AgeCategory.Year3, grouping.Sum(s => s.Year3 ?? 0));
            distribution.TransactionCounts.Add(AgeCategory.Year4, grouping.Sum(s => s.Year4 ?? 0));
            distribution.TransactionCounts.Add(AgeCategory.Years5To9, grouping.Sum(s => s.Years5To9 ?? 0));
            distribution.TransactionCounts.Add(AgeCategory.Years10To19, grouping.Sum(s => s.Years10To19 ?? 0));
            distribution.TransactionCounts.Add(AgeCategory.Older, grouping.Sum(s => s.Older ?? 0));

            return distribution;
        }
    }
}
