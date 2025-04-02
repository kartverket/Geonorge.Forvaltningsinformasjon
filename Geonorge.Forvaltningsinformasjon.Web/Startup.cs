using Geonorge.Forvaltningsinformasjon.Infrastructure;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS;
using Geonorge.Forvaltningsinformasjon.Utils;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;

namespace Geonorge.Forvaltningsinformasjon.Web
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

            // init infrastructure
            InfrastructureSettings infrastructureSettings = new InfrastructureSettings
            {
                KosConnectionString = applicationSettings.ConnectionStrings.KOS,
                MunicipalitiesGeoJsonUrl = applicationSettings.ExternalUrls.MunicipalitiesGeoJson,
                DataSetToLayerMap = applicationSettings.DataSetToLayerMap,
                DataAgeDataSetToLayerMap = applicationSettings.DataAgeDataSetToLayerMap,
                DataQualityDataSetToLayerMap = applicationSettings.DataQualityDataSetToLayerMap,
                WmsUrlBase = applicationSettings.Wms.UrlBase,
                WmsVersion = applicationSettings.Wms.Version
            };

            Infrastructure.StartupInitializer.Initialize(services, infrastructureSettings);

            // init core
            Core.StartupInitializer.InitializeDependencies(services);
            Core.StartupInitializer.LocalPathThematicGeoJson = applicationSettings.LocalPathThematicGeoJson;

            // init web
            services.AddTransient<IContextViewModelHelper, ContextViewModelHelper>();
            services.AddHttpClient();

            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddDbContext<KosContext>(item => item.UseSqlServer(Configuration.GetConnectionString("KOS")));

            services.AddScoped<GeonorgeOpenIdConnectEvents>();
            services.AddTransient<IGeonorgeAuthorizationService, GeonorgeAuthorizationService>();
            services.AddTransient<IBaatAuthzApi, BaatAuthzApi>();

            services
             .AddAuthentication(options => {
                 options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

             })
             .AddCookie()
             .AddOpenIdConnect(options =>
             {
                 options.TokenValidationParameters.ValidIssuer = Configuration["auth:oidc:issuer"];
                 options.Authority = Configuration["auth:oidc:authority"];
                 options.ClientId = Configuration["auth:oidc:clientid"];
                 options.ClientSecret = Configuration["auth:oidc:clientsecret"];
                 options.MetadataAddress = Configuration["auth:oidc:metadataaddress"];
                 options.ResponseType = OpenIdConnectResponseType.CodeIdTokenToken;
                 options.SignedOutRedirectUri = Configuration["PostLogoutRedirectUri"];
                 options.SaveTokens = true;
                 options.EventsType = typeof(GeonorgeOpenIdConnectEvents);
             })
             .AddJwtBearer(options =>
             {
                 options.Authority = Configuration["auth:oidc:authority"];
                 options.Audience = Configuration["auth:oidc:clientid"];
                 options.MetadataAddress = Configuration["auth:oidc:metadataaddress"];
             });

            // authorize both via cookies and jwt bearer tokens
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    CookieAuthenticationDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            ConfigureProxy(applicationSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
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

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(e =>
            {
                e.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
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
                HttpClient.DefaultProxy = proxy;
            }
        }
    }
}
