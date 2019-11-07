using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.Common
{
    public class MapViewModel
    {
        private double _k1 = 550000.0;
        private double _k2 = 1.304347826086957e-5;
        public const string ViewHeight = "500px";

        public string Latitude { get; } = "7248546.391464977";
        public string Longitude { get; } = "444437.4530454726";
        public string Zoom { get; } = "2";
        public string CoordinateSystem { get; } = "EPSG:25833";

        public MapViewModel(IBoundingBox boundingBox = null)
        {
            if (boundingBox != null)
            {
                double lat = (boundingBox.MaxY + boundingBox.MinY) / 2.0;
                double lon = (boundingBox.MaxX + boundingBox.MinX) / 2.0;
                double zoom = (_k1 - (boundingBox.MaxX - boundingBox.MinX)) * _k2;

                NumberFormatInfo nfi = new NumberFormatInfo
                {
                    NumberDecimalSeparator = "."
                };

                Latitude = lat.ToString(nfi);
                Longitude = lon.ToString(nfi);
                Zoom = zoom.ToString(nfi);
                CoordinateSystem = boundingBox.CoordinateSystem;
            }
        }
    }
}
