using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Services.Authorization
{
    public class GeonorgeClaims
    {
        public const string Name = "Name";
        public const string Email = "Email";
        public const string AuthorizedFrom = "AuthorizedFrom";
        public const string AuthorizedUntil = "AuthorizedUntil";
        public const string OrganizationName = "OrganizationName";
        public const string OrganizationOrgnr = "OrganizationOrgnr";
        public const string OrganizationContactName = "OrganizationContactName";
        public const string OrganizationContactEmail = "OrganizationContactEmail";
        public const string OrganizationContactPhone = "OrganizationContactPhone";
        public const string IdToken = "id_token";
        public const string AccessToken = "access_token";
    }
}
