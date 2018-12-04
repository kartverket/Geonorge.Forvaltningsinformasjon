using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Koordsys
    {
        public Koordsys()
        {
            Kommune = new HashSet<Kommune>();
        }

        public short Koordsys1 { get; set; }
        public string Navn { get; set; }
        public string Epsg { get; set; }
        public string UtmSone { get; set; }
        public int? EpsgWgs { get; set; }

        public ICollection<Kommune> Kommune { get; set; }
    }
}
