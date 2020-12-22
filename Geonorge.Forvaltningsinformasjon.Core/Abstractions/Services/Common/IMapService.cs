using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services.Common
{
    public interface IMapService
    {
        string GetAdminstrativeUnitsWmsUrl();
        string GetAdministrativeUnitSld();
        string GetWmsUrl();
    }
}
