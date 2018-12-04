using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence
{
    public class Repository : IRepository
    {
        private FDV_Drift2Context _dbContext;
        private ICountyDataSet _counties;
        private IMunicipalityDataSet _municipalities;

        public ICountyDataSet Counties
        {
            get
            {
                return _counties;
            }
        }

        public IMunicipalityDataSet Municipalities
        {
            get
            {
                return _municipalities;
            }
        }

        public Repository(FDV_Drift2Context dbContext, ICountyDataSet counties, IMunicipalityDataSet municipalities)
        {
            _dbContext = dbContext;
            _counties = counties;
            _municipalities = municipalities;
        }
    }
}       
