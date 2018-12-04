namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class FdvdatasettArbeidLogg
    {
        public int Id { get; set; }
        public long? FdvdatasettArbeidId { get; set; }
        public int? Indeks { get; set; }
        public string Navn { get; set; }
        public string Brukernavn { get; set; }
        public string Dato { get; set; }
        public int? StatusId { get; set; }
        public string Kommentar { get; set; }
        public string Link { get; set; }

        public FdvdatasettArbeid FdvdatasettArbeid { get; set; }
        public Fdvstatus1 Status { get; set; }
    }
}
