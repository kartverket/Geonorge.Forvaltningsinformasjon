using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area
{
    public class MunicipalityDetailsViewModel
    {
        public List<IDataSet> DataSets { get; set; }
        public string GetIntroductionStateText(IntroductionState introductionState)
        {
            switch (introductionState)
            {
                case IntroductionState.NotPlanned:
                    return "Ikke planlagt innføring av direkteoppdatering i Sentral FKB";
                case IntroductionState.Planned:
                    return "Direkteoppdatering i Sentral FKB planlagt innført {0}";
                case IntroductionState.Introduced:
                    return "Direkteoppdatering i Sentral FKB innført {0}";
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
