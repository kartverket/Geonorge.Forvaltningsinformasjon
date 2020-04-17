using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData
{
    public interface IWmsUrlProvider
    {
        string GetCapabilitiesUrl(string serviceName);
        string GetLegendGraphicsUrl(string serviceName, string layerName, string format, int width, int height);
        string GetStyledLayerDescritpionUrl(string serviceName, List<string> LayerNames = null);
    }
}
