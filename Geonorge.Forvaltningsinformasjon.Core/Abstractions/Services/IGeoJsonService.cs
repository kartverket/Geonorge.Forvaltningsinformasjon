using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IGeoJsonService
    {
        string GetPath();
        string GetPathByCounty(int id);
        string GetPathByMunicipality(int id);
    }
}
