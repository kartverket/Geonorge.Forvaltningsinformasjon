﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class MappingProject
    {
        public int Id { get; set; }
        public int? Active { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public ICollection<MappingProjectMunicipalityLink> MappingProjectMunicipalityLinks { get; set; }
        public ICollection<MappingProjectDelivery> Deliveries { get; set; }
        public ICollection<MappingProjectActivity> ProjectActivities { get; set; }
    }
}
