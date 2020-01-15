using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom
{
    internal class DataAgeDistribution : IDataAgeDistribution
    {
        public int Id { get; set; }

        public string DataSetName { get; set; }
        public Dictionary<AgeCategory, long> TransactionCounts { get; } = new Dictionary<AgeCategory, long>();
    }
}
