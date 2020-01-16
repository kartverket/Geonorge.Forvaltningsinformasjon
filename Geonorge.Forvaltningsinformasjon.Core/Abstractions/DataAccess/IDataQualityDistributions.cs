using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IDataQualityDistributions
    {
        List<IDataQualityDistribution> Get();
        List<IDataQualityDistribution> GetByCounty(int id);
        List<IDataQualityDistribution> GetByMunicipality(int id);
    }
}
