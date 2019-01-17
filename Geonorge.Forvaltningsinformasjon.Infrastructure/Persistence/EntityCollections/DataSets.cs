using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.EntityCollections
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
                return _dbContext.Set<Fdvdatasett>().Where(d => d.FdvprosjektId == project.Id).Include(d => d.Datasett).AsEnumerable<IDataSet>().OrderBy(d => d.Name).ToList();
            }
            else
            {
                return new List<IDataSet>();
            }
        }
    }
}
