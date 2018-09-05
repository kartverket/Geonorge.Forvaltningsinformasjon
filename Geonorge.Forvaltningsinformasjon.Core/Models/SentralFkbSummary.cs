using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public class SentralFkbSummary
    {
        public List<SentralFkbSummaryLine> Result { get; set; } = new List<SentralFkbSummaryLine>();
    }

    public class SentralFkbSummaryLine
    {
        public string Fylke { get; set; }
        public int AntallDirekteOppdatering { get; set; } 
        public int AntallKommunerTotalt { get; set; }
    }    
}