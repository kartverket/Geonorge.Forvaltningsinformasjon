using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Utils
{
    public interface IGeonorgeAuthorizationService
    {
        /// <summary>
        ///     Return Claims for the given user
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<List<Claim>> GetClaims(ClaimsIdentity identity);
        Task<string> GetUserNameFromIntrospection(string token);
    }
}