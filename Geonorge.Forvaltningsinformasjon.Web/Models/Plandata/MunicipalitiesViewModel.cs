using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.Plandata
{
    public class MunicipalitiesViewModel
    {
        public List<GeosynchInfo> Municipalities { get; set; }
        public int CountGeosynch { get; set; }
        public int Count { get; set; }
        public string CountyName { get; set; }
    }

    public class GeosynchInfo
    {
        public string MunicipalityNumber { get; set; }
        public string MunicipalityName { get; set; }
        public string UpdateType { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
    }
}
