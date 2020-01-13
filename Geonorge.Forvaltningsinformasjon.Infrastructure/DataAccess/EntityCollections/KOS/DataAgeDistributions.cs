using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
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
                .GroupBy(s => s.DataSetId)
                .Select(g => new DataAgeDistribution
                {
                    DataSetName = g.First().DataSet.Name,
                    Year0 = g.Sum(s => s.Year0),
                    Year1 = g.Sum(s => s.Year1),
                    Year2 = g.Sum(s => s.Year2),
                    Year3 = g.Sum(s => s.Year3),
                    Year4 = g.Sum(s => s.Year4),
                    Years5To9 = g.Sum(s => s.Years5To9),
                    Years10To19 = g.Sum(s => s.Years10To19),
                    Older = g.Sum(s => s.Older)
                })
                .OrderBy(s => s.DataSetName)
                .ToList<IDataAgeDistribution>();
        }
    }
}
