using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence
{
    public interface IRepository
    {
        ICountyDataSet Counties { get; }
        IMunicipalityDataSet Municipalities { get; }
    }
}
