using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IDataAgeDistributionService : IEntityServiceBase<IDataAgeDistribution>, IMapService
    {
        List<IDataAgeDistribution> GetByCounty(int id);
        List<IDataAgeDistribution> GetByMunicipality(int id);
    }
}
