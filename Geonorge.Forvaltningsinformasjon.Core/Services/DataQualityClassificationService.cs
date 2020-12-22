using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Core.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class DataQualityClassificationService : MapService, IDataQualityClassificationService
    {
        private const string _LayerName = "fkb_abcd";

        private IRepository _repository;
        private IDataQualityClassificationSldProvider _sldProvider;

        public DataQualityClassificationService(
            IRepository repository, 
            IDataQualityClassificationSldProvider sldProvider,
            IWmsUrlProvider wmsUrlProvider,
            IAdministrativeUnitSldProvider administrativeUnitSldProvider)
            : base (wmsUrlProvider, administrativeUnitSldProvider)
        {
            _repository = repository;
            _sldProvider = sldProvider;
            _WmsServiceName = "georef3";
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

        public Dictionary<string, ILegendItemStyle> GetLegendItemStyles()
        {
            return _sldProvider.GetLegendItemStyles();
        }
    }
}
