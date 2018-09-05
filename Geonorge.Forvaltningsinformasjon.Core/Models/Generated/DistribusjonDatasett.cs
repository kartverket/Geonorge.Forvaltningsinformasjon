namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class DistribusjonDatasett
    {
        public int Id { get; set; }
        public int? DistribusjonId { get; set; }
        public int? DatasettId { get; set; }

        public Datasett Datasett { get; set; }
        public Distribusjon Distribusjon { get; set; }
    }
}
