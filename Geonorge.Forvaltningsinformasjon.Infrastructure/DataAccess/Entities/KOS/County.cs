using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Custom;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos
{
    internal class County : BoundingBox, ICounty
    {
        private int _id = 0;

        #region ICounty
        public int Id
        {
            get
            {
                return _id == 0 ? _id = int.Parse(Number) : _id;
            }
        }
        public string Number { get; set; }
        public string Name { get; set; }
        public int MunicipalityCount
        {
            get
            {
                return Municipalities.Count;
            }
        }
        public int DirectUpdateCount
        {
            get
            {
                return Municipalities.Where(k => k.CentralFkb.First().DirectUpdateInroduced != null).Count();
            }
        }

        #endregion

        public ICollection<Municipality> Municipalities { get; set; }
        public int? Active { get; set; }

        public County()
        {
            Municipalities = new HashSet<Municipality>();
        }
    }
}
