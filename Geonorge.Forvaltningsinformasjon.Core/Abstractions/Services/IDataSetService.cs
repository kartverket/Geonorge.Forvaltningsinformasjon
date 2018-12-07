using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IDataSetService : IEntityServiceBase<IDataSet>
    {
        List<IDataSet> GetByMunicipality(int id);
    }
}
