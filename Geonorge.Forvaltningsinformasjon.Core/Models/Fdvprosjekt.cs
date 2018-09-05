using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class Fdvprosjekt
    {
        public Fdvprosjekt()
        {
            Fdvdatasett = new HashSet<Fdvdatasett>();
            Fdvpart = new HashSet<Fdvpart>();
            FdvprosjektLenke = new HashSet<FdvprosjektLenke>();
            FdvprosjektLogg = new HashSet<FdvprosjektLogg>();
            Fdvrunde = new HashSet<Fdvrunde>();
        }

        public int Id { get; set; }
        public string Ar { get; set; }
        public string KommuneKommunenr { get; set; }
        public string Prosjektnavn { get; set; }
        public int? StatusAvtale { get; set; }
        public int? StatusArsmote { get; set; }
        public int? StatusFakturering { get; set; }
        public int? Aktiv { get; set; }

        public Kommune KommuneKommunenrNavigation { get; set; }
        public Fdvstatus1 StatusArsmoteNavigation { get; set; }
        public Fdvstatus1 StatusAvtaleNavigation { get; set; }
        public Fdvstatus1 StatusFaktureringNavigation { get; set; }
        public ICollection<Fdvdatasett> Fdvdatasett { get; set; }
        public ICollection<Fdvpart> Fdvpart { get; set; }
        public ICollection<FdvprosjektLenke> FdvprosjektLenke { get; set; }
        public ICollection<FdvprosjektLogg> FdvprosjektLogg { get; set; }
        public ICollection<Fdvrunde> Fdvrunde { get; set; }
    }
}
