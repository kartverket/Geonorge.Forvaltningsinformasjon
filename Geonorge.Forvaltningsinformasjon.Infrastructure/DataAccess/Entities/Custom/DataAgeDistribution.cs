using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom
{
    internal class DataAgeDistribution : IDataAgeDistribution
    {
        public int Id { get; set; }
        public int? Active { get; set; } = 1;
        public string DataSetName { get; set; }
        public int ObjectCount { get; set; }
        public Dictionary<AgeCategory, long> TransactionCounts { get; } = new Dictionary<AgeCategory, long>();
    }
}
