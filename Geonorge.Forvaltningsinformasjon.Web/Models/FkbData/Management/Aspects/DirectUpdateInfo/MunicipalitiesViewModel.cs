﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.DirectUpdateInfo
{
    public class MunicipalitiesViewModel
    {
        public List<IMunicipality> Municipalities { get; set; }
        public int DirectUpdateCount { get; set; }
        public string CountyName { get; set; }

        public MapViewModel MapViewModel { get; set; }

        public string GetIntroductionStateText(IntroductionState introductionState)
        {
            switch (introductionState)
            {
                case IntroductionState.NotIntroduced:
                    return "Ikke innført";
                case IntroductionState.Planned:
                    return "Direkteoppdatering planlagt innført {0}";
                case IntroductionState.Introduced:
                    return "Direkteoppdatering innført";
                case IntroductionState.Geosynch:
                    return "Geosynkronisering innført";
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
