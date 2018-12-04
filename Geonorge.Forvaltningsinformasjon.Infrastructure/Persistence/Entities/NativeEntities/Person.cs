using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Person
    {
        public Person()
        {
            PartsKontakt = new HashSet<PartsKontakt>();
        }

        public int Id { get; set; }
        public string Navn { get; set; }
        public string Epost { get; set; }
        public string Mobil { get; set; }
        public string Telefon { get; set; }
        public int KundeId { get; set; }
        public int? Aktiv { get; set; }

        public Kunde Kunde { get; set; }
        public ICollection<PartsKontakt> PartsKontakt { get; set; }
    }
}
