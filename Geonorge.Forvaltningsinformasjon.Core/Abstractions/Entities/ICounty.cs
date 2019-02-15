
namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface ICounty : IEntityBase
    {
        string Number { get; }
        string Name { get; }
        int MunicipalityCount { get; }
        int DirectUpdateCount { get; }
    }
}
