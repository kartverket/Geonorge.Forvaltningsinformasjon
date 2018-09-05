using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class Part
    {
        public Part()
        {
            Fdvpart = new HashSet<Fdvpart>();
            KursPart = new HashSet<KursPart>();
            PartsKontakt = new HashSet<PartsKontakt>();
        }

        public int Id { get; set; }
        public int? KundeId { get; set; }
        public int? KartkontorNr { get; set; }
        public string Navn { get; set; }
        public string Kode { get; set; }
        public string PartskontoNr { get; set; }
        public string PartskontoNavn { get; set; }
        public string Referanse { get; set; }
        public int? Aktiv { get; set; }
        public int? FakturaKundeNr { get; set; }

        public Kartkontor KartkontorNrNavigation { get; set; }
        public Kunde Kunde { get; set; }
        public ICollection<Fdvpart> Fdvpart { get; set; }
        public ICollection<KursPart> KursPart { get; set; }
        public ICollection<PartsKontakt> PartsKontakt { get; set; }
    }
}
