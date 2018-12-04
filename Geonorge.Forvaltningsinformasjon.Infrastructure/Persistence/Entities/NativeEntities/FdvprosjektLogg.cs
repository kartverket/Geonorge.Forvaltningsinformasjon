namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class FdvprosjektLogg
    {
        public int Id { get; set; }
        public int? FdvprosjektId { get; set; }
        public string Brukernavn { get; set; }
        public string Dato { get; set; }
        public string Logg { get; set; }

        public Fdvprosjekt Fdvprosjekt { get; set; }
    }
}
