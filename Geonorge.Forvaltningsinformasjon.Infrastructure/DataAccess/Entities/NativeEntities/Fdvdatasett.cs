using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Fdvdatasett
    {
        public Fdvdatasett()
        {
            Fdvdatamottak = new HashSet<Fdvdatamottak>();
        }

        public int Id { get; set; }
        public int? FdvprosjektId { get; set; }
        public int? DatasettId { get; set; }
        public int? FdvdatasettForvaltningstypeId { get; set; }

        public Datasett Datasett { get; set; }
        public FdvdatasettForvaltningstype FdvdatasettForvaltningstype { get; set; }
        public Fdvprosjekt Fdvprosjekt { get; set; }
        public ICollection<Fdvdatamottak> Fdvdatamottak { get; set; }
    }
}
