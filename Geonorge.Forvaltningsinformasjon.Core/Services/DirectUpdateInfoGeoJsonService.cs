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

        public string GetFileName()
        {
            return _provider.GetFileName(_geonerator, _municipalityService.Get(), "25833");
        }

        public string GetFileNameCounty(int id)
        {
            return _provider.GetFileName(_geonerator, _municipalityService.GetByCounty(id), "25833", id);
        }

        public string GetFileNameByMunicipality(int id)
        {
            // we don't need this
            throw new NotImplementedException();
        }
    }
}
