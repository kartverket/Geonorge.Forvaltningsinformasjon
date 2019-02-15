using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Kartkontor
    {
        public Kartkontor()
        {
            Kommune = new HashSet<Kommune>();
            Part = new HashSet<Part>();
            Rolle = new HashSet<Rolle>();
        }

        public int Nr { get; set; }
        public string Navn { get; set; }
        public int? Aktiv { get; set; }

        public ICollection<Kommune> Kommune { get; set; }
        public ICollection<Part> Part { get; set; }
        public ICollection<Rolle> Rolle { get; set; }
    }
}
