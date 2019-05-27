using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Fdvdatasett : IDataSet
    {
        private DateTime? _updateDate;
        private bool _isEmpty;

        public string Name
        {
            get
            {
                return Datasett.Navn;
            }
        }

        public IDataSetUpdateType UpdateType
        {
            get
            {
                return FdvdatasettForvaltningstype;
            }
        }

        [NotMapped]
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
            }
        }

        [NotMapped]
        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
            }
        }
    }
}
