using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Tilgang
    {
        public Tilgang()
        {
            Kurs = new HashSet<Kurs>();
        }

        public string Brukernavn { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Enhet { get; set; }
        public string Epost { get; set; }

        public ICollection<Kurs> Kurs { get; set; }
    }
}
