using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Database
{
    public class Repository : IRepository
    {
        private FDV_Drift2Context _dbContext;
        public Repository(FDV_Drift2Context dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return _dbContext.Set<T>().AsQueryable();
        }
    }
}       
