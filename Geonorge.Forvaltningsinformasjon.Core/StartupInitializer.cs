﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Geonorge.Forvaltningsinformasjon.Core
{
    public class StartupInitializer
    {
        public static void InitializeDepenencies(IServiceCollection services)
        {
            services.AddTransient<ICountyService, CountyService>();
            services.AddTransient<IMunicipalityService, MunicipalityService>();
            services.AddTransient<IDataSetService, DataSetService>();
            services.AddTransient<ITransactionDataService, TransactionDataService>();
        }
    }
}