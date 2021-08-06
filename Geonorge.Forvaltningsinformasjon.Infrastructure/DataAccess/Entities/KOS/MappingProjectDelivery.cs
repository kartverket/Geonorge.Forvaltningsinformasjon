using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class MappingProjectDelivery
    {
        public int Id { get; set; }
        public int? Active { get; set; }

        public string Name { get; set; }
        public string Deadline { get; set; }
        public string ChangedDeadline { get; set; }
        public string FinalDeadline { get; set; }
        public string ReleaseDate { get; set; }

        public int TypeId { get; set; }
        public MappingProjectDeliveryType Type { get; set; }
        public int ProjectId { get; set; }
        public MappingProject Project { get; set; }
    }
}
