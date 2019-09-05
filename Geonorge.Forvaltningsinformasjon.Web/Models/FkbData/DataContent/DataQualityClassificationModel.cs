using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent
{
    public class DataQualityClassificationModel
    {
        public List<IDataQualityClassification> Classifications { get; set; }
    }
}
