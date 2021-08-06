
namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class MappingProjectMunicipalityLink
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public MappingProject Project { get; set; }
        public string MunicipalityNumber { get; set; }
        public Municipality Municipality { get; set; }
    }
}
