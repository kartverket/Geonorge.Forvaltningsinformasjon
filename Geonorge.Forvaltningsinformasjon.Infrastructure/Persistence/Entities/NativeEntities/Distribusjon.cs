using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Distribusjon
    {
        public Distribusjon()
        {
            DistribusjonDatasett = new HashSet<DistribusjonDatasett>();
            Fdvrunde = new HashSet<Fdvrunde>();
            FdvrundeDistribusjon = new HashSet<FdvrundeDistribusjon>();
        }

        public int Id { get; set; }
        public string KommuneKommunenr { get; set; }
        public string DistribusjonsDato { get; set; }
        public string Kommentar { get; set; }
        public string Distribusjontype { get; set; }
        public string DatamottaksDatoForrige { get; set; }
        public string DatamottaksDato { get; set; }
        public int? AntallEndringerMatrikkel { get; set; }
        public int? AntallEndringerFkb { get; set; }
        public int? AntallEndringerDyrkamark { get; set; }

        public Kommune KommuneKommunenrNavigation { get; set; }
        public ICollection<DistribusjonDatasett> DistribusjonDatasett { get; set; }
        public ICollection<Fdvrunde> Fdvrunde { get; set; }
        public ICollection<FdvrundeDistribusjon> FdvrundeDistribusjon { get; set; }
    }
}
