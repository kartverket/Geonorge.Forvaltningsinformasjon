using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class DataQualityClassificationService : IDataQualityClassificationService
    {
        private const string _WmsServiceName = "georef3";
        private const string _LayerName = "fkb_abcd";
        private const string _AdminUnitsWmsServiceName = "adm_enheter2";

        private IRepository _repository;
        private IDataQualityClassificationSldProvider _sldProvider;
        private IWmsUrlProvider _wmsUrlProvider;
        private IAdministrativeUnitSldProvider _administrativeUnitSldProvider;

        public DataQualityClassificationService(
            IRepository repository, 
            IDataQualityClassificationSldProvider sldProvider,
            IWmsUrlProvider wmsUrlProvider,
            IAdministrativeUnitSldProvider administrativeUnitSldProvider)
        {
            _repository = repository;
            _sldProvider = sldProvider;
            _wmsUrlProvider = wmsUrlProvider;
            _administrativeUnitSldProvider = administrativeUnitSldProvider;
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

        public string GetSld()
        {
            return _sldProvider.GetSld();
        }

        public string GetLegendUrl(string format, int width, int height)
        {
            return _wmsUrlProvider.GetLegendGraphicsUrl(_WmsServiceName, _LayerName, format, width, height);
        }

        public string GetWmsUrl()
        {
            return _wmsUrlProvider.GetCapabilitiesUrl(_WmsServiceName);
        }

        public string GetAdminstrativeUnitsWmsUrl()
        {
            return _wmsUrlProvider.GetCapabilitiesUrl(_AdminUnitsWmsServiceName);
        }

        public string GetAdministrativeUnitSld()
        {
            return _administrativeUnitSldProvider.GetSld();
        }
    }
}
