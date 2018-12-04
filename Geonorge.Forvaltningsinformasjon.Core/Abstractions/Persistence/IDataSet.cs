using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence
{
    public interface IDataSet<T>
    {
        List<T> Get();

        T Get(string id);

        bool Exists(string id);
    }
}
