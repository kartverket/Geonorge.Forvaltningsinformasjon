using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess
{
    public class Repository : IRepository
    {
        public ICounties Counties { get; }

        public IMunicipalities Municipalities { get; }

        public IDataSets DataSets { get; }

        public ITransactionDataStore TransactionData { get; }

        public IDataQualityClassifications DataQualityClassifications { get; }

        public IDataQualityDistributions DataQualityDistributions { get; }

        public IDataAgeDistributions DataAgeDistributions { get; }

        public IMappingProjects MappingProjects { get; }

        public IOffices Offices { get; }

        public Repository(
            ICounties counties, 
            IMunicipalities municipalities, 
            IDataSets datasets, 
            ITransactionDataStore transactionData,
            IDataQualityClassifications dataQualityClassifications,
            IDataAgeDistributions dataAgeDistributions,
            IDataQualityDistributions dataQualityDistributions,
            IMappingProjects mappingProjects,
            IOffices offices)
        {
            Counties = counties;
            Municipalities = municipalities;
            DataSets = datasets;
            TransactionData = transactionData;
            DataQualityClassifications = dataQualityClassifications;
            DataAgeDistributions = dataAgeDistributions;
            DataQualityDistributions = dataQualityDistributions;
            MappingProjects = mappingProjects;
            Offices = offices;
        }
    }
}       
