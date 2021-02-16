using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services.Common
{
    class MapService : IMapService
    {
        private const string _AdminUnitsWmsServiceName = "adm_enheter2";
        
        private IAdministrativeUnitSldProvider _administrativeUnitSldProvider;

        protected IWmsUrlProvider _wmsUrlProvider;
        protected string _WmsServiceName = "";

        public MapService(
                        IWmsUrlProvider wmsUrlProvider,
                        IAdministrativeUnitSldProvider administrativeUnitSldProvider)
        {
            _wmsUrlProvider = wmsUrlProvider;
            _administrativeUnitSldProvider = administrativeUnitSldProvider;
        }
   
        public string GetAdminstrativeUnitsWmsUrl()
        {
            return _wmsUrlProvider.GetCapabilitiesUrl(_AdminUnitsWmsServiceName);
        }

        public string GetAdministrativeUnitSld()
        {
            return _administrativeUnitSldProvider.GetSld();
        }

        public string GetWmsUrl()
        {
            return _wmsUrlProvider.GetCapabilitiesUrl(_WmsServiceName);
        }

    }
}
