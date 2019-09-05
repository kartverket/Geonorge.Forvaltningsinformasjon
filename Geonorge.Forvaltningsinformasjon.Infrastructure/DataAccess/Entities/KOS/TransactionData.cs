using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos
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
    }
}
