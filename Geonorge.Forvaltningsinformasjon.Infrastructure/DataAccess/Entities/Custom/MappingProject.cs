using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom
{
    internal class MappingProject : IMappingProject
    {
        public string Name { get; set; }

        public IOffice Office { get; set; }

        public int Year { get; set; }

        public MappingProjectState State { get; set; }

        public int Id { get; set; }

        public int? Active { get; set; }

        public List<IMunicipality> Municipalities { get; set; } = new List<IMunicipality>();
        public List<IMappingProjectDelivery> Deliveries { get; set; } = new List<IMappingProjectDelivery>();
    }
}
