namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class CentralFkb
    {
        public int Id { get; set; }
        public string MunicipalitzNumber { get; set; }
        public string PlannedIntroduction { get; set; }
        public string DirectUpdateInroduced { get; set; }

        public Municipality Municipality { get; set; }
    }
}
