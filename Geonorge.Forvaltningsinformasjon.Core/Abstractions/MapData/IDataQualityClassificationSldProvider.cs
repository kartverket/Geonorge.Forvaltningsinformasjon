using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData
{
    public interface IDataQualityClassificationSldProvider
    {
        string GetSld();
        Dictionary<string, ILegendItemStyle> GetLegendItemStyles();
    }
}
