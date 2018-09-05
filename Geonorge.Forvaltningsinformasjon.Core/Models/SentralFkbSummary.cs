using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public class SentralFkbSummary
    {
        public List<SentralFkbSummaryLine> Result { get; set; } = new List<SentralFkbSummaryLine>();

        public int TotaltAntallKommunerDirekteOppdatering()
        {
            return Result.Sum(r => r.AntallDirekteOppdatering);
        }
    }

    public class SentralFkbSummaryLine
    {
        public Fylke Fylke { get; set; }
        public int AntallDirekteOppdatering { get; set; } 
        public int AntallKommunerTotalt { get; set; }
    }    
}