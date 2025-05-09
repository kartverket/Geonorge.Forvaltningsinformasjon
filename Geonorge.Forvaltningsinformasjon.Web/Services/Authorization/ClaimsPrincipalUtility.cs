using System.Security.Claims;
using System.Security.Principal;

namespace Geonorge.Forvaltningsinformasjon.Services.Authorization
{
    public static class ClaimsPrincipalUtility
    {
        public static string GetCurrentUserOrganizationName(IPrincipal user)
        {
            if (user is ClaimsPrincipal principal)
            {
                return principal.GetOrganizationName();
            }

            return null;
        }

        public static string GetUsername(IPrincipal user)
        {
            if (user is ClaimsPrincipal principal)
            {
                return principal.GetUsername();
            }

            return null;
        }

        public static bool UserHasMetadataAdminRole(IPrincipal user)
        {
            if (user is ClaimsPrincipal principal)
            {
                return principal.IsInRole(GeonorgeRoles.MetadataAdmin);
            }
            return false;
        }

        public static bool UserHasMetadataEditorRole(IPrincipal user)
        {
            if (user is ClaimsPrincipal principal)
            {
                return principal.IsInRole(GeonorgeRoles.MetadataEditor);
            }
            return false;
        }
    }
}