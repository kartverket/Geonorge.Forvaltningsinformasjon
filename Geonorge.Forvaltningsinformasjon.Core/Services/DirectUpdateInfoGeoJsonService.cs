using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Core.Internal.GeoJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class DirectUpdateInfoGeoJsonService : IDirectUpdateInfoGeoJsonService
    {
        private IGeoJsonProvider _provider;
        private IDirectUpdateInfoGeoJsonGenerator _geonerator;
        private IMunicipalityService _municipalityService;

        public DirectUpdateInfoGeoJsonService(
                    IGeoJsonProvider provider, 
                    IDirectUpdateInfoGeoJsonGenerator geonerator,
                    IMunicipalityService municipalityService)
        {
            _provider = provider;
            _geonerator = geonerator;
            _municipalityService = municipalityService;
        }

        public string GetPath()
        {
            return _provider.GetPath(_geonerator, _municipalityService.Get());
        }

        public string GetPathByCounty(int id)
        {
            return _provider.GetPath(_geonerator, _municipalityService.GetByCounty(id), id);
        }

        public string GetPathByMunicipality(int id)
        {
            // we don't need this
            throw new NotImplementedException();
        }
    }
}
