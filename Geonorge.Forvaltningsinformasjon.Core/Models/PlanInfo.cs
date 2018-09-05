namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class PlanInfo
    {
        public int Id { get; set; }
        public string KommuneKommunenr { get; set; }
        public string Planregisterlink { get; set; }
        public int? LeveransenivaRegPlan { get; set; }
        public int? LeveransenivaKomPlan { get; set; }
        public int? Planregistertype { get; set; }
        public string GeosynkInnfort { get; set; }
        public string Merknad { get; set; }

        public Kommune KommuneKommunenrNavigation { get; set; }
    }
}
