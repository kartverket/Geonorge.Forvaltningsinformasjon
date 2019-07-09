using System.Collections.Generic;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.Common
{
    public class ContextViewModel
    {
        public Dictionary<string, string> Counties { get; }
        public Dictionary<string, string> Municipalities { get;  }
        public string SelectedKey { get; set; }

        public ContextViewModel()
        {
            Counties = new Dictionary<string, string>();
            Municipalities = new Dictionary<string, string>();
        }
    }
}
