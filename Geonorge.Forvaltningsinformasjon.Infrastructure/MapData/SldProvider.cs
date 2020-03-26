using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    internal class SldProvider
    {
        private string _styleUrl;

        protected SldProvider(string styleUrl)
        {
            _styleUrl = styleUrl;
        }
        protected XmlDocument GetStyleXml(List<string> layers = null)
        {
            XmlDocument xmlDocument = new XmlDocument();
            string styles;

            string url = _styleUrl;

            if (layers != null)
            {
                url += string.Join(',', layers);
            }

            using (HttpClient client = new HttpClient())
            {
                styles = client.GetStringAsync(url).Result;
            }
            xmlDocument.LoadXml(styles);
            return xmlDocument;
        }
    }
}
