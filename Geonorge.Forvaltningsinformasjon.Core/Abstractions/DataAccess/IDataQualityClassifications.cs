﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.DataAccess
{
    public interface IDataQualityClassifications
    {
        List<IDataQualityClassification> Get();
        List<IDataQualityClassification> GetByCounty(int id);
        List<IDataQualityClassification> GetByMunicipality(int id);
    }
}
