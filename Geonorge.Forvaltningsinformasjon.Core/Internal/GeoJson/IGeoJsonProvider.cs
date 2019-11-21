using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Internal.GeoJson
{
    internal interface IGeoJsonProvider
    {
        string GetPath(IGeoJsonGenerator geoJsonGenerator, List<IMunicipality> municipalites, int id = 0);
    }
}
