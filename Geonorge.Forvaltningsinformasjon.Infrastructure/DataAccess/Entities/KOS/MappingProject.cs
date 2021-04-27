using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class MappingProject
    {
        public int Id { get; set; }
        public int? Active { get; set; }
        public int OfficeNumber { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
