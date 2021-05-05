using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class Office
    {
        public int Id { get; set; }
        public int Active { get; set; }
        public string Name { get; set; }
        public ICollection<MappingProject> MappingProjects { get; set; }
    }
}
