using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    internal class GeoJsonGenerator
    {
        public virtual string Name { get; }

        private string _municipalitiesGeoJsonUrl;

        protected GeoJsonGenerator(string municipalitiesGeoJsonUrl)
        {
            _municipalitiesGeoJsonUrl = municipalitiesGeoJsonUrl;
        }
            
        public string Generate(List<IMunicipality> municipalities, string coordinateSystem)
        {
            string geoJson = GetMunicipalitiesGeoJson(coordinateSystem);

            return MergeThematicData(geoJson, municipalities);
        }

        protected virtual string MergeThematicData(string geoJson, List<IMunicipality> municipalities)
        {
            return geoJson;
        }

        private string GetMunicipalitiesGeoJson(string coordinateSystem)
        {
            string geoJson;

            if (!string.IsNullOrWhiteSpace(_municipalitiesGeoJsonUrl))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    geoJson = httpClient.GetStringAsync(string.Format(_municipalitiesGeoJsonUrl, coordinateSystem)).Result;
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
