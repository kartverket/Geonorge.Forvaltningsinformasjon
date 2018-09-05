using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class KundeGruppe
    {
        public KundeGruppe()
        {
            Kunde = new HashSet<Kunde>();
        }

        public string Id { get; set; }
        public string Navn { get; set; }

        public ICollection<Kunde> Kunde { get; set; }
    }
}
