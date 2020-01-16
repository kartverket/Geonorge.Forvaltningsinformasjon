﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom
{
    public class DataQualityDistribution : IDataQualityDistribution
    {
        public int Id { get; set; }
        public string DataSetName { get; set; }

        public Dictionary<QualityCategory, long> TransactionCounts { get; set; } = new Dictionary<QualityCategory, long>();
    }
}
