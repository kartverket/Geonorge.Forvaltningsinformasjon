using Geonorge.Forvaltningsinformasjon.Models;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace Geonorge.Forvaltningsinformasjon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationSettings = new ApplicationSettings();
            Configuration.Bind(applicationSettings);
            services.AddSingleton<ApplicationSettings>(applicationSettings);

            // register databases
            Infrastructure.StartupInitializer.InitializeDatabases(
                services,
                applicationSettings.ConnectionStrings.KOS,
                applicationSettings.ConnectionStrings.Georef);

            // register dependencies
            Infrastructure.StartupInitializer.InitializeDependencies(services);
            Core.StartupInitializer.InitializeDependencies(services);

            services.AddTransient<IContextViewModelHelper, ContextViewModelHelper>();

            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            ConfigureProxy(applicationSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var supportedCultures = new[]
            {
                new CultureInfo("nb-NO"),
                new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("nb-NO"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>()   // @TMP single-language solution
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void ConfigureProxy(ApplicationSettings settings)
        {
            if (!string.IsNullOrWhiteSpace(settings.UrlProxy))
            {
                WebProxy proxy = new WebProxy(settings.UrlProxy);

                if (!(string.IsNullOrWhiteSpace(settings.UserNameProxy) || string.IsNullOrWhiteSpace(settings.PasswordProxy)))
                {
                    NetworkCredential credentials = new NetworkCredential
                    {
                        UserName = settings.UserNameProxy,
                        Password = settings.PasswordProxy
                    };

                    if (!string.IsNullOrWhiteSpace(settings.CredentialVerificationDomain))
                    {
                        credentials.Domain = settings.CredentialVerificationDomain; 
                    }
                    proxy.Credentials = credentials;
                }
                else
                {
                    proxy.Credentials = CredentialCache.DefaultCredentials;
                }

                WebRequest.DefaultWebProxy = proxy;
            }
        }
    }
}
