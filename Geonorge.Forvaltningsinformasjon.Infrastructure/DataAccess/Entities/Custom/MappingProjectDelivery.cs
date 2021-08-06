using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom
{
    internal class MappingProjectDelivery : IMappingProjectDelivery
    {
        public string Name { get; set; }
        public RelevantMappingProjectDeliveryType Type { get; set; }
        public string Deadline { get; set; }
        public string ChangedDeadline { get; set; }
        public string FinalDeadline { get; set; }
        public string ReleaseDate { get; set; }
    }
}
