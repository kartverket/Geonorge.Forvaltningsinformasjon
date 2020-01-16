using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IDataQualityDistributionService : IEntityServiceBase<IDataQualityDistribution>
    {
        List<IDataQualityDistribution> GetByCounty(int id);
        List<IDataQualityDistribution> GetByMunicipality(int id);
    }
}
