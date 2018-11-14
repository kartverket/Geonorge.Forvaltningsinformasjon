using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Common;
using Geonorge.Forvaltningsinformasjon.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.FkbData.Management.Area;
using Geonorge.Forvaltningsinformasjon.Core.Entities.FkbData.Management.Area;

namespace Geonorge.Forvaltningsinformasjon.Core.Services.FkbData.Management.Area
{
    class CountyService : ICountyService
    {
        private IRepository _repository;

        public CountyService(IRepository repository)
        {
            _repository = repository;
        }

        public List<ICounty> GetAll()
        {
            return _repository.Get<Fylke>().Select(f => new County()
            {
                Number = f.Fylkesnr,
                Name = f.Fylkesnavn,
                MunicipalityCount = f.Kommune.Count,
                DirectUpdateCount = f.Kommune.Where(k => k.SentralFkb.First().DirekteoppdateringInfort != null).Count()
            } as ICounty).ToList();
        }
    }
}
