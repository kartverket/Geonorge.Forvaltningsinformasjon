using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Persistence;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence
{
    public class Repository : IRepository
    {
        private FDV_Drift2Context _dbContext;
        private ICounties _counties;
        private IMunicipalities _municipalities;
        private IDataSets _dataSets;

        public ICounties Counties
        {
            get
            {
                return _counties;
            }
        }

        public IMunicipalities Municipalities
        {
            get
            {
                return _municipalities;
            }
        }

        public IDataSets DataSets
        {
            get
            {
                return _dataSets;
            }
        }

        public Repository(FDV_Drift2Context dbContext, ICounties counties, IMunicipalities municipalities, IDataSets datasets)
        {
            _dbContext = dbContext;
            _counties = counties;
            _municipalities = municipalities;
            _dataSets = datasets;
        }
    }
}       
