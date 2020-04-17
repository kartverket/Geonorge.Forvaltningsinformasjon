using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.MapData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Geonorge.Forvaltningsinformasjon.Infrastructure.MapData
{
    internal class TransactionDataSldProvider : SldProvider, ITransactionDataSldProvider
    {
        private const string _ServiceName = "sfkb-transaksjoner";
        Dictionary<string, string> _dataSetToLayerMap;

        public TransactionDataSldProvider(InfrastructureSettings settings, IWmsUrlProvider wmsUrlProvider) : base(_ServiceName, wmsUrlProvider)
        {
            _dataSetToLayerMap = settings.DataSetToLayerMap;
        }
        public Dictionary<string, ILayerStyle> GetLayerStyles(List<ITransactionData> transactionData)
        {
            Dictionary<string, ILayerStyle> layerStyles = new Dictionary<string, ILayerStyle>();
            List<string> layerNames = new List<string>();

            transactionData.ForEach(td => layerNames.Add($"{_dataSetToLayerMap[td.DataSetName]}_N1"));

            XmlDocument xmlDocument = GetStyleXml(layerNames);
            XmlNodeList fillStyles = xmlDocument.GetElementsByTagName("Fill");
            XmlNodeList strokeStyles = xmlDocument.GetElementsByTagName("Stroke");

            for (int i = 0; i < transactionData.Count; ++i)
            {
                LayerStyle layerStyle = new LayerStyle();

                layerStyle.FillColor = fillStyles[i].SelectSingleNode("*[@name='fill']").InnerText;
                layerStyle.FillOpacity = fillStyles[i].SelectSingleNode("*[@name='fill-opacity']").InnerText;
                layerStyle.StrokeColor = strokeStyles[i].SelectSingleNode("*[@name='stroke']").InnerText;
                layerStyle.StrokeWidth = strokeStyles[i].SelectSingleNode("*[@name='stroke-width']").InnerText;

                layerStyles.Add(transactionData[i].DataSetName, layerStyle);
            }
            return layerStyles;
        }
    }
}
