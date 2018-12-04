using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Kunde
    {
        public Kunde()
        {
            Part = new HashSet<Part>();
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Navn { get; set; }
        public string KundeGruppeId { get; set; }
        public string Postgate { get; set; }
        public string Poststed { get; set; }
        public string Postnummer { get; set; }
        public int? KundeNr { get; set; }
        public string Epost { get; set; }
        public string Telefon { get; set; }
        public int? Aktiv { get; set; }

        public KundeGruppe KundeGruppe { get; set; }
        public ICollection<Part> Part { get; set; }
        public ICollection<Person> Person { get; set; }
    }
}
