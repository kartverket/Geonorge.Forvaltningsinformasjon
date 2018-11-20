using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Kurs
    {
        public Kurs()
        {
            KursDato = new HashSet<KursDato>();
            KursPart = new HashSet<KursPart>();
        }

        public int Id { get; set; }
        public int? KartkontorId { get; set; }
        public int? KursTypeId { get; set; }
        public string PlanlagtManed { get; set; }
        public string TilgangBrukernavn { get; set; }
        public string Status { get; set; }
        public string Merknad { get; set; }
        public int? Aktiv { get; set; }

        public KursType KursType { get; set; }
        public Tilgang TilgangBrukernavnNavigation { get; set; }
        public ICollection<KursDato> KursDato { get; set; }
        public ICollection<KursPart> KursPart { get; set; }
    }
}
