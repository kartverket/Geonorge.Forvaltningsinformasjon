using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface ITransactionDataService : IEntityServiceBase<ITransactionData>
    {
        List<ITransactionData> GetByCounty(int id);
        List<ITransactionData> GetByMunicipality(int id);
        Dictionary<string, ILegendItemStyle> GetLayerStyles(List<ITransactionData> transactionData);
        string GetWmsUrl();
        string GetAdminstrativeUnitsWmsUrl();
        string GetAdministrativeUnitSld();
    }
}
