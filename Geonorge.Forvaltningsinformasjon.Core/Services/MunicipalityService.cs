using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class MunicipalityService : IMunicipalityService
    {
        private IRepository _repository;

        public MunicipalityService(IRepository repository)
        {
            _repository = repository;
        }

        public List<IMunicipality> Get()
        {
            return _repository.Municipalities.Get();
        }

        public IMunicipality Get(int id)
        {
            return _repository.Municipalities.Get(id);
        }

        public List<IMunicipality> GetByCounty(int id)
        {
            return _repository.Municipalities.GetByCounty(id);
        }
    }
}
