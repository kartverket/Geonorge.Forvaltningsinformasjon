using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities
{
    public interface IEntityBase
    {
        int Id { get; }
        int? Active { get; }
    }
}
