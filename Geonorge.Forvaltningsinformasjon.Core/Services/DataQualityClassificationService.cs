using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class DataQualityClassificationService : IDataQualityClassificationService
    {
        private IRepository _repository;

        public DataQualityClassificationService(IRepository repository)
        {
            _repository = repository;
        }

        public List<IDataQualityClassification> Get()
        {
            return _repository.DataQualityClassifications.Get();
        }

        public IDataQualityClassification Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<IDataQualityClassification> GetByCounty(int id)
        {
            return _repository.DataQualityClassifications.GetByCounty(id);
        }

        public List<IDataQualityClassification> GetByMunicipality(int id)
        {
            return _repository.DataQualityClassifications.GetByMunicipality(id);
        }
    }
}
