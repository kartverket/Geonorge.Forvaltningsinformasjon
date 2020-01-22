using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class CoordintateSystem
    {
        public short Id { get; set; }

        public string EpsgName { get; set; }

        public ICollection<Municipality> Municipalities { get; set; }

        public CoordintateSystem()
        {
            Municipalities = new HashSet<Municipality>();
        }
    }
}
