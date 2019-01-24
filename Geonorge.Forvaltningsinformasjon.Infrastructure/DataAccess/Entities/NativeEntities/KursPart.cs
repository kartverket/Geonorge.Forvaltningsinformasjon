using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class KursPart
    {
        public KursPart()
        {
            KursPartDeltatt = new HashSet<KursPartDeltatt>();
        }

        public int Id { get; set; }
        public int? KursId { get; set; }
        public int? PartId { get; set; }

        public Kurs Kurs { get; set; }
        public Part Part { get; set; }
        public ICollection<KursPartDeltatt> KursPartDeltatt { get; set; }
    }
}
