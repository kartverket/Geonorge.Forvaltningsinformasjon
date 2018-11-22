using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management
{
    public class ContextViewModel
    {
        public enum EnumAspect
        {
            Management,
            ActivityOverview,
            TransactionData,
            OperatingState
        }

        public Dictionary<string, string> Counties { get; }
        public Dictionary<string, string> Municipalities { get;  }
        public string SelectedKey { get; set; }

        public EnumAspect Aspect { get; set; }

        public ContextViewModel()
        {
            Counties = new Dictionary<string, string>();
            Municipalities = new Dictionary<string, string>();
        }
    }
}
