using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Kommune : IMunicipality
    {
        public string Id
        {
            get
            {
                return Kommunenr;
            }
        }
        public string Name
        {
            get
            {
                return Kommunenavn;
            }
        }
        public DateTime? PlannedIntroductionDate
        {
            get
            {
                if (SentralFkb.First().PlanlagtInnforing == null)
                {
                    return null;
                }
                return DateTime.ParseExact(
                    SentralFkb.First().PlanlagtInnforing,
                    "yyyymmdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
            }
        }
        public DateTime? IntroductionDate
        {
            get
            {
                if (SentralFkb.First().DirekteoppdateringInfort == null)
                {
                    return null;
                }
                return DateTime.ParseExact(
                    SentralFkb.First().DirekteoppdateringInfort,
                    "yyyymmdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
            }
        }

        public IntroductionState IntroductionState
        {
            get
            {
                if (IntroductionDate.HasValue)
                {
                    return IntroductionState.Introduced;
                }
                else if (PlannedIntroductionDate.HasValue)
                {
                    return IntroductionState.Planned;
                }
                return IntroductionState.NotPlanned;
            }
        }
    }
}
