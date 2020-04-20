using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IDataQualityDistribution : IEntityBase
    {
        string DataSetName { get; }
        Dictionary<QualityCategory, long> TransactionCounts { get; }
        int ObjectCount { get; set; }
    }
}
