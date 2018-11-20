
namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface ICounty : IEntityBase
    {
        string Name { get; }
        int MunicipalityCount { get; }
        int DirectUpdateCount { get; }
    }
}
