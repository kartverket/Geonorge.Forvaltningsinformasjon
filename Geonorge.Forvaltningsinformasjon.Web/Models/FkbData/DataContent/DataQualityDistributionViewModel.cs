using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
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

        public string DataSetNames { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        public string AdministrativeUnitName { get; set; }
        public AdministrativeUnitType Type { get; set; }

        public DataQualityDistributionViewModel(List<IDataQualityDistribution> distributions, Dictionary<string, string> qualityCategoryColors)
        {
            InitColors(qualityCategoryColors);

            distributions.ForEach(d => {
                DataSetNames += $"'{d.DataSetName} ({d.ObjectCount} objekter)',";
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
                category.Percents.Add(((double)distributions[i].TransactionCounts[qualityCategory] / _sums[i]) * 100);
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
