using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class Office : IOffice
    {
        public int Id { get; set; }
        public int? Active { get; set; }
        public string Name { get; set; }
        public ICollection<MappingProject> MappingProjects { get; set; }
    }
}
