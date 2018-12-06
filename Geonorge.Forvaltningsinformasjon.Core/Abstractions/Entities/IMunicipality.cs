using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IMunicipality : IEntityBase
    {
        string Number { get; }
        string Name { get; }

        DateTime? PlannedIntroductionDate { get; }
        DateTime? IntroductionDate { get; }

        IntroductionState IntroductionState { get; }
    }
}
