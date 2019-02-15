using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class FdvdatasettForvaltningstype
    {
        public FdvdatasettForvaltningstype()
        {
            Fdvdatasett = new HashSet<Fdvdatasett>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Beskrivelse { get; set; }

        public ICollection<Fdvdatasett> Fdvdatasett { get; set; }
    }
}
