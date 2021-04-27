using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Core.Internal.GeoJson;
using Geonorge.Forvaltningsinformasjon.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Geonorge.Forvaltningsinformasjon.Core
{
    public class StartupInitializer
    {
        public static string LocalPathThematicGeoJson { get; set; }
        public static void InitializeDependencies(IServiceCollection services)
        {
            // services
            services.AddTransient<ICountyService, CountyService>();
            services.AddTransient<IMunicipalityService, MunicipalityService>();
            services.AddTransient<IDataSetService, DataSetService>();
            services.AddTransient<ITransactionDataService, TransactionDataService>();
            services.AddTransient<IDataQualityClassificationService, DataQualityClassificationService>();
            services.AddTransient<IDataAgeDistributionService, DataAgeDistributionService>();
            services.AddTransient<IDataQualityDistributionService, DataQualityDistributionService>();
            services.AddTransient<IDirectUpdateInfoGeoJsonService, DirectUpdateInfoGeoJsonService>();
            services.AddTransient<IMappingProjectService, MappingProjectService>();

            // internals
            services.AddTransient<IGeoJsonProvider, GeoJsonProvider>();
        }
    }
}
