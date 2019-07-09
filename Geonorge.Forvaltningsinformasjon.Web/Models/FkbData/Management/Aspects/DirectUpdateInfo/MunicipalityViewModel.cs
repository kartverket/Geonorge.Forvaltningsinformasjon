using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.DirectUpdateInfo
{
    public class MunicipalityViewModel
    {
        public string Name { get; set; }
        public List<IDataSet> DataSets { get; set; }
        public string Caption { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
