namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class Fdvnotat
    {
        public int Id { get; set; }
        public string KommuneKommunenr { get; set; }
        public string Tekst { get; set; }

        public Kommune KommuneKommunenrNavigation { get; set; }
    }
}
