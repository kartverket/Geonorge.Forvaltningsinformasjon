using Geonorge.Forvaltningsinformasjon.Web.Models.Common;

namespace Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers
{
    public interface IContextViewModelHelper
    {
        ContextViewModel Create(string selectedKey = "");
        bool IsCounty(string key);
        int Key2Id(string key);
        string Id2Key(int id, bool isCounty);
    }
}
