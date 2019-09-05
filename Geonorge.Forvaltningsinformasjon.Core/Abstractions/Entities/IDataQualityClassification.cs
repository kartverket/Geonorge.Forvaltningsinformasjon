using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IDataQualityClassification : IEntityBase
    {
        string Class { get; }
        double Area { get; }
    }
}
