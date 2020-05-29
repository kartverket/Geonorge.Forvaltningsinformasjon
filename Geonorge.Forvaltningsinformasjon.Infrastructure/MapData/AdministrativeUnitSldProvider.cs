using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    using CssParam = KeyValuePair<string, string>;

    internal class AdministrativeUnitSldProvider : SldProvider, IAdministrativeUnitSldProvider
    {
        private const string _ServiceName = "adm_enheter2";
        private const string _StrokeWidthForCounty = "2.00";
        private const string _StrokeWidthForMunicipality = "1.00";

        public AdministrativeUnitSldProvider(IWmsUrlProvider wmsUrlProvider) : base(_ServiceName, wmsUrlProvider)
        {

        }
        public string GetSld()
        {
            XmlDocument xmlDocument = GetStyleXml(new List<string>() { "fylker_gjel", "kommuner_gjel" });
            
            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDocument.NameTable);
            ns.AddNamespace("ns", "http://www.opengis.net/sld");

            XmlNodeList names = xmlDocument.SelectNodes("//ns:NamedLayer/ns:Name", ns);

            foreach (XmlNode name in names)
            {
                XmlNodeList strokeStyles = (name.ParentNode as XmlElement).GetElementsByTagName("Stroke");
                
                if (name.InnerText == "kommuner_gjel")
                {
                    IterateNodeList(strokeStyles, new CssParam("stroke-width", _StrokeWidthForMunicipality));
                }
                else if (name.InnerText == "fylker_gjel")
                {
                    IterateNodeList(strokeStyles, new CssParam("stroke-width", _StrokeWidthForCounty));
                }
            }

            StringWriter writer = new StringWriter();

            xmlDocument.Save(writer);
            return writer.ToString();
        }
    }
}
