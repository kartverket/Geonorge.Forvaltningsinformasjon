using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class FdvdatasettForvaltningstype : IDataSetUpdateType
    {
        public string Name
        {
            get
            {
                return Type;
            }
        }

        public string Description
        {
            get
            {
                return Beskrivelse;
            }
        }
    }
}
