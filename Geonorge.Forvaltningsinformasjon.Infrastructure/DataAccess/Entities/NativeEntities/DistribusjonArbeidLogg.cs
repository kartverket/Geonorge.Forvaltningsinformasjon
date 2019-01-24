namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class DistribusjonArbeidLogg
    {
        public int Id { get; set; }
        public int? DistribusjonArbeidId { get; set; }
        public int? Indeks { get; set; }
        public string Navn { get; set; }
        public string Brukernavn { get; set; }
        public string Dato { get; set; }
        public string Kommentar { get; set; }
        public string Link { get; set; }

        public DistribusjonArbeid DistribusjonArbeid { get; set; }
    }
}
