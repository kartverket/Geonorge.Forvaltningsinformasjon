using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class Round
    {
        public Round()
        {
        }

        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? Active { get; set; }

        public Project Project { get; set; }
    }
}
