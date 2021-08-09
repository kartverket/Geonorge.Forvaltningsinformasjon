using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class MappingProjectActivity
    {
        public enum ActivityType
        {
            STARTED = 1,
            COMPLETED = 15
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public MappingProject Project {get;set; }
        public ActivityType Activity { get; set; }
        public string Date { get; set; }
    }
}
