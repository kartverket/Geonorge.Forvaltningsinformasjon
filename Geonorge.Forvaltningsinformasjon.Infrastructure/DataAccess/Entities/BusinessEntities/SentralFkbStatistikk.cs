using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class SentralFkbStatistikk : ITransactionData
    {
        public string DataSetName
        {
            get
            {
                return DatasettIdNavigation.Navn;
            }
        }

        public int SumLastWeek
        {
            get
            {
                return AntTransUke.HasValue ? (int)AntTransUke : 0;
            }
        }

        public int SumLastMonth
        {
            get
            {
                return AntTransMnd.HasValue ? (int)AntTransMnd : 0;
            }
        }

        public int SumLastYear 
        {
            get
            {
                return AntTransAr.HasValue ? (int)AntTransAr : 0;
            }
        }
    }
}
