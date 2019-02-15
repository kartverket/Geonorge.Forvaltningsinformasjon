using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class TransactionDataService : ITransactionDataService
    {
        private IRepository _repository;

        public TransactionDataService(IRepository repository)
        {
            _repository = repository;
        }

        public List<ITransactionData> Get()
        {
            return _repository.TransactionData.Get();
        }

        public ITransactionData Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ITransactionData> GetByCounty(int id)
        {
            return _repository.TransactionData.GetByCounty(id);
        }

        public List<ITransactionData> GetByMunicipality(int id)
        {
            return _repository.TransactionData.GetByMunicipality(id);
        }
    }
}
