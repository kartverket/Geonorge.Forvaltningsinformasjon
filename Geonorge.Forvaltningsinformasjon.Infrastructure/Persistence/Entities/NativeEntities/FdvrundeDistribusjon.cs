namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class FdvrundeDistribusjon
    {
        public int Id { get; set; }
        public int? FdvrundeId { get; set; }
        public int? DistribusjonId { get; set; }

        public Distribusjon Distribusjon { get; set; }
        public Fdvrunde Fdvrunde { get; set; }
    }
}
