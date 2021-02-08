using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    class MunicipalityService : IMunicipalityService
    {
        private IRepository _repository;

        public MunicipalityService(IRepository repository)
        {
            _repository = repository;
        }

        public List<IMunicipality> Get()
        {
            return _repository.Municipalities.Get();
        }

        public IMunicipality Get(int id)
        {
            IMunicipality municipality = _repository.Municipalities.Get(id);

            InitIntroductionState(municipality);

            return municipality;
        }

        public List<IMunicipality> GetByCounty(int id)
        {
            List<IMunicipality> municipalities = _repository.Municipalities.GetByCounty(id);

            municipalities.ForEach(m => InitIntroductionState(m));

            return municipalities;
        }

        private void InitIntroductionState(IMunicipality municipality)
        {
            if (municipality.IntroductionDate.HasValue)
            {
                municipality.IntroductionState = IntroductionState.Introduced;
            }
            else if (municipality.PlannedIntroductionDate.HasValue)
            {
                municipality.IntroductionState = IntroductionState.Planned;
            }
            else
            {
                municipality.IntroductionState = IntroductionState.NotIntroduced;

                List<IDataSet> dataSets = _repository.DataSets.GetByMunicipality(municipality.Id);

                if (dataSets.Count > 0)
                {
                    IDataSet dataSet = dataSets.First(d => string.Compare(d.Name, "FKB-Bygning", true) == 0);

                    if (dataSet != null && string.Compare(dataSet.UpdateTypeName, "Geosynkronisering", true) == 0)
                    {
                        municipality.IntroductionState = IntroductionState.Geosynch;
                    }
                }
            }
        }
    }
}
