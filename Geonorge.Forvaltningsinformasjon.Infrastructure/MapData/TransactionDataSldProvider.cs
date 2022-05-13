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
        public Dictionary<string, ILegendItemStyle> GetLegendItemStyles(List<ITransactionData> transactionData)
        {
            Dictionary<string, ILegendItemStyle> legendItemStyles = new Dictionary<string, ILegendItemStyle>();
            List<string> layerNames = new List<string>();

            transactionData.ForEach(td => layerNames.Add($"{_dataSetToLayerMap[td.DataSetName]}_N1"));

            XmlDocument xmlDocument = GetStyleXml(layerNames);
            XmlNodeList fillStyles = xmlDocument.GetElementsByTagName("Fill");
            XmlNodeList strokeStyles = xmlDocument.GetElementsByTagName("Stroke");

            for (int i = 0; i < transactionData.Count; ++i)
            {
                LegendItemStyle layerStyle = new LegendItemStyle();

                if(fillStyles.Count > 0 && layerStyle?.FillColor != null) { 
                layerStyle.FillColor = fillStyles[i].SelectSingleNode("*[@name='fill']")?.InnerText;
                layerStyle.FillOpacity = fillStyles[i].SelectSingleNode("*[@name='fill-opacity']")?.InnerText;
                }
                if(strokeStyles.Count > 0 && layerStyle?.FillColor != null) { 
                layerStyle.StrokeColor = strokeStyles[i].SelectSingleNode("*[@name='stroke']")?.InnerText;
                layerStyle.StrokeWidth = strokeStyles[i].SelectSingleNode("*[@name='stroke-width']")?.InnerText;
                }

                if(!legendItemStyles.ContainsKey(transactionData[i].DataSetName))
                    legendItemStyles.Add(transactionData[i].DataSetName, layerStyle);
            }
            return legendItemStyles;
        }
    }
}
