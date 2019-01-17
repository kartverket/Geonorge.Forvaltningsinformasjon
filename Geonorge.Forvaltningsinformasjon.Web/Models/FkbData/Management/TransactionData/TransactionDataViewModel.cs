using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.TransactionData
{
    public class TransactionDataViewModel
    {
        public string AdministrativeUnitName { get; set; }
        public List<ITransactionData> TransactionData;
    }
}
