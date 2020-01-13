using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IDataAgeDistribution : IEntityBase
    {
        string DataSetName { get; }
        long Year0 { get; }
        long Year1 { get; }
        long Year2 { get; }
        long Year3 { get; }
        long Year4 { get; }
        long Years5To9 { get; }
        long Years10To19 { get; }
        long Older { get; }
    }
}
