using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData
{
    public interface ITransactionDataSldProvider
    {
        Dictionary<string, ILayerStyle> GetLayerStyles(List<ITransactionData> transactionData);
    }
}
