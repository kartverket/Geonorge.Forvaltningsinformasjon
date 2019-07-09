using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess
{
    public class Repository : IRepository
    {
        private FDV_Drift2Context _dbContext;

        public ICounties Counties { get; }

        public IMunicipalities Municipalities { get; }

        public IDataSets DataSets { get; }

        public ITransactionDataStore TransactionData { get; }

        public IDataQualityClassifications DataQualityClassifications { get; }

        public IDataQualityDistributions DataQualityDistributions { get; }

        public IDataAgeDistributions DataAgeDistributions { get; }

        public Repository(
            FDV_Drift2Context dbContext, 
            ICounties counties, 
            IMunicipalities municipalities, 
            IDataSets datasets, 
            ITransactionDataStore transactionData,
            IDataQualityClassifications dataQualityClassifications,
            IDataAgeDistributions dataAgeDistributions,
            IDataQualityDistributions dataQualityDistributions)
        {
            _dbContext = dbContext;
            Counties = counties;
            Municipalities = municipalities;
            DataSets = datasets;
            TransactionData = transactionData;
            DataQualityClassifications = dataQualityClassifications;
            DataAgeDistributions = dataAgeDistributions;
            DataQualityDistributions = dataQualityDistributions;
        }
    }
}       
