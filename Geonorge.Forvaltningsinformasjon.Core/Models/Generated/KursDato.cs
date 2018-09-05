using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class KursDato
    {
        public KursDato()
        {
            KursPartDeltatt = new HashSet<KursPartDeltatt>();
        }

        public int Id { get; set; }
        public int? KursId { get; set; }
        public string Dato { get; set; }

        public Kurs Kurs { get; set; }
        public ICollection<KursPartDeltatt> KursPartDeltatt { get; set; }
    }
}
