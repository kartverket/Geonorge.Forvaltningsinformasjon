using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Fdvpart
    {
        public Fdvpart()
        {
            Fdvdatamottak = new HashSet<Fdvdatamottak>();
        }

        public int Id { get; set; }
        public int? FdvprosjektId { get; set; }
        public int? PartId { get; set; }

        public Fdvprosjekt Fdvprosjekt { get; set; }
        public Part Part { get; set; }
        public ICollection<Fdvdatamottak> Fdvdatamottak { get; set; }
    }
}
