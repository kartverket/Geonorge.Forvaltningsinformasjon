using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management.Area
{
    class County : ICounty
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int MunicipalityCount { get; set; }
        public int DirectUpdateCount { get; set; }
    }
}
