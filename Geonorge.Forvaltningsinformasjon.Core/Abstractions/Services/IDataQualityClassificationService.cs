using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IDataQualityClassificationService : IEntityServiceBase<IDataQualityClassification>
    {
        List<IDataQualityClassification> GetByCounty(int id);
        List<IDataQualityClassification> GetByMunicipality(int id);
        string GetSld();
        string GetLegendUrl(string format, int width, int height);
        Dictionary<string, ILegendItemStyle> GetLegendItemStyles();
        string GetWmsUrl();
        string GetAdminstrativeUnitsWmsUrl();
        string GetAdministrativeUnitSld();
    }
}
