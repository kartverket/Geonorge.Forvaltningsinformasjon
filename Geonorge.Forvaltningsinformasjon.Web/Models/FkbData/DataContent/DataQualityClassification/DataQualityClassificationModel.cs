using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent.DataQualityClassification
{
    public class DataQualityClassificationModel
    {
        public List<IDataQualityClassification> Classifications { get; set; }
        public string AdministrativeUnitName { get; set; }
        public IBoundingBox AdministrativeUnitBBox { get; set; }
    }
}
