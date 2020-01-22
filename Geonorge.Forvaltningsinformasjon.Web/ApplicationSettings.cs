using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web
{
    public class ApplicationSettings
    {
        public string BuildVersionNumber { get; set; }
        public string EnvironmentName { get; set; }
        public string UrlGeonorgeRoot { get; set; }
        public string UrlOperationalStatus { get; set; }
        public string UrlMunicipalitiesGeoJson { get; set; }
        public string UrlThematicGeoJson { get; set; }
        public string LocalPathThematicGeoJson { get; set; }
        public string UrlProxy { get; set; }
        public string UserNameProxy { get; set; }
        public string PasswordProxy { get; set; }
        public string CredentialVerificationDomain { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
        public Dictionary<string,string> DataSetToLayerMap { get; set; }
        public Dictionary<string,string> AgeCategoryColors { get; set; }
        public Dictionary<string, string> QualityCategoryColors { get; set; }
    }

    public class ConnectionStrings
    {
        public string KOS { get; set; }
    }
}