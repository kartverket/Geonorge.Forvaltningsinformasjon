using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    using CssParam = KeyValuePair<string, string>;

    internal class DataQualityClassificationSldProvider : SldProvider, IDataQualityClassificationSldProvider
    {
        private const string _ServiceName = "georef3";
        private const string _FillOpacity = "0.2";
        private const string _StrokeWidth = "0.2";
        public DataQualityClassificationSldProvider(IWmsUrlProvider wmsUrlProvider): base(_ServiceName, wmsUrlProvider)
        {

        }

        public string GetSld()
        {
            XmlDocument xmlDocument = GetStyleXml(new List<string>() { "fkb_abcd" });
            XmlNodeList fillStyles = xmlDocument.GetElementsByTagName("Fill");
            XmlNodeList strokeStyles = xmlDocument.GetElementsByTagName("Stroke");

            IterateNodeList(fillStyles, new CssParam("fill-opacity", _FillOpacity));
            IterateNodeList(strokeStyles, new CssParam("stroke-width", _StrokeWidth));

            StringWriter writer = new StringWriter();

            xmlDocument.Save(writer);
            return writer.ToString();             
        }

        public Dictionary<string, ILegendItemStyle> GetLegendItemStyles()
        {
            Dictionary<string, ILegendItemStyle> legendItemStyles = new Dictionary<string, ILegendItemStyle>();
            List<string> layerNames = new List<string>() { "fkb_ABCD" };

            XmlDocument xmlDocument = GetStyleXml(layerNames);
            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDocument.NameTable);
            ns.AddNamespace("ns", "http://www.opengis.net/sld");

            XmlNodeList names = xmlDocument.SelectNodes("//ns:Rule/ns:Name", ns);

            foreach (XmlNode name in names)
            {
                LegendItemStyle layerStyle = new LegendItemStyle();
                layerStyle.FillColor = name.ParentNode.SelectSingleNode("descendant::*[@name='fill']").InnerText;
                layerStyle.StrokeColor = name.ParentNode.SelectSingleNode("descendant::*[@name='stroke']").InnerText;
                layerStyle.FillOpacity = _FillOpacity;
                layerStyle.StrokeWidth = _StrokeWidth;

                legendItemStyles.Add(name.InnerText, layerStyle);
            }

            return legendItemStyles;
        }
    }
}
