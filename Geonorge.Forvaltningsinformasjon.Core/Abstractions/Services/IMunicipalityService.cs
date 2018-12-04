using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IMunicipalityService : IEntityServiceBase<IMunicipality>
    {
        List<IMunicipality> GetByCounty(string number);
    }
}
