using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence
{
    class DataSetBase<T, TImpl> : IDataSet<T> 
        where T : IEntityBase 
        where TImpl : class, T
    {
        protected FDV_Drift2Context _dbContext;

        public DataSetBase(FDV_Drift2Context dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual List<T> Get()
        {
            return _dbContext.Set<TImpl>().AsEnumerable<T>().ToList();
        }

        public T Get(string id)
        {
            return _dbContext.Set<TImpl>().Where(e => e.Id == id).First();
        }

        public bool Exists(string id)
        {
            return _dbContext.Set<TImpl>().Where(e => e.Id == id).FirstOrDefault() != default(TImpl);
        }

    }
}
