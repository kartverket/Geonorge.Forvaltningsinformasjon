using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData
{
    public interface ILegendItemStyle
    {
        string FillColor { get; set; }
        string FillOpacity { get; set; }
        string StrokeColor { get; set; }
        string StrokeWidth { get; set; }
    }
}
