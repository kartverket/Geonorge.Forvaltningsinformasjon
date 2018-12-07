using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class DataSetService : IDataSetService
    {
        private IRepository _repository;

        public DataSetService(IRepository repository)
        {
            _repository = repository;
        }

        public List<IDataSet> Get()
        {
            return _repository.DataSets.Get();
        }

        public IDataSet Get(int id)
        {
            return _repository.DataSets.Get(id);
        }

        public List<IDataSet> GetByMunicipality(int id)
        {
            return _repository.DataSets.GetByMunicipality(id);
        }
    }
}
