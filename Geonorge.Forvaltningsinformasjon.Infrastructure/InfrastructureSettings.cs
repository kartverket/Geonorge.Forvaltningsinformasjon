using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure
{
    public class InfrastructureSettings
    {
        public string KosConnectionString { get; set; }
        public Dictionary<string, string> DataSetToLayerMap { get; set; }
        public string MunicipalitiesGeoJsonUrl { get; set; }
        public string WmsUrlBase { get; set; }
        public string WmsVersion { get; set; }
    }
}
