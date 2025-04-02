using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Geonorge.Forvaltningsinformasjon.Web;
using Geonorge.Forvaltningsinformasjon.Services.Authorization;
using Newtonsoft.Json;
using Serilog;

namespace Geonorge.Forvaltningsinformasjon.Utils
{
    public interface IBaatAuthzApi
    {
        /// <summary>
        /// returns information about a user, e.g. name and organization
        /// </summary>
        /// <param name="username"></param>
        Task<BaatAuthzUserInfoResponse> Info(string username);

        Task<BaatAuthzUserRolesResponse> GetRoles(string username);
    }

    /// <summary>
    /// Implementation of the BaatAuthzApi. Returns authorization information about BAAT users. 
    /// </summary>
    public class BaatAuthzApi : IBaatAuthzApi
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _apiUrl;
        private readonly string _apiCredentials;
        private readonly IHttpClientFactory _httpClientFactory;
        
        public BaatAuthzApi(ApplicationSettings applicationSettings, IHttpClientFactory httpClientFactory)
        {
            _apiUrl = applicationSettings.Urls.BaatAuthzApi;
            _apiCredentials = applicationSettings.BaatAuthzApiCredentials;
            _httpClientFactory = httpClientFactory;
        }
        
        /// <summary>
        /// Returns information about a user, e.g. name and organization
        /// </summary>
        /// <param name="username"></param>
        public async Task<BaatAuthzUserInfoResponse> Info(string username)
        {
            var url = $"{_apiUrl}authzinfo/{username}";
            Log.Debug("Fetching data from {url}", url);
            
            var res = await GetClient().GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                Log.Error("Looking up {user} from BaatAuthzApi failed with status code: {code}", username, res.StatusCode);
                return BaatAuthzUserInfoResponse.Empty;
            }
            
            var json = await res.Content.ReadAsStringAsync();
            
            Log.Debug("Response from BaatAuthzApi: {json}", json);
            
            return JsonConvert.DeserializeObject<BaatAuthzUserInfoResponse>(json);
        
        }

        public async Task<BaatAuthzUserRolesResponse> GetRoles(string username)
        {
            var url = $"{_apiUrl}authzlist/{username}";
            Log.Debug("Fetching data from {url}", url);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var res = await GetClient().GetAsync(url);
            stopwatch.Stop();

            Log.Information("Http call to {url} with response code {statuscode} executed in {millis}", url, res.StatusCode, stopwatch.ElapsedMilliseconds);

            if (!res.IsSuccessStatusCode)
            {
                Log.Error("Looking up {user} from BaatAuthzApi failed with status code: {code}", username, res.StatusCode);
                return BaatAuthzUserRolesResponse.Empty;
            }

            var json = await res.Content.ReadAsStringAsync();

            Log.Debug("Response from BaatAuthzApi: {json}", json);

            if (json.Contains("\"services\": false"))
                json = json.Replace("\"services\": false", "\"services\": \"\"");

            return JsonConvert.DeserializeObject<BaatAuthzUserRolesResponse>(json);

        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            
            Log.Debug("Connecting to BaatAuthzApi with credentials: {credentials}", _apiCredentials);
            
            var byteArray = Encoding.ASCII.GetBytes(_apiCredentials);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }

    }
}