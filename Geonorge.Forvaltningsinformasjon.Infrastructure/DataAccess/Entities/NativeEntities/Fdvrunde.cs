using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Fdvrunde
    {
        public Fdvrunde()
        {
            DistribusjonArbeid = new HashSet<DistribusjonArbeid>();
            Fdvdatamottak = new HashSet<Fdvdatamottak>();
            FdvdatasettArbeid = new HashSet<FdvdatasettArbeid>();
            FdvrundeDistribusjon = new HashSet<FdvrundeDistribusjon>();
        }

        public int Id { get; set; }
        public int? FdvprosjektId { get; set; }
        public string PlanlagtDato { get; set; }
        public string PaminningSendt { get; set; }
        public string AvsluttetDato { get; set; }
        public string Kommentar { get; set; }
        public int? FkbDistribusjonId { get; set; }
        public bool? Milepel2Ferdig { get; set; }
        public bool? Milepel4Ferdig { get; set; }
        public bool? Fkbferdig { get; set; }
        public bool? VegnettFerdig { get; set; }
        public bool? Ar5ferdig { get; set; }
        public bool? LedningFerdig { get; set; }
        public bool? PlanFerdig { get; set; }
        public bool? TemadataFerdig { get; set; }
        public string Fkbmerknad { get; set; }
        public string VegnettMerknad { get; set; }
        public string Ar5merknad { get; set; }
        public string LedningMerknad { get; set; }
        public string PlanMerknad { get; set; }
        public string TemadataMerknad { get; set; }
        public int? Aktiv { get; set; }

        public Fdvprosjekt Fdvprosjekt { get; set; }
        public Distribusjon FkbDistribusjon { get; set; }
        public ICollection<DistribusjonArbeid> DistribusjonArbeid { get; set; }
        public ICollection<Fdvdatamottak> Fdvdatamottak { get; set; }
        public ICollection<FdvdatasettArbeid> FdvdatasettArbeid { get; set; }
        public ICollection<FdvrundeDistribusjon> FdvrundeDistribusjon { get; set; }
    }
}
