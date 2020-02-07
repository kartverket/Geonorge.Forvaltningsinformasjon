using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom
{
    internal class DataQualityClassification : IDataQualityClassification
    {
        #region IDataQualityClassification
        public int Id { get; set; }
        public int? Active { get; set; } = 1;
        public string Class { get; set; }
        public double Area { get; set; }
        #endregion
    }
}
