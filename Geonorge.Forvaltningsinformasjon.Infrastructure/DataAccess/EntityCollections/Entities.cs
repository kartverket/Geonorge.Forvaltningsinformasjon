﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections
{
    class Entities<T, TImpl>
        where T : IEntityBase
        where TImpl : class, T
    {
        protected DbContext _dbContext;

        public Entities(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual List<T> Get()
        {
            return _dbContext.Set<TImpl>().Where(e => e.Active > 0).ToList<T>();
        }

        public virtual T Get(int id)
        {
            return _dbContext.Set<TImpl>().Where(e => e.Id == id && e.Active > 0).First();
        }

        public bool Exists(int id)
        {
            return _dbContext.Set<TImpl>().Where(e => e.Id == id && e.Active > 0).FirstOrDefault() != default(TImpl);
        }
    }
}
