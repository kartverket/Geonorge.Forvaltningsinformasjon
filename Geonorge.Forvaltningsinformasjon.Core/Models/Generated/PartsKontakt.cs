namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class PartsKontakt
    {
        public int Id { get; set; }
        public int? PartId { get; set; }
        public int? PersonId { get; set; }
        public string RolleNavn { get; set; }
        public int? Aktiv { get; set; }

        public Part Part { get; set; }
        public Person Person { get; set; }
        public Rolle RolleNavnNavigation { get; set; }
    }
}
