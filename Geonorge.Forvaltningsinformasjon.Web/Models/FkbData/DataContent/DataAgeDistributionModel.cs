using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent
{
    public class DataAgeDistributionModel
    {
        public enum AdministrativeUnitType
        {
            Country,
            County,
            Municipality
        }

        public List<IDataAgeDistribution> Distributions { get; set; }
        public string AdministrativeUnitName { get; set; }
        public AdministrativeUnitType Type { get; set; }
    }
}
