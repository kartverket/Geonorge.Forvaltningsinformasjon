using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    using CssParam = KeyValuePair<string, string>;

    internal class SldProvider
    {
        private string _serviceName;
        private IWmsUrlProvider _wmUrlProvider;

        protected SldProvider(string serviceName, IWmsUrlProvider wmsUrlProvider)
        {
            _serviceName = serviceName;
            _wmUrlProvider = wmsUrlProvider;
        }
        protected XmlDocument GetStyleXml(List<string> layers = null)
        {
            XmlDocument xmlDocument = new XmlDocument();
            string styles;

            string url = _wmUrlProvider.GetStyledLayerDescritpionUrl(_serviceName, layers);

            using (HttpClient client = new HttpClient())
            {
                styles = client.GetStringAsync(url).Result;
            }
            xmlDocument.LoadXml(styles);
            return xmlDocument;
        }

        protected void IterateNodeList(XmlNodeList nodeList, CssParam cssParam)
        {
            foreach (XmlNode node in nodeList)
            {
                ModifyNode(node, cssParam);
            }
        }

        protected void ModifyNode(XmlNode node, CssParam cssParam)
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
