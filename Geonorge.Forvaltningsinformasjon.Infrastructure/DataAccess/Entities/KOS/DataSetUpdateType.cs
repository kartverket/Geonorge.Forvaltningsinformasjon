using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.KOS
{
    internal class DataSetUpdateType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<FdvDataSet> FdvDataSet { get; set; }

        public DataSetUpdateType()
        {
            FdvDataSet = new HashSet<FdvDataSet>();
        }
    }
}
