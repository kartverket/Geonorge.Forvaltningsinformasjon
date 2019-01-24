using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IEntities<T>
    {
        List<T> Get();

        T Get(int id);

        bool Exists(int id);
    }
}
