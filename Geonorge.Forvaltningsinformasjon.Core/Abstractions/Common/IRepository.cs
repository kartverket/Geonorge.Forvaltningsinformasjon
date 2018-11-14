using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Common
{
    public interface IRepository
    {
        IQueryable<T> Get<T>() where T : class;
    }
}
