namespace Geonorge.Forvaltningsinformasjon.Models
{
    public class ApplicationSettings
    {
        public string BuildVersionNumber { get; set; }
        public string EnvironmentName { get; set; }
        public string UrlGeonorgeRoot { get; set; }
        public string UrlOperationalStatus { get; set; }
        public string UrlProxy { get; set; }
        public string UserNameProxy { get; set; }
        public string PasswordProxy { get; set; }
        public string CredentialVerificationDomain { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string KOS { get; set; }
    }
}