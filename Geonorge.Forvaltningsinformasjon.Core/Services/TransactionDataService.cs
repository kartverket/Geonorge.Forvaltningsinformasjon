using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class TransactionDataService : ITransactionDataService
    {
        private const string _WmsServiceName = "sfkb-transaksjoner";
        private const string _AdminUnitsWmsServiceName = "adm_enheter2";

        private IRepository _repository;
        private ITransactionDataSldProvider _sldProvider;
        private IWmsUrlProvider _wmsUrlProvider;
        private IAdministrativeUnitSldProvider _administrativeUnitSldProvider;

        public TransactionDataService(
            IRepository repository, 
            ITransactionDataSldProvider sldProvider,
            IWmsUrlProvider wmsUrlProvider,
            IAdministrativeUnitSldProvider administrativeUnitSldProvider)
        {
            _repository = repository;
            _sldProvider = sldProvider;
            _wmsUrlProvider = wmsUrlProvider;
            _administrativeUnitSldProvider = administrativeUnitSldProvider;
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

        public string GetWmsUrl()
        {
            return _wmsUrlProvider.GetCapabilitiesUrl(_WmsServiceName);
        }

        public string GetAdministrativeUnitSld()
        {
            return _administrativeUnitSldProvider.GetSld();
        }

        public string GetAdminstrativeUnitsWmsUrl()
        {
            return _wmsUrlProvider.GetCapabilitiesUrl(_AdminUnitsWmsServiceName);
        }
    }
}
