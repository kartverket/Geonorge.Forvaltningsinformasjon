using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections
{
    class TransactionDataStore : Entities<ITransactionData, SentralFkbStatistikk>, ITransactionDataStore
    {
        public TransactionDataStore(FDV_Drift2Context dbContext) : base(dbContext)
        {

        }

        public override List<ITransactionData> Get()
        {
            return GetTransactionData(_dbContext.Set<SentralFkbStatistikk>());
        }

        public List<ITransactionData> GetByCounty(int id)
        {
            string strId = string.Format("{0:D2}", id);

            IQueryable<Kommune> kommuner = _dbContext.Set<Kommune>().Where(k => k.FylkeFylkesnr == strId);

            return GetTransactionData(_dbContext.Set<SentralFkbStatistikk>().Where(s => kommuner.Any(k => k.Kommunenr == s.KommuneKommunenr)));
        }

        public List<ITransactionData> GetByMunicipality(int id)
        {
            string strId = string.Format("{0:D4}", id);

            return GetTransactionData(_dbContext.Set<SentralFkbStatistikk>().Where(s => s.KommuneKommunenr == strId));
        }

        private List<ITransactionData> GetTransactionData(IQueryable<SentralFkbStatistikk> sentralFkbStatistikks)
        {
            return sentralFkbStatistikks
                .GroupBy(s => s.DatasettId)
                .Select(g => new SentralFkbStatistikk
                {
                    AntTransAr = g.Sum(s => s.AntTransAr),
                    AntTransMnd = g.Sum(s => s.AntTransMnd),
                    AntTransUke = g.Sum(s => s.AntTransUke),
                    DatasettIdNavigation = g.First().DatasettIdNavigation
                })
                .Include(s => s.DatasettIdNavigation)
                .OrderBy(s => s.DataSetName)
                .ToList<ITransactionData>();
        }
    }
}
