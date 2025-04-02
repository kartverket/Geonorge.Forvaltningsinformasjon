using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web
{
    public class ApplicationSettings
    {
        public string BuildVersionNumber { get; set; }
        public string EnvironmentName { get; set; }
        public string UrlGeonorgeRoot { get; set; }
        public string UrlThematicGeoJson { get; set; }
        public string LocalPathThematicGeoJson { get; set; }
        public string UrlProxy { get; set; }
        public string UserNameProxy { get; set; }
        public string PasswordProxy { get; set; }
        public string CredentialVerificationDomain { get; set; }

        public ExternalUrls ExternalUrls { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }

        public Wms Wms { get; set; }       
        public Dictionary<string,string> DataSetToLayerMap { get; set; }
        public Dictionary<string, string> DataAgeDataSetToLayerMap { get; set; }
        public Dictionary<string, string> DataQualityDataSetToLayerMap { get; set; }
        public Dictionary<string,string> AgeCategoryColors { get; set; }
        public Dictionary<string, string> QualityCategoryColors { get; set; }
        public ChartLegendSettings ChartLegendSettings { get; set; }

        public Urls Urls { get; set; }
        public string BaatAuthzApiCredentials { get; set; }
        public string RedirectUri { get; set; }
        public string PostLogoutRedirectUri { get; set; }

        public auth auth { get; set; }
    }

    public class Urls
    {
        public string BaatAuthzApi { get; set; }
    }

    public class auth
    {
        public oidc oidc { get; set; }
    }

    public class oidc
    {
        public string clientid { get; set; }
        public string clientsecret { get; set; }
        public string IntrospectionUrl { get; set; }
    }

    public class Wms
    {
        public string UrlBase { get; set; }
        public string Version { get; set; }
    }

    public class ConnectionStrings
    {
        public string KOS { get; set; }
    }

    public class ChartLegendSettings 
    {
        public string TitleTextColor { get; set; }
        public string BodyTextColor { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderWidth { get; set; }
    }

    public class ExternalUrls
    {
        public string OperationalStatus { get; set; }
        public string MunicipalitiesGeoJson { get; set; }
        public string MetadataTransactionData { get; set; }
        public string MetadataDataQualityDistribution { get; set; }
        public string MetadataDataAgeDistribution { get; set; }
        public string MetadataDataQualityClassification { get; set; }
    }
}