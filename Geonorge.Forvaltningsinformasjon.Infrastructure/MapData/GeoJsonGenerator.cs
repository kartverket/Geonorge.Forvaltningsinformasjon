﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    internal class GeoJsonGenerator : IGeoJsonGenerator
    {
        public virtual string Name { get; }

        protected GeoJsonGenerator()
        {
        }
            
        public string Generate(List<IMunicipality> municipalities)
        {
            string geoJson = GetMunicipalitiesGeoJson();

            return MergeThematicData(geoJson, municipalities);
        }

        protected virtual string MergeThematicData(string geoJson, List<IMunicipality> municipalities)
        {
            return geoJson;
        }

        private string GetMunicipalitiesGeoJson()
        {
            string geoJson;

            if (!string.IsNullOrWhiteSpace(StartupInitializer.MunicipalitiesGeoJsonUrl))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    geoJson = httpClient.GetStringAsync(StartupInitializer.MunicipalitiesGeoJsonUrl).Result;
                }
            }
            else
            {
                // @TMP hack for dev / test env.
                geoJson = File.ReadAllText("./wwwroot/dist/geojson/kommuner.geojson");
                // @TMP_END
            }
            return geoJson;
        }
    }
}