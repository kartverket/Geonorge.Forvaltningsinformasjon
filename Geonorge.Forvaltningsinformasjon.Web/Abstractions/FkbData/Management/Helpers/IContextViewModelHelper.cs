using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers
{
    public interface IContextViewModelHelper
    {
        ContextViewModel Create(string selectedKey = "");
        bool IsCounty(string key);
        int Key2Id(string key);
        string Id2Key(int id, bool isCounty);
    }
}
