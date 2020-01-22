using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IDataAgeDistributionService : IEntityServiceBase<IDataAgeDistribution>
    {
        List<IDataAgeDistribution> GetByCounty(int id);
        List<IDataAgeDistribution> GetByMunicipality(int id);
    }
}
