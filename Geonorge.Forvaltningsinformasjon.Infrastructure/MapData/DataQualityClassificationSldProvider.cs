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
    }
}
