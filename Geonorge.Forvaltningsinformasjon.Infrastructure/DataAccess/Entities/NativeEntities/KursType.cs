using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class KursType
    {
        public KursType()
        {
            Kurs = new HashSet<Kurs>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Beskrivelse { get; set; }

        public ICollection<Kurs> Kurs { get; set; }
    }
}
