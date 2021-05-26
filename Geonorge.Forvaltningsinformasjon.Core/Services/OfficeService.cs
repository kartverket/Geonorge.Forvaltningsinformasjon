using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Services
{
    internal class OfficeService : IOfficeService
    {
        private IRepository _repository;

        public OfficeService(IRepository repository)
        {
            _repository = repository;
        }

        public List<IOffice> Get()
        {
            return _repository.Offices.Get();
        }

        public IOffice Get(int id)
        {
            return _repository.Offices.Get(id);
        }
    }
}
