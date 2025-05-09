using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Geonorge.Forvaltningsinformasjon.Services.Authorization
{
    public static class PrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        }

        public static string GetUserFullName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(GeonorgeClaims.Name);
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(GeonorgeClaims.Email);
        }

        public static string GetOrganizationName(this ClaimsPrincipal principal)
        {
                return principal.FindFirstValue(GeonorgeClaims.OrganizationName);
        }

        public static string GetOrganizationOrgnr(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(GeonorgeClaims.OrganizationOrgnr);
        }

        public static bool IsAuthenticated(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal?.Identities != null && principal.Identities.Any(i => i.IsAuthenticated);
        }

        /// https://github.com/aspnet/Identity/blob/ab43154577fe126ef531bb7a11b5eaa03add7bbf/src/Microsoft.AspNet.Identity/PrincipalExtensions.cs#L65
        /// <summary>
        /// Returns the value for the first claim of the specified type otherwise null the claim is not present.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentity"/> instance this method extends.</param>
        /// <param name="claimType">The claim type whose first value should be returned.</param>
        /// <returns>The value of the first instance of the specifed claim type, or null if the claim is not present.</returns>
        public static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(claimType);
            return claim != null ? claim.Value : null;
        }
    }
}