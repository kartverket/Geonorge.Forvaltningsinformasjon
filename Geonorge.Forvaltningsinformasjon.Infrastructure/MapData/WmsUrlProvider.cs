using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    using QueryStringParams = Dictionary<string, string>;

    internal class WmsUrlProvider : IWmsUrlProvider
    {
        private string _urlFormatString;
        private QueryStringParams _commonParams = new QueryStringParams();

        public WmsUrlProvider(InfrastructureSettings settings)
        {
            _commonParams.Add("service", "wms");
            _commonParams.Add("version", settings.WmsVersion);
            _urlFormatString = $"{settings.WmsUrlBase}{{0}}?{{1}}";
        }

        public string GetCapabilitiesUrl(string serviceName)
        {
            return string.Format(_urlFormatString, serviceName, "");
        }

        public string GetLegendGraphicsUrl(string serviceName, string layerName, string format, int width, int height)
        {
            QueryStringParams parameters = new QueryStringParams()
            {
                { "request", "GetLegendGraphic" },
                { "layer", layerName },
                { "format", format },
                { "width", width.ToString() },
                { "height", height.ToString() }
            };

            return string.Format(_urlFormatString, serviceName, GetQueryString(parameters));
        }

        public string GetStyledLayerDescritpionUrl(string serviceName, List<string> layerNames = null)
        {
            QueryStringParams parameters = new QueryStringParams()
            {
                { "request", "GetStyles" }
            };

            if (layerNames != null && layerNames.Count > 0)
            {
                parameters.Add("layers", string.Join(',', layerNames));
            }
            return string.Format(_urlFormatString, serviceName, GetQueryString(parameters));
        }

        private string GetQueryString(Dictionary<string,string> parameters = null)
        {
            string queryString = string.Join('&', _commonParams.Select(p => $"{p.Key}={p.Value}"));

            if (parameters != null && parameters.Count > 0)
            {
                queryString += "&" + string.Join('&', parameters.Select(p => $"{p.Key}={p.Value}"));
            }
            return queryString;
        }
    }
}
