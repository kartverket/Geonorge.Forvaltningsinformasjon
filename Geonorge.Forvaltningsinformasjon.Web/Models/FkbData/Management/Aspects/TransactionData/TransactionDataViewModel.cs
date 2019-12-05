using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.TransactionData
{
    public enum Period
    {
        Week,
        Month,
        Year
    }

    public class TransactionDataViewModel
    {
        public string AdministrativeUnitName { get; set;}
        public List<ITransactionData> TransactionData { get; set; }

        public int SumLastWeek
        {
            get
            {
                return TransactionData.Sum(d => d.SumLastWeek);
            }
        }

        public int SumLastMonth
        {
            get
            {
                return TransactionData.Sum(d => d.SumLastMonth);
            }
        }
        public int SumLastYear
        {
            get
            {
                return TransactionData.Sum(d => d.SumLastYear);
            }
        }

        public MapViewModel MapViewModel { get; set; } = new MapViewModel();
    }
}
