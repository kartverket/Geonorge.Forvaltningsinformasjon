
namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface ICounty : IEntityBase, IBoundingBox
    {
        string Number { get; }
        string Name { get; }
        int MunicipalityCount { get; }
        int DirectUpdateCount { get; }
    }
}
