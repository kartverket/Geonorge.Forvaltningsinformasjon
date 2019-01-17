using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence
{
    public interface IRepository
    {
        ICounties Counties { get; }
        IMunicipalities Municipalities { get; }
        IDataSets DataSets { get; }
        ITransactionDataStore TransactionData { get; }
    }
}
