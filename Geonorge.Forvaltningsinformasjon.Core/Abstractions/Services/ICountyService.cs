using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface ICountyService : IEntityServiceBase<ICounty>
    {
        ICounty GetByMunicipalityId(int municipalityId);
    }
}
