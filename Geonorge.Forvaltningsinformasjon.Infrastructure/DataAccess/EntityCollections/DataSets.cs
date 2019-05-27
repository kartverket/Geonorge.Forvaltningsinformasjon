using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections
{
    class DataSets : Entities<IDataSet, Fdvdatasett>, IDataSets
    {
        public DataSets(FDV_Drift2Context dbContext) : base(dbContext)
        {

        }

        public List<IDataSet> GetByMunicipality(int id)
        {
            Fdvprosjekt project = _dbContext.Set<Fdvprosjekt>().Where(p => p.KommuneKommunenrNavigation.Id == id).OrderBy(p => p.Ar).LastOrDefault();

            if (project != null)
            {
                IOrderedEnumerable<Fdvdatasett> datasets = _dbContext.Set<Fdvdatasett>().Where(d => d.FdvprosjektId == project.Id && d.Datasett.Type == "FKB").Include(d => d.Datasett).Include(d => d.FdvdatasettForvaltningstype).AsEnumerable<Fdvdatasett>().OrderBy(d => d.Name);

                string strId = string.Format("{0:D4}", id);
                IQueryable<SentralFkbStatistikk> statistics = _dbContext.Set<SentralFkbStatistikk>().Where(s => s.KommuneKommunenr == strId);

                IEnumerable<Fdvdatasett> result = datasets.GroupJoin(
                                                        statistics, d => d.DatasettId, 
                                                        s => s.DatasettId, 
                                                        (d, s) => new {  Ds = d, Stat = s }
                                                    ).SelectMany(
                                                        d => d.Stat.DefaultIfEmpty(),
                                                        (d, s) => ExtendDataSet(d.Ds, s)
                                                        );
                return result.ToList<IDataSet>();
            }
            else
            {
                return new List<IDataSet>();
            }
        }

        private Fdvdatasett ExtendDataSet(Fdvdatasett dataSet, SentralFkbStatistikk statistics)
        {
            if (statistics != null)
            {
                dataSet.UpdateDate = statistics.GeonorgeFildato;
                dataSet.IsEmpty = !statistics.AntObjekter.HasValue || statistics.AntObjekter.Value == 0;
            }
            return dataSet;
        }
    }
}
