using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class Rolle
    {
        public Rolle()
        {
            PartsKontakt = new HashSet<PartsKontakt>();
        }

        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int? KartkontorNr { get; set; }

        public Kartkontor KartkontorNrNavigation { get; set; }
        public ICollection<PartsKontakt> PartsKontakt { get; set; }
    }
}
