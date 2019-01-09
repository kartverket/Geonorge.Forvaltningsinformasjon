using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Area
{
    public class MunicipalityViewModel
    {
        public List<IDataSet> DataSets { get; set; }
        public string Caption { get; set; }
        public DateTime? DateTime { get; set; }

        public string GetUpdateTypeText(UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.Direct:
                    return "Direkteoppdatering";
                case UpdateType.Historical:
                    return "Historisk datasett";
                case UpdateType.Sosi:
                    return "SOSI-originaldata";
                case UpdateType.SosiChangesOnly:
                    return "SOSI-endringsdata";
                case UpdateType.Sync:
                    return "Synkronisering";
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public string GetToolTip(UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.Direct:
                    return "Direkteoppdatering i Sentral Felles KartdataBase (SFKB)";
                case UpdateType.Historical:
                    return "FKB-datasett som er historisk";
                case UpdateType.Sosi:
                    return "Kommunen har originaldata som periodisk sendes inn til Kartverket for distribusjon";
                case UpdateType.SosiChangesOnly:
                    return "Kartverket har originalen og mottar endringsdata på SOSI-format periodisk fra andre parter.";
                case UpdateType.Sync:
                    return "Kommunen har originaldata som synkroniseres til Kartverket for distribusjon";
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
