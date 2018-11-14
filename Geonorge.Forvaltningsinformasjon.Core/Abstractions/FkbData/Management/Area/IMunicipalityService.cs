using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management.Area;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area
{
    public interface IMunicipalityService : IEntityServiceBase<IMunicipality>
    {
        List<IMunicipality> GetAllByCounty(string number);
    }
}
