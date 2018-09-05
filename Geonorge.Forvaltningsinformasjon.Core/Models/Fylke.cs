using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Core.Models
{
    public partial class Fylke
    {
        public Fylke()
        {
            Kommune = new HashSet<Kommune>();
        }

        public string Fylkesnr { get; set; }
        public string Fylkesnavn { get; set; }
        public string FylkesnavnDos { get; set; }
        public int? BbSorVestN { get; set; }
        public int? BbSorVestE { get; set; }
        public int? BbNordOstN { get; set; }
        public int? BbNordOstE { get; set; }

        public ICollection<Kommune> Kommune { get; set; }
    }
}
