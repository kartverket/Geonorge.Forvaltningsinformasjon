using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.EntityCollections
{
    class TransactionDataStore : Entities<ITransactionData, SentralFkbStatistikk>, ITransactionDataStore
    {
        public TransactionDataStore(FDV_Drift2Context dbContext) : base(dbContext)
        {

        }

        public override List<ITransactionData> Get()
        {
            return _dbContext.Set<SentralFkbStatistikk>()
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

        public List<ITransactionData> GetByCounty(int id)
        {
            string strId = id.ToString();

            IQueryable<Kommune> kommuner = _dbContext.Set<Kommune>().Where(k => k.FylkeFylkesnr == strId);

            return _dbContext.Set<SentralFkbStatistikk>()
                .Where(s => kommuner.Any(k => k.Kommunenr == s.KommuneKommunenr))
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

        public List<ITransactionData> GetByMunicipality(int id)
        {
            string strId = id.ToString();

            return _dbContext.Set<SentralFkbStatistikk>()
                .Where(s => s.KommuneKommunenr == strId)
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
