using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class FdvdatasettArbeid
    {
        public FdvdatasettArbeid()
        {
            FdvdatasettArbeidLogg = new HashSet<FdvdatasettArbeidLogg>();
        }

        public long Id { get; set; }
        public int? FdvrundeId { get; set; }
        public int? DatasettId { get; set; }
        public string Brukernavn { get; set; }
        public string Dato { get; set; }
        public int? StatusId { get; set; }
        public string Kommentar { get; set; }
        public string Link { get; set; }

        public Datasett Datasett { get; set; }
        public Fdvrunde Fdvrunde { get; set; }
        public Fdvstatus1 Status { get; set; }
        public ICollection<FdvdatasettArbeidLogg> FdvdatasettArbeidLogg { get; set; }
    }
}
