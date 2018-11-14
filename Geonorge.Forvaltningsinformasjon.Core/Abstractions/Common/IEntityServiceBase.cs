using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Common
{
    public interface IEntityServiceBase<T>
    {
        List<T> GetAll();
    }
}
