using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.Plandata
{
    public class MunicipalityViewModel
    {
        public string Name { get; set; }
        public string StatusMessage { get; set; }
        public List<DataSet> DataSets { get; set; }
    }

    public class DataSet
    {
        public string Name { get; set; }
        public string UpdateTypeName { get; set; }
        public DateTime? LastDeliveryFromMunicipality { get; set; }
        public DateTime? FileGeneratedGeonorge { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public string Comment { get; set; }
    }
}
