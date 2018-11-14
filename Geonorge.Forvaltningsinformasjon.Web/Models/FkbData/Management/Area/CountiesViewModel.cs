using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area
{
    public class CountiesViewModel
    {
        public List<ICounty> Counties { get; set; }

        public int DirectUpdateCount { get; set; }
    }
}
