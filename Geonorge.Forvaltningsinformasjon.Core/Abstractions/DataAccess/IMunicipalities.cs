﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IMunicipalities : IEntities<IMunicipality>
    {
        List<IMunicipality> GetByCounty(int id);
    }
}
