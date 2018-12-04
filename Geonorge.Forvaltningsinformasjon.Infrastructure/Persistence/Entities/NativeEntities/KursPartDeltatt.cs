namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class KursPartDeltatt
    {
        public int Id { get; set; }
        public int? KursPartId { get; set; }
        public int? KursDatoId { get; set; }

        public KursDato KursDato { get; set; }
        public KursPart KursPart { get; set; }
    }
}
