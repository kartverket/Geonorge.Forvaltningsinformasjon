using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IEntityServiceBase<T>
    {
        List<T> Get();
        T Get(int id);
    }
}
