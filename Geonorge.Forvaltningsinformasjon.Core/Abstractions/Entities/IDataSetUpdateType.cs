using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IDataSetUpdateType : IEntityBase
    {
        string Name { get; }
        string Description { get; }
    }
}
