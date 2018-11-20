using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence
{
    public interface IMunicipalityDataSet : IDataSet<IMunicipality>
    {
        List<IMunicipality> GetByCounty(string id);
    }
}
