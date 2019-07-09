using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.DirectUpdateInfo
{
    public class CountiesViewModel
    {
        public List<ICounty> Counties { get; set; }

        public int DirectUpdateCount { get; set; }
    }
}
