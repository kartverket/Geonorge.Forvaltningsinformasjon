using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;
using Geonorge.Forvaltningsinformasjon.Core.Services.FkbData.Management.Area;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core
{
    public class StartupInitializer
    {
        public static void InitializeDepenencies(IServiceCollection services)
        {
            services.AddTransient<ICountyService, CountyService>();
            services.AddTransient<IMunicipalityService, MunicipalityService>();
        }
    }
}
