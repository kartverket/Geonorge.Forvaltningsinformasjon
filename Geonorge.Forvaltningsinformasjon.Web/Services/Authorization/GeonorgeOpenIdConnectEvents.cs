using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;

namespace Geonorge.Forvaltningsinformasjon.Utils
{
    /// <summary>
    /// Custom event handler for authorization in Geonorge
    /// </summary>
    public class GeonorgeOpenIdConnectEvents : OpenIdConnectEvents
    {
        /// <summary>
        /// Appends claims from Geonorge's authorization service
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public override async Task TokenValidated(TokenValidatedContext ctx)
        {
            var authorizationService =
                ctx.HttpContext.RequestServices.GetRequiredService<IGeonorgeAuthorizationService>();

            if (ctx.Principal.Identity is ClaimsIdentity identity)
            {
                identity.AddClaims(await authorizationService.GetClaims(identity));

                if (ctx.SecurityToken != null)
                    identity.AddClaim(new Claim("access_token", ctx.SecurityToken.RawData));
            }
        }
    }
}