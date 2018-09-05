namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class FdvprosjektLenke
    {
        public int Id { get; set; }
        public int? FdvprosjektId { get; set; }
        public string Lenke { get; set; }
        public string Beskrivelse { get; set; }

        public Fdvprosjekt Fdvprosjekt { get; set; }
    }
}
