using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class DataQualityDistributionService : IDataQualityDistributionService
    {
        private IRepository _repository;

        public DataQualityDistributionService(IRepository repository)
        {
            _repository = repository;
        }
        public List<IDataQualityDistribution> Get()
        {
            return _repository.DataQualityDistributions.Get();
        }

        public IDataQualityDistribution Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<IDataQualityDistribution> GetByCounty(int id)
        {
            return _repository.DataQualityDistributions.GetByCounty(id);
        }

        public List<IDataQualityDistribution> GetByMunicipality(int id)
        {
            return _repository.DataQualityDistributions.GetByMunicipality(id);
        }
    }
}
