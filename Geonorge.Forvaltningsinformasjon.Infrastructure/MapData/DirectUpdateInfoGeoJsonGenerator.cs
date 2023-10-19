using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    internal class DirectUpdateInfoGeoJsonGenerator : GeoJsonGenerator, IDirectUpdateInfoGeoJsonGenerator
    {
        public override string Name
        {
            get
            {
                return "DirectUpdateInfo";
            }
        }
        public DirectUpdateInfoGeoJsonGenerator(InfrastructureSettings settings) : base(settings.MunicipalitiesGeoJsonUrl)
        {
            
        }

        protected override string MergeThematicData(string geoJson, List<IMunicipality> municipalities)
        {
            JObject originalModel = JObject.Parse(geoJson);
            JArray features = new JArray();

            foreach (IMunicipality municipality in municipalities)
            {
                JToken feature = (JToken)(originalModel["features"].Where(f => f["properties"]["kommunenummer"]?.ToString() == municipality.Number).FirstOrDefault());
                if(feature != null) { 
                ((JObject)feature["properties"]).Add("IntroductionState", (int)municipality.IntroductionState);
                
                features.Add(feature);
                }
            }

            JObject mergedModel = new JObject(
                    new JProperty("features", features),
                    new JProperty("type", "FeatureCollection"),
                    new JProperty("name", "sFkbStatus"));

            return JsonConvert.SerializeObject(mergedModel);
        }
    }
}
