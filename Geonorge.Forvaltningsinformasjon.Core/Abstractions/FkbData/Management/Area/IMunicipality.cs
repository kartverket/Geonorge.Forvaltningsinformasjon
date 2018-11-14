using Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area
{
    public interface IMunicipality
    {
        string Number { get; set; }
        string Name { get; set; }

        DateTime PlannedIntroductionDate { get; set; }
        DateTime IntroductionDate { get; set; }

        IntroductionState IntroductionState { get; }
    }
}
