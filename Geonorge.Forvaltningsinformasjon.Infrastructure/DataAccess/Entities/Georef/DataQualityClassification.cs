using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Georef
{
    internal class DataQualityClassification : IDataQualityClassification
    {
        #region IDataQualityClassification
        public string Class { get; set; }
        public double Area { get; set; }
        #endregion

        public int Id { get; set; }
        public string MunicipalityNumber { get; set; }
        public string MunicipalityName { get; set; }
    }
}
