using System;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class SentralFkbStatistikk
    {
        public int Id { get; set; }
        public string KommuneKommunenr { get; set; }
        public int DatasettId { get; set; }
        public int? AntObjekter { get; set; }
        public int? AntTransUke { get; set; }
        public int? AntTransMnd { get; set; }
        public int? AntTransAr { get; set; }
        public DateTime? AntTransOppdateringsdato { get; set; }
        public DateTime? GeonorgeFildato { get; set; }
        public DateTime? GeonorgeOppdateringsdato { get; set; }
        public Datasett DatasettIdNavigation { get; set; }
    }
}