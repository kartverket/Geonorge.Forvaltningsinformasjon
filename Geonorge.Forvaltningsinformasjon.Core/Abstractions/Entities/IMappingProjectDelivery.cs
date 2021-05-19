
namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IMappingProjectDelivery
    {
        string Name { get; }
        string TypeName { get; }
        string Deadline { get; }
        string ChangedDeadline { get; }
        string FinalDeadline { get; }
        string ReleaseDate { get; }
    }
}
