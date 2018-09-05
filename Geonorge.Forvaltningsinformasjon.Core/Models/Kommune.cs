using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class Kommune
    {
        public Kommune()
        {
            Distribusjon = new HashSet<Distribusjon>();
            Fdvnotat = new HashSet<Fdvnotat>();
            Fdvprosjekt = new HashSet<Fdvprosjekt>();
            PlanInfo = new HashSet<PlanInfo>();
            SentralFkb = new HashSet<SentralFkb>();
        }

        public string Kommunenr { get; set; }
        public string Kommunenavn { get; set; }
        public string KommunenavnDos { get; set; }
        public string FylkeFylkesnr { get; set; }
        public short? KoordsysKoordsys { get; set; }
        public string Vertdatum { get; set; }
        public int? BbSorVestN { get; set; }
        public int? BbSorVestE { get; set; }
        public int? BbNordOstN { get; set; }
        public int? BbNordOstE { get; set; }
        public string Fkboppdatering { get; set; }
        public string Tjenesteserver { get; set; }
        public string Forelopig { get; set; }
        public int? KartkontorNr { get; set; }
        public string Region { get; set; }
        public string WebKart { get; set; }
        public string SystemGis { get; set; }
        public int? Aktiv { get; set; }

        public Fylke FylkeFylkesnrNavigation { get; set; }
        public Kartkontor KartkontorNrNavigation { get; set; }
        public Koordsys KoordsysKoordsysNavigation { get; set; }
        public ICollection<Distribusjon> Distribusjon { get; set; }
        public ICollection<Fdvnotat> Fdvnotat { get; set; }
        public ICollection<Fdvprosjekt> Fdvprosjekt { get; set; }
        public ICollection<PlanInfo> PlanInfo { get; set; }
        public ICollection<SentralFkb> SentralFkb { get; set; }
    }
}
