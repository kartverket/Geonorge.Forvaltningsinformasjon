using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface ITransactionDataStore : IEntities<ITransactionData>
    {
        List<ITransactionData> GetByCounty(int id);
        List<ITransactionData> GetByMunicipality(int id);
    }
}
