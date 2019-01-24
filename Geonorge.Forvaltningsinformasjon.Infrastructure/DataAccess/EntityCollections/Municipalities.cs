using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess;
using Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.DataAccess.EntityCollections
{
    class Municipalities : Entities<IMunicipality, Kommune>, IMunicipalities
    {
        public Municipalities(FDV_Drift2Context dbContext) : base(dbContext)
        {

        }

        public List<IMunicipality> GetByCounty(int id)
        {
            return _dbContext.Set<Kommune>().Where(k => k.FylkeFylkesnrNavigation.Id == id).Include(k => k.SentralFkb).AsEnumerable<IMunicipality>().ToList();
        }

        public override IMunicipality Get(int id)
        {
            return _dbContext.Set<Kommune>().Where(k => k.Id == id).Include(k => k.SentralFkb).First();
        }
    }
}
