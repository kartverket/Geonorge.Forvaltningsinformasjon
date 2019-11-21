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
        private const string _GeoJsonPathWeb = "dist/geojson";
        private static string _GeoJsonPathLocal = $"./wwwroot/{_GeoJsonPathWeb}";

        static GeoJsonProvider()
        {
            Directory.CreateDirectory(_GeoJsonPathLocal);
        }
        
        public string GetPath(IGeoJsonGenerator geoJsonGenerator, List<IMunicipality> municipalities, int id = 0)
        {
            string path = BuildPathLocal(geoJsonGenerator, id);

            if (MustGenerate(path))
            {
                string geoJson = geoJsonGenerator.Generate(municipalities);

                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(geoJson);
                }
            }
            return BuildPathWeb(geoJsonGenerator, id); ;
        }

        private bool MustGenerate(string path)
        {
            return !File.Exists(path) || File.GetLastWriteTime(path) < DateTime.Now.Date;
        }

        private string BuildPathLocal(IGeoJsonGenerator geoJsonGenerator, int id)
        {
            return $"{_GeoJsonPathLocal}/{geoJsonGenerator.Name}_{id}.geojson";
        }

        private string BuildPathWeb(IGeoJsonGenerator geoJsonGenerator, int id)
        {
            return $"{_GeoJsonPathWeb}/{geoJsonGenerator.Name}_{id}.geojson";
        }

    }
}
