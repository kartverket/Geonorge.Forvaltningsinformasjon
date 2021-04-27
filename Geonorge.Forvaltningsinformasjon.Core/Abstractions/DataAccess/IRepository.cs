using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IRepository
    {
        ICounties Counties { get; }
        IMunicipalities Municipalities { get; }
        IDataSets DataSets { get; }
        ITransactionDataStore TransactionData { get; }
        IDataQualityClassifications DataQualityClassifications { get; }
        IDataQualityDistributions DataQualityDistributions { get; }
        IDataAgeDistributions DataAgeDistributions { get; }
        IMappingProjects MappingProjects { get; }
    }
}
