using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS
{
    class DataSets : Entities<IDataSet, FdvDataSet>, IDataSets
    {
        public DataSets(KosContext dbContext) : base(dbContext)
        {

        }

        public List<IDataSet> GetByMunicipality(int id)
        {
            Project project = _dbContext.Set<Project>().Where(p => p.Municipality.Id == id).OrderBy(p => p.Year).LastOrDefault();

            if (project != null)
            {
                IOrderedEnumerable<FdvDataSet> dataSets = _dbContext.Set<FdvDataSet>().Where(d => d.ProjectId == project.Id && d.DataSet.Type == "FKB").Include(d => d.DataSet).Include(d => d.UpdateType).AsEnumerable<FdvDataSet>().OrderBy(d => d.Name);

                string strId = string.Format("{0:D4}", id);
                IQueryable<TransactionData> statistics = _dbContext.Set<TransactionData>().Where(s => s.MunicipalityNumber == strId);

                IEnumerable<FdvDataSet> result = dataSets.GroupJoin(
                                                        statistics, d => d.DataSetId, 
                                                        s => s.DataSetId, 
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

        private FdvDataSet ExtendDataSet(FdvDataSet dataSet, TransactionData statistics)
        {
            if (statistics != null)
            {
                dataSet.UpdateDate = statistics.GeonorgeFileDate;
                dataSet.IsEmpty = !statistics.ObjectCount.HasValue || statistics.ObjectCount.Value == 0;
            }
            return dataSet;
        }
    }
}
