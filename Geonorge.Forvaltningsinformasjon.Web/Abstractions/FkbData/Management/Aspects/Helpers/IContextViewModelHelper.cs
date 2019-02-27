using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects;

namespace Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Aspects.Helpers
{
    public interface IContextViewModelHelper
    {
        ContextViewModel Create(string selectedKey = "");
        bool IsCounty(string key);
        int Key2Id(string key);
        string Id2Key(int id, bool isCounty);
    }
}
