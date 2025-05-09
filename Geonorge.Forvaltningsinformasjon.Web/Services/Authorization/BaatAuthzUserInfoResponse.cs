using Newtonsoft.Json;

namespace Geonorge.Forvaltningsinformasjon.Utils
{
    public class BaatAuthzUserInfoResponse
    {
        public static readonly BaatAuthzUserInfoResponse Empty = new BaatAuthzUserInfoResponse();
        
        public string User { get; set; }
        
        public BaatAuthzUserInfoOrganization Organization { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        [JsonProperty("authorized_from")]
        public string AuthorizedFrom { get; set; }
        
        [JsonProperty("authorized_until")]
        public string AuthorizedUntil { get; set; }
    }
    
    public class BaatAuthzUserInfoOrganization {
        public string Name { get; set; }
        
        public string Orgnr { get; set; }
        
        [JsonProperty("contact_name")]
        public string ContactName { get; set; }
        
        [JsonProperty("contact_email")]
        public string ContactEmail { get; set; }
        
        [JsonProperty("contact_phone")]
        public string ContactPhone { get; set; }
    }
}