using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IMappingProjectService : IEntityServiceBase<IMappingProject>
    {
        List<IMappingProject> Get(
            string municipalityNumber,
            int officeId,
            MappingProjectState state,
            RelevantMappingProjectDeliveryType deliveryType,
            int year);
    }
}
