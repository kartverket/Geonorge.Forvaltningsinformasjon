using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class PuljeManed
    {
        public PuljeManed()
        {
            SentralFkb = new HashSet<SentralFkb>();
        }

        public int Pulje { get; set; }
        public string Maned { get; set; }
        public string Merknad { get; set; }

        public ICollection<SentralFkb> SentralFkb { get; set; }
    }
}
