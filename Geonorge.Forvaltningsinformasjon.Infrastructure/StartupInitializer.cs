using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections;
using Microsoft.Extensions.DependencyInjection;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure
{
    public class StartupInitializer
    {
        public static void InitializeDependencies(IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ICounties, Counties>();
            services.AddTransient<IMunicipalities, Municipalities>();
            services.AddTransient<IDataSets, DataSets>();
            services.AddTransient<ITransactionDataStore, TransactionDataStore>();
            services.AddTransient<IDataQualityClassifications, DataQualityClassifications>();
            services.AddTransient<IDataAgeDistributions, DataAgeDistributions>();
            services.AddTransient<IDataQualityDistributions, DataQualityDistributions>();
        }
    }
}