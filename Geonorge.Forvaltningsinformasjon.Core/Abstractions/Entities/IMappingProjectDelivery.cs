using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IMappingProjectDelivery
    {
        string Name { get; }
        RelevantMappingProjectDeliveryType Type { get; }
        string Deadline { get; }
        string ChangedDeadline { get; }
        string FinalDeadline { get; }
        string ReleaseDate { get; }
    }
}
