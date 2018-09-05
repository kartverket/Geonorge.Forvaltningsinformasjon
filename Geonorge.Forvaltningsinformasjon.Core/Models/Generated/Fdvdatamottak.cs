namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class Fdvdatamottak
    {
        public int Id { get; set; }
        public int? FdvrundeId { get; set; }
        public int? FdvpartId { get; set; }
        public long? FdvdatasettId { get; set; }
        public string Dato { get; set; }
        public string Brukernavn { get; set; }
        public string Kommentar { get; set; }

        public Fdvdatasett Fdvdatasett { get; set; }
        public Fdvpart Fdvpart { get; set; }
        public Fdvrunde Fdvrunde { get; set; }
    }
}
