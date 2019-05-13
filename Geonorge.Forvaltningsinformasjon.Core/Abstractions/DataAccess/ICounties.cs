using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface ICounties : IEntities<ICounty>
    {
        ICounty GetByMunicipalityId(int municipalityId);
    }
}
