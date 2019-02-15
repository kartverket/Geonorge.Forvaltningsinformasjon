namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class SentralFkb
    {
        public int Id { get; set; }
        public string KommuneKommunenr { get; set; }
        public string KonverteringFerdig { get; set; }
        public string PlanlagtInnforing { get; set; }
        public string DirekteoppdateringInfort { get; set; }
        public string OnsketInnforing { get; set; }
        public string PriListe { get; set; }
        public int? Pulje { get; set; }
        public string BekreftetPulje { get; set; }
        public string BekreftetKlar { get; set; }
        public string Merknad { get; set; }

        public Kommune KommuneKommunenrNavigation { get; set; }
        public PuljeManed PuljeNavigation { get; set; }
    }
}
