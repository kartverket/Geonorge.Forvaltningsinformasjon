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
        private string _fillOpacity = "0.2";
        private string _strokeWidth = "0.2";
        public DataQualityClassificationSldProvider(InfrastructureSettings settings): base(settings.GeorefStyle)
        {

        }
        public string GetSld()
        {
            XmlDocument xmlDocument = GetStyleXml();
            XmlNodeList fillStyles = xmlDocument.GetElementsByTagName("Fill");
            XmlNodeList strokeStyles = xmlDocument.GetElementsByTagName("Stroke");

            IterateNodeList(fillStyles, new CssParam("fill-opacity", _fillOpacity));
            IterateNodeList(strokeStyles, new CssParam("stroke-width", _strokeWidth));

            StringWriter writer = new StringWriter();

            xmlDocument.Save(writer);
            return writer.ToString();             
        }

        private void IterateNodeList(XmlNodeList nodeList, CssParam cssParam)
        {
            foreach (XmlNode node in nodeList)
            {
                ModifyNode(node, cssParam);
            }
        }

        private void ModifyNode(XmlNode node, CssParam cssParam)
        {
            XmlNode cssParamNode = node.SelectSingleNode($"*[@name='{cssParam.Key}']");

            if (cssParamNode == null)
            {
                cssParamNode = node.OwnerDocument.CreateNode(XmlNodeType.Element, "CssParameter", node.OwnerDocument.DocumentElement.NamespaceURI);
                
                XmlAttribute nameAttribute = node.OwnerDocument.CreateAttribute("name");
                nameAttribute.Value = cssParam.Key;

                cssParamNode.Attributes.Append(nameAttribute);
                node.AppendChild(cssParamNode);
            }
            cssParamNode.InnerText = cssParam.Value;
        }
    }
}
