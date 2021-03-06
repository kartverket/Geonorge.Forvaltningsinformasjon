﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Internal.GeoJson
{
    internal interface IGeoJsonProvider
    {
        string GetFileName(IGeoJsonGenerator geoJsonGenerator, List<IMunicipality> municipalites, string coordinateSystem, int id = 0);
    }
}
