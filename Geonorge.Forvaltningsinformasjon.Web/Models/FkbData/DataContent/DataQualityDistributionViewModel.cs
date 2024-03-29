﻿using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent
{
    public class DataQualityDistributionViewModel
    {
        public class Category
        {
            public string Name { get; set; }
            public string RgbColor { get; set; }
            public List<double> Percents { get; set; } = new List<double>();
        }

        private List<long> _sums = new List<long>();
        private Dictionary<QualityCategory, string> _qualityCategoryColors = new Dictionary<QualityCategory, string>();

        public Dictionary<string, string> DataSets { get; set; } = new Dictionary<string, string>();
        public List<string> LayerNames { get; set; } = new List<string>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string BarLabels { get; set; }

        public string AdministrativeUnitName { get; set; }
        public AdministrativeUnitType Type { get; set; }
        public string MetadataUrl { get; set; }

        public MapViewModel MapViewModel { get; set; } = new MapViewModel();

        public DataQualityDistributionViewModel(
            List<IDataQualityDistribution> distributions, 
            Dictionary<string, string> qualityCategoryColors,
            Dictionary<string, string> dataSetToLayer)
        {
            InitColors(qualityCategoryColors);

            distributions.ForEach(d => {     
                DataSets.Add($"{d.DataSetName} ({d.ObjectCount} objekter)", dataSetToLayer[d.DataSetName]);
                BarLabels += "'',";
                _sums.Add(GetSum(d));
            });

            IEnumerable<QualityCategory> enums = Enum.GetValues(typeof(QualityCategory)).Cast<QualityCategory>();

            foreach (QualityCategory enumValue in enums)
            {
                Categories.Add(GetCategory(enumValue, distributions));
            }
        }

        private Category GetCategory(QualityCategory qualityCategory, List<IDataQualityDistribution> distributions)
        {
            Category category = new Category
            {
                Name = GetQualityCategoryName(qualityCategory),
                RgbColor = _qualityCategoryColors[qualityCategory]
            };

            for (int i = 0; i < distributions.Count; ++i)
            {
                var perCent = ((double)distributions[i].TransactionCounts[qualityCategory] / _sums[i]) * 100;
                if (Double.IsNaN(perCent))
                    perCent = 0;
                category.Percents.Add(perCent);
            }
            return category;
        }

        private string GetQualityCategoryName(QualityCategory qualityCategory)
        {
            string name;
            int year = DateTime.Now.Year;

            switch (qualityCategory)
            {
                case QualityCategory.Measured:
                    name = "Terrengmålt";
                    break;
                case QualityCategory.PhotogrammetricB:
                    name = "Fotogrammetrisk <=40cm";
                    break;
                case QualityCategory.PhotogrammetricC:
                    name = "Fotogrammetrisk >40cm";
                    break;
                case QualityCategory.DigitalizedM200:
                    name = "Digitalisert <=200cm";
                    break;
                case QualityCategory.DigitalizedS200:
                    name = "Digitalisert >200cm";
                    break;
                default:
                    name = "Ikke målt";
                    break;
            }
            return name;
        }

        private long GetSum(IDataQualityDistribution distribution)
        {
            long sum = 0;

            foreach (KeyValuePair<QualityCategory, long> count in distribution.TransactionCounts)
            {
                sum += count.Value;
            }
            return sum;
        }

        private void InitColors(Dictionary<string, string> colors)
        {
            foreach (KeyValuePair<string, string> color in colors)
            {
                QualityCategory qualityCategory;

                Enum.TryParse(color.Key, out qualityCategory);
                _qualityCategoryColors.Add(qualityCategory, color.Value);
            }
        }
    }
}
