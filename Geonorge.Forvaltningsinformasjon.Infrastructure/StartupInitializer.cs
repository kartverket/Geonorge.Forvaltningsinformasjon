using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections.KOS;
using Geonorge.Forvaltningsinformasjon.Infrastructure.MapData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure
{
    public class StartupInitializer
    {
        public static void Initialize(
            IServiceCollection services,
            InfrastructureSettings infrastructureSettings)
        {
            services.AddDbContext<KosContext>(options =>
            options.UseSqlServer(infrastructureSettings.KosConnectionString));

            services.AddSingleton<InfrastructureSettings>(infrastructureSettings);

            // data access
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ICounties, Counties>();
            services.AddTransient<IMunicipalities, Municipalities>();
            services.AddTransient<IDataSets, DataSets>();
            services.AddTransient<ITransactionDataStore, TransactionDataStore>();
            services.AddTransient<IDataQualityClassifications, DataQualityClassifications>();
            services.AddTransient<IDataAgeDistributions, DataAgeDistributions>();
            services.AddTransient<IDataQualityDistributions, DataQualityDistributions>();
            services.AddTransient<IMappingProjects, MappingProjects>();

            // map data
            services.AddTransient<IDirectUpdateInfoGeoJsonGenerator, DirectUpdateInfoGeoJsonGenerator>();
            services.AddTransient<ITransactionDataSldProvider, TransactionDataSldProvider>();
            services.AddTransient<IDataQualityClassificationSldProvider, DataQualityClassificationSldProvider>();
            services.AddTransient<IAdministrativeUnitSldProvider, AdministrativeUnitSldProvider>();
            services.AddTransient<IWmsUrlProvider, WmsUrlProvider>();
        }
    }
}