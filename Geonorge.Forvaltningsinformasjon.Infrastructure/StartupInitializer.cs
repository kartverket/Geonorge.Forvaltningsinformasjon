using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.DataSets;
using Microsoft.Extensions.DependencyInjection;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure
{
    public class StartupInitializer
    {
        public static void InitializeDepenencies(IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ICountyDataSet, CountyDataSet>();
            services.AddTransient<IMunicipalityDataSet, MunicipalityDataSet>();
        }
    }
}