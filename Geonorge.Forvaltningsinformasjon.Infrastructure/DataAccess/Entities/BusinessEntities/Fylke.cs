using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities
{
    public partial class Fylke : ICounty
    {
        public string Number
        {
            get
            {
                return Fylkesnr;
            }
        }

        public string Name
        {
            get
            {
                return Fylkesnavn;
            }
        }
        public int MunicipalityCount
        {
            get
            {
                return Kommune.Count;
            }
        }
        public int DirectUpdateCount
        {
            get
            {
                return Kommune.Where(k => k.SentralFkb.First().DirekteoppdateringInfort != null).Count();
            }
        }

        private int _id = 0;
        public int Id
        {
            get
            {
                return _id == 0 ? _id = int.Parse(Fylkesnr) : _id;
            }
        }
    }
}
