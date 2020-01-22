using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services
{
    public interface IGeoJsonService
    {
        string GetFileName();
        string GetFileNameCounty(int id);
        string GetFileNameByMunicipality(int id);
    }
}
