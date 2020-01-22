using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class DataQualityClassification
    {
        public int Id { get; set; }
        public string MunicipalityNumber { get; set; }
        public double AreaA { get; set; }
        public double AreaB { get; set; }
        public double AreaC { get; set; }
        public double AreaD { get; set; }
    }
}
