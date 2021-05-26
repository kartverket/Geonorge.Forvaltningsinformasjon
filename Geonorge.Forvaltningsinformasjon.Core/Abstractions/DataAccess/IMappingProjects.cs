using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IMappingProjects
    {
        List<IMappingProject> Get();
        List<IMappingProject> Get(
            string municipalityNumber, 
            int officeId,
            MappingProjectState state,
            int year);
        IMappingProject GetDetails(int id);
    }
}
