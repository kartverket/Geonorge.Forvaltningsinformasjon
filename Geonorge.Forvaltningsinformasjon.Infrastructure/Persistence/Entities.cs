using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence
{
    class Entities<T, TImpl> : IEntities<T> 
        where T : IEntityBase 
        where TImpl : class, T
    {
        protected FDV_Drift2Context _dbContext;

        public Entities(FDV_Drift2Context dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual List<T> Get()
        {
            return _dbContext.Set<TImpl>().AsEnumerable<T>().ToList();
        }

        public virtual T Get(int id)
        {
            return _dbContext.Set<TImpl>().Where(e => e.Id == id).First();
        }

        public bool Exists(int id)
        {
            return _dbContext.Set<TImpl>().Where(e => e.Id == id).FirstOrDefault() != default(TImpl);
        }

    }
}
