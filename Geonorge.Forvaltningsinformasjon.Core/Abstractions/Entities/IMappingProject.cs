using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IMappingProject : IEntityBase
    {
        string Name { get; }
        int Year { get; }
        IOffice Office { get; }
        MappingProjectState State { get; }
        List<IMunicipality> Municipalities { get; }
        List<IMappingProjectDelivery> Deliveries { get; }
    }
}
