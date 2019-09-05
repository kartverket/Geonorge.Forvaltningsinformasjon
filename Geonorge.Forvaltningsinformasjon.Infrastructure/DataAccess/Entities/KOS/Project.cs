using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos
{
    internal class Project
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string MunicipalityNumber { get; set; }
        public string Name { get; set; }
        public int? Active { get; set; }

        public Municipality Municipality { get; set; }
        public ICollection<FdvDataSet> FdvDataSet { get; set; }
        public ICollection<Round> Round { get; set; }

        public Project()
        {
            FdvDataSet = new HashSet<FdvDataSet>();
            Round = new HashSet<Round>();
        }
    }
}
