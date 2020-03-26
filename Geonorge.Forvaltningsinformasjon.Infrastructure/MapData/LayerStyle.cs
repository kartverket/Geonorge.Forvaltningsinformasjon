using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    internal class LayerStyle : ILayerStyle
    {
        public string FillColor { get; set; }
        public string FillOpacity { get; set; }
        public string StrokeColor { get; set; }
        public string StrokeWidth { get; set; }
    }
}
