using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    class TransactionDataStore : Entities<ITransactionData, TransactionData>, ITransactionDataStore
    {
        public TransactionDataStore(KosContext dbContext) : base(dbContext)
        {

        }

        public override List<ITransactionData> Get()
        {
            return GetTransactionData(_dbContext.Set<TransactionData>());
        }

        public List<ITransactionData> GetByCounty(int id)
        {
            string strId = string.Format("{0:D2}", id);

            IQueryable<Municipality> municipalities = _dbContext.Set<Municipality>().Where(k => k.CountyId == strId);

            return GetTransactionData(_dbContext.Set<TransactionData>().Where(s => municipalities.Any(k => k.Number == s.MunicipalityNumber)));
        }

        public List<ITransactionData> GetByMunicipality(int id)
        {
            string strId = string.Format("{0:D4}", id);

            return GetTransactionData(_dbContext.Set<TransactionData>().Where(s => s.MunicipalityNumber == strId));
        }

        private List<ITransactionData> GetTransactionData(IQueryable<TransactionData> transactionData)
        {
            return transactionData
                .GroupBy(s => s.DataSetId)
                .Select(g => new TransactionData
                {
                    SumLastYear = g.Sum(s => s.SumLastYear),
                    SumLastMonth = g.Sum(s => s.SumLastMonth),
                    SumLastWeek = g.Sum(s => s.SumLastWeek),
                    DataSet = g.First().DataSet
                })
                .Include(s => s.DataSet)
                .OrderBy(s => s.DataSetName)
                .ToList<ITransactionData>();
        }
    }
}
