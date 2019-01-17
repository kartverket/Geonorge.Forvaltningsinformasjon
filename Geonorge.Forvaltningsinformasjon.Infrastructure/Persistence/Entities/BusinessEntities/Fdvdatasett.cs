using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.Persistence.Entities
{
    public partial class Fdvdatasett : IDataSet
    {
        public string Name
        {
            get
            {
                return Datasett.Navn;
            }
        }

        public UpdateType UpdateType
        {
            get
            {
                return (UpdateType)(FdvdatasettForvaltningstypeId - 1);
            }
        }
    }
}
