using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Internal.GeoJson
{
    internal class GeoJsonProvider : IGeoJsonProvider
    {
        private static string _GeoJsonPathLocal = StartupInitializer.LocalPathThematicGeoJson;

        static GeoJsonProvider()
        {
            Directory.CreateDirectory(_GeoJsonPathLocal);
        }
        
        public string GetFileName(IGeoJsonGenerator geoJsonGenerator, List<IMunicipality> municipalities, string coordinateSystem, int id = 0)
        {
            string path = BuildPathLocal(geoJsonGenerator, id);

            if (MustGenerate(path))
            {
                string geoJson = geoJsonGenerator.Generate(municipalities, coordinateSystem);

                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(geoJson);
                }
            }
            return GetFileName(geoJsonGenerator, id); ;
        }

        private bool MustGenerate(string path)
        {
            return !File.Exists(path) || File.GetLastWriteTime(path) < DateTime.Now.Date;
        }

        private string BuildPathLocal(IGeoJsonGenerator geoJsonGenerator, int id)
        {
            return $"{_GeoJsonPathLocal}/{geoJsonGenerator.Name}_{id}.geojson";
        }

        private string GetFileName(IGeoJsonGenerator geoJsonGenerator, int id)
        {
            return $"{geoJsonGenerator.Name}_{id}.geojson";
        }
    }
}
