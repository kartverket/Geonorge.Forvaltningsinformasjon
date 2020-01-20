using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class TransactionData : ITransactionData
    {
        #region ITransactionData
        public int Id { get; set; }
        public string DataSetName
        {
            get
            {
                return DataSet.Name;
            }
        }
        public int SumLastWeek { get; set; }
        public int SumLastMonth { get; set; }
        public int SumLastYear { get; set; }
        #endregion

        public string MunicipalityNumber { get; set; }
        public int DataSetId { get; set; }
        public int? ObjectCount { get; set; }
        internal DateTime? GeonorgeFileDate { get; set; }
        public DataSet DataSet { get; set; }
        public Municipality Municipality { get; set; }

        // age categories
        public long? Year0 { get; set; }
        public long? Year1 { get; set; }
        public long? Year2 { get; set; }
        public long? Year3 { get; set; }
        public long? Year4 { get; set; }
        public long? Years5To9 { get; set; }
        public long? Years10To19 { get; set; }
        public long? Older { get; set; }

        // quality categories
        public long? Measured { get; set; }
        public long? PhotogrammetricB { get; set; }
        public long? PhotogrammetricC { get; set; }
        public long? DigitalizedM200 { get; set; }
        public long? DigitalizedS200 { get; set; }
        public long? NotMeasured { get; set; }
    }
}
