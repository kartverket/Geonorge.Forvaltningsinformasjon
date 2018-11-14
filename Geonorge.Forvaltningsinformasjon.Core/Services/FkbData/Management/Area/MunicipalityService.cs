using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management.Area;
using Geonorge.Forvaltningsinformasjon.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management;
using System.Globalization;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;

namespace Geonorge.Forvaltningsinformasjon.Core.Services.FkbData.Management.Area
{
    class MunicipalityService : IMunicipalityService
    {
        private IRepository _repository;

        public MunicipalityService(IRepository repository)
        {
            _repository = repository;
        }

        public List<IMunicipality> GetAll()
        {
            return _repository.Get<Kommune>().Select(k => Convert(k)).ToList();
        }

        public List<IMunicipality> GetAllByCounty(string number)
        {
            return _repository.Get<Kommune>().Where(k => k.FylkeFylkesnr == number).Select(k => Convert(k)).ToList();
        }

        private IMunicipality Convert(Kommune kommune)
        {
            IMunicipality municipality = new Municipality()
            {
                Number = kommune.Kommunenr,
                Name = kommune.Kommunenavn,
            };

            SentralFkb introductionDescriptor = kommune.SentralFkb.FirstOrDefault();

            if (introductionDescriptor != default(SentralFkb))
            {
                if (!string.IsNullOrWhiteSpace(introductionDescriptor.DirekteoppdateringInfort))
                {
                    municipality.IntroductionDate = DateTime.ParseExact(
                        introductionDescriptor.DirekteoppdateringInfort,
                        "yyyymmdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None);
                }

                if (!string.IsNullOrWhiteSpace(introductionDescriptor.PlanlagtInnforing))
                {
                    municipality.PlannedIntroductionDate = DateTime.ParseExact(
                        introductionDescriptor.PlanlagtInnforing,
                        "yyyymmdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None);
                }
            }
            return municipality;
        }
    }
}
