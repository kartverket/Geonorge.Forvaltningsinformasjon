using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IDataSet : IEntityBase
    {
        string Name { get; }
        string UpdateTypeName { get; }
        string UpdateTypeDescription { get; }
        DateTime? UpdateDate { get; }
        bool IsEmpty { get; }
    }
}
