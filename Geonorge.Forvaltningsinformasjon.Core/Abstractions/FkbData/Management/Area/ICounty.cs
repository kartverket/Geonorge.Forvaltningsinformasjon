using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area
{
    public interface ICounty
    {
        string Number { get; set; }
        string Name { get; set; }
        int MunicipalityCount { get; set; }
        int DirectUpdateCount { get; set; }
    }
}
