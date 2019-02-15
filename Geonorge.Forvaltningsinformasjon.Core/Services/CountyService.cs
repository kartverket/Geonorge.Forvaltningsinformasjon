using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using System.Collections.Generic;
using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class CountyService : ICountyService
    {
        private IRepository _repository;

        public CountyService(IRepository repository)
        {
            _repository = repository;
        }

        public List<ICounty> Get()
        {
            return _repository.Counties.Get();
        }

        public ICounty Get(int id)
        {
            return _repository.Counties.Get(id);
        }
    }
}
