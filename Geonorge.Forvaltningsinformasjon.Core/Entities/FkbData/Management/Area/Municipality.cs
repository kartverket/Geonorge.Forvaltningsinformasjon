using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management.Area
{
    class Municipality : IMunicipality
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public DateTime PlannedIntroductionDate { get; set; }
        public DateTime IntroductionDate { get; set; }

        public IntroductionState IntroductionState
        {
            get
            {
                if (IntroductionDate != default(DateTime))
                {
                    return IntroductionState.Introduced;
                }
                else if (PlannedIntroductionDate != default(DateTime))
                {
                    return IntroductionState.Planned;
                }
                return IntroductionState.NotPlanned;
            }
        }
    }
}
