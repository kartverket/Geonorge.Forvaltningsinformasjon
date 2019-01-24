using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class DistribusjonArbeid
    {
        public DistribusjonArbeid()
        {
            DistribusjonArbeidLogg = new HashSet<DistribusjonArbeidLogg>();
        }

        public int Id { get; set; }
        public int? FdvrundeId { get; set; }
        public int? Indeks { get; set; }
        public string Navn { get; set; }
        public string Brukernavn { get; set; }
        public string Dato { get; set; }
        public int? StatusId { get; set; }
        public string Kommentar { get; set; }
        public string Link { get; set; }

        public Fdvrunde Fdvrunde { get; set; }
        public ICollection<DistribusjonArbeidLogg> DistribusjonArbeidLogg { get; set; }
    }
}
