using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData
{
    public interface ILayerStyle
    {
        string FillColor { get; set; }
        string FillOpacity { get; set; }
        string StrokeColor { get; set; }
        string StrokeWidth { get; set; }
    }
}
