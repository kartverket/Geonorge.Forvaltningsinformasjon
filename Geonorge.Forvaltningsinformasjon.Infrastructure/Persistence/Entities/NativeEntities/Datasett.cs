using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Datasett
    {
        public Datasett()
        {
            DistribusjonDatasett = new HashSet<DistribusjonDatasett>();
            Fdvdatasett = new HashSet<Fdvdatasett>();
            FdvdatasettArbeid = new HashSet<FdvdatasettArbeid>();
        }

        public int Id { get; set; }
        public int? IdLogisk { get; set; }
        public string Type { get; set; }
        public string Navn { get; set; }
        public string Versjon { get; set; }
        public string Underversjon { get; set; }
        public string Fdvforvaltning { get; set; }
        public string Dokstatus { get; set; }
        public string ProduktspekUrl { get; set; }
        public string ObjektkatUrl { get; set; }
        public string GmlskjemaUrl { get; set; }
        public int? Aktiv { get; set; }

        public ICollection<DistribusjonDatasett> DistribusjonDatasett { get; set; }
        public ICollection<Fdvdatasett> Fdvdatasett { get; set; }
        public ICollection<FdvdatasettArbeid> FdvdatasettArbeid { get; set; }
    }
}
