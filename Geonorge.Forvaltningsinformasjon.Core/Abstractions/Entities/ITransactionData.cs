using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface ITransactionData : IEntityBase
    {
        string DataSetName { get; }
        int SumLastWeek { get; }
        int SumLastMonth { get; }
        int SumLastYear { get; }
    }
}
