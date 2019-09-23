using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IBoundingBox
    {
        int MinX { get; }
        int MinY { get; }
        int MaxX { get; }
        int MaxY { get; }
    }
}
