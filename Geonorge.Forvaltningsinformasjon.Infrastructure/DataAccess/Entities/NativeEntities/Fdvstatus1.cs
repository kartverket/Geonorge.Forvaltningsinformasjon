using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Fdvstatus1
    {
        public Fdvstatus1()
        {
            FdvdatasettArbeid = new HashSet<FdvdatasettArbeid>();
            FdvdatasettArbeidLogg = new HashSet<FdvdatasettArbeidLogg>();
            FdvprosjektStatusArsmoteNavigation = new HashSet<Fdvprosjekt>();
            FdvprosjektStatusAvtaleNavigation = new HashSet<Fdvprosjekt>();
            FdvprosjektStatusFaktureringNavigation = new HashSet<Fdvprosjekt>();
        }

        public int Id { get; set; }
        public string Navn { get; set; }
        public int? Gruppe { get; set; }

        public ICollection<FdvdatasettArbeid> FdvdatasettArbeid { get; set; }
        public ICollection<FdvdatasettArbeidLogg> FdvdatasettArbeidLogg { get; set; }
        public ICollection<Fdvprosjekt> FdvprosjektStatusArsmoteNavigation { get; set; }
        public ICollection<Fdvprosjekt> FdvprosjektStatusAvtaleNavigation { get; set; }
        public ICollection<Fdvprosjekt> FdvprosjektStatusFaktureringNavigation { get; set; }
    }
}
