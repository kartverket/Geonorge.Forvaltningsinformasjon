using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos
{
    internal class DataSet
    {
        public DataSet()
        {
            FdvDataSet = new HashSet<FdvDataSet>();
            TransactionData = new HashSet<TransactionData>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int? Active { get; set; }

        public ICollection<FdvDataSet> FdvDataSet { get; set; }
        public ICollection<TransactionData> TransactionData { get; set; }
    }
}
