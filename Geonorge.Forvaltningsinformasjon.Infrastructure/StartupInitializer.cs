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
        public static string MunicipalitiesGeoJsonUrl {get;set;}
        public static void InitializeDatabases(
            IServiceCollection services,
            string connStrKOS)
        {
            services.AddDbContext<KosContext>(options =>
                options.UseSqlServer(connStrKOS));

        }
        public static void InitializeDependencies(IServiceCollection services)
        {
            // data access
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ICounties, Counties>();
            services.AddTransient<IMunicipalities, Municipalities>();
            services.AddTransient<IDataSets, DataSets>();
            services.AddTransient<ITransactionDataStore, TransactionDataStore>();
            services.AddTransient<IDataQualityClassifications, DataQualityClassifications>();
            services.AddTransient<IDataAgeDistributions, DataAgeDistributions>();
            services.AddTransient<IDataQualityDistributions, DataQualityDistributions>();

            // map data
            services.AddTransient<IDirectUpdateInfoGeoJsonGenerator, DirectUpdateInfoGeoJsonGenerator>();
        }
    }
}