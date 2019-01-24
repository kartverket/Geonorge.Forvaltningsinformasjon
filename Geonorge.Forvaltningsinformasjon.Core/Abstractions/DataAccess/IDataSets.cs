using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IDataSets : IEntities<IDataSet>
    {
        List<IDataSet> GetByMunicipality(int id);
    }
}
