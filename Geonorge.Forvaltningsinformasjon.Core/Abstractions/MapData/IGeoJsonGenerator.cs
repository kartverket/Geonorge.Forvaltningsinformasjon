using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData
{
    public interface IGeoJsonGenerator
    {
        string Name { get; }
        string Generate(List<IMunicipality> municipalities, string coordinateSystem);
    }
}
