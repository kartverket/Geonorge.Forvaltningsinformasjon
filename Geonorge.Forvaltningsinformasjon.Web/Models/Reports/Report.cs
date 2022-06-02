using System;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.Reports
{
    public class Report
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Beskrivelse { get; set; }

        public string FylkesNummer { get; set; }
        public string FylkesNavn { get; set; }
        public DateTime? Tidspunkt { get; set; }
        public string HTMLRapport { get; set; }
        public string KommuneNummer { get; internal set; }
        public string KommuneNavn { get; internal set; }
    }
}
