using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.Plandata
{
    public class CountiesViewModel
    {
        public List<County> Counties { get; set; }

        public int GeosynchIntroducedCount { get; set; }
    }

    public class County
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int MunicipalityCount { get; set; }
        public int GeosynchIntroducedCount { get; set; }
    }
}
