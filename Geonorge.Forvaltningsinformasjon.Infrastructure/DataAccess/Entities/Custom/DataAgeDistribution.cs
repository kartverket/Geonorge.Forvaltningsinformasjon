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

        public long Year0 { get; set; }
        public long Year1 { get; set; }
        public long Year2 { get; set; }
        public long Year3 { get; set; }
        public long Year4 { get; set; }
        public long Years5To9 { get; set; }
        public long Years10To19 { get; set; }
        public long Older { get; set; }
    }
}
