using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent
{
    public class DataQualityClassificationModel
    {
        public List<IDataQualityClassification> Classifications { get; set; }
        public string AdministrativeUnitName { get; set; }

        public MapViewModel MapViewModel { get; set; } = new MapViewModel();
    }
}
