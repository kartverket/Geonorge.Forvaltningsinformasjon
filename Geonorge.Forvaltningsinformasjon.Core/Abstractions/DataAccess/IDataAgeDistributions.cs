using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IDataAgeDistributions
    {
        List<IDataAgeDistribution> Get();
        List<IDataAgeDistribution> GetByCounty(int id);
        List<IDataAgeDistribution> GetByMunicipality(int id);
    }
}
