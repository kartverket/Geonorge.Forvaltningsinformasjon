using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class MappingProjectService : IMappingProjectService
    {
        private IRepository _repository;

        public MappingProjectService(IRepository repository)
        {
            _repository = repository;
        }

        public List<IMappingProject> Get()
        {
            return _repository.MappingProjects.Get();
        }

        public IMappingProject Get(int id)
        {
            return _repository.MappingProjects.GetDetails(id);
        }

        public List<IMappingProject> Get(
            string municipalityNumber,
            int officeId,
            MappingProjectState state,
            RelevantMappingProjectDeliveryType deliveryType,
            int year)
        {
            return _repository.MappingProjects.Get(municipalityNumber, officeId, state, deliveryType, year);
        }
    }
}
