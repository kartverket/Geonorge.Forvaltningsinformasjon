using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities.Kos
{
    internal class County : ICounty
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

        public int? BBoxSouthWestN { get; set; }
        public int? BBoxSouthWestE { get; set; }
        public int? BBoxNorthEastN { get; set; }
        public int? BBoxNorthEastE { get; set; }
        public ICollection<Municipality> Municipalities { get; set; }
        public int? Active { get; set; }

        public County()
        {
            Municipalities = new HashSet<Municipality>();
        }
    }
}
