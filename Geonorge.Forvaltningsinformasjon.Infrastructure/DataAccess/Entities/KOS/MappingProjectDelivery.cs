using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class MappingProjectDelivery
    {
        public int Id { get; set; }
        public int? Active { get; set; }

        public int TypeId { get; set; }
        public int ProjectId { get; set; }
        public MappingProject Project { get; set; }
    }
}
