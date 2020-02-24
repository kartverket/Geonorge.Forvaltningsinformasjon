using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.Common
{
    public class MapViewModel
    {
        private double _k1 = 550000.0;
        private double _k2 = 1.304347826086957e-5;

        public class Service
        {
            public string Title { get; set; }
            public string ServiceType { get; set; }
            public string Url { get; set; }
            public Dictionary<string,string> CustomParameters { get; set; }
            public List<string> Layers { get; set; }

            public Service(string title, string serviceType, string url, List<string> layers, Dictionary<string, string> customParameters = null)
            {
                Title = title;
                ServiceType = serviceType;
                Url = url;
                CustomParameters = customParameters;
                Layers = layers;
            }
        }

        public const string ViewHeight = "500px";

        public string Latitude { get; set; } = "7248546.391464977";
        public string Longitude { get; set; } = "444437.4530454726";
        public string Zoom { get; set; } = "2";
        public string CoordinateSystem { get; set; } = "EPSG:25833";

        public List<Service> Services { get; set; } = new List<Service>();

        public MapViewModel()
        {

        }

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

        public void AddService(string serviceType, string url, string layer, Dictionary<string, string> customParameters = null)
        {
            Services.Add(new Service("", serviceType, url, new List<string> { layer }, customParameters));
        }
        public void AddService(string serviceType, string url, List<string> layers, Dictionary<string, string> customParameters = null)
        {
            Services.Add(new Service("", serviceType, url, layers, customParameters));
        }
    }
}
