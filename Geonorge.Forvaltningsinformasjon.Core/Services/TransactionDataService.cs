using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Core.Services.Common;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class TransactionDataService : MapService, ITransactionDataService
    {
        private IRepository _repository;
        private ITransactionDataSldProvider _sldProvider;

        public TransactionDataService(
            IRepository repository,
            IWmsUrlProvider wmsUrlProvider,
            IAdministrativeUnitSldProvider administrativeUnitSldProvider,
            ITransactionDataSldProvider sldProvider)
            : base(wmsUrlProvider, administrativeUnitSldProvider)
        {
            _repository = repository;
            _sldProvider = sldProvider;
            _WmsServiceName = "sfkb-transaksjoner";
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

        public Dictionary<string, ILegendItemStyle> GetLayerStyles(List<ITransactionData> transactionData)
        {
            return _sldProvider.GetLegendItemStyles(transactionData);
        }
    }
}
