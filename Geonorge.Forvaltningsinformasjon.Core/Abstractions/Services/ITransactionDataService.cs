using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface ITransactionDataService : IEntityServiceBase<ITransactionData>, IMapService
    {
        List<ITransactionData> GetByCounty(int id);
        List<ITransactionData> GetByMunicipality(int id);
        Dictionary<string, ILegendItemStyle> GetLayerStyles(List<ITransactionData> transactionData);
    }
}
