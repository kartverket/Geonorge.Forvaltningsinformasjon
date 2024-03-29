﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class MappingProjectDeliveryType
    {
        public int Id { get; set; }
        public int? Active { get; set; }

        public string Name { get; set; }

        public ICollection<MappingProjectDelivery> Deliveries { get; set; }
    }
}
