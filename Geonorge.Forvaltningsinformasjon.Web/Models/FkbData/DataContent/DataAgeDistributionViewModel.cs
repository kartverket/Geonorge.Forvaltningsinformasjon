using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities.Enums;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common;
using Geonorge.Forvaltningsinformasjon.Web.Models.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent
{
    public class DataAgeDistributionViewModel
    {
        public class Category
        {
            public string Name { get; set; }
            public string RgbColor { get; set; }
            public List<double> Percents { get; set; } = new List<double>();
        }

        private List<long> _sums = new List<long>();
        private Dictionary<AgeCategory,string> _ageCategoryColors = new Dictionary<AgeCategory, string>();

        public Dictionary<string, string> DataSets { get; set; } = new Dictionary<string, string>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string BarLabels { get; set; }

        public string AdministrativeUnitName { get; set; }
        public AdministrativeUnitType Type { get; set; }
        public string MetadataUrl { get; set; }
        public MapViewModel MapViewModel { get; set; } = new MapViewModel();

        public DataAgeDistributionViewModel(
            List<IDataAgeDistribution> distributions, 
            Dictionary<string,string> ageCategoryColors,
            Dictionary<string, string> dataSetToLayer)
        {
            InitColors(ageCategoryColors);

            distributions.ForEach(d => {
                DataSets.Add($"{d.DataSetName} ({d.ObjectCount} objekter)", dataSetToLayer[d.DataSetName]);
                BarLabels += "'',";
                _sums.Add(GetSum(d));
                });

            IEnumerable<AgeCategory> enums = Enum.GetValues(typeof(AgeCategory)).Cast<AgeCategory>();

            foreach (AgeCategory enumValue in enums)
            {
                Categories.Add(GetCategory(enumValue, distributions));
            }
        }

        private Category GetCategory(AgeCategory ageCategory, List<IDataAgeDistribution> distributions)
        {
            Category category = new Category
            {
                Name = GetAgeCategoryName(ageCategory),
                RgbColor = _ageCategoryColors[ageCategory]
            };

            for (int i = 0; i < distributions.Count; ++i)
            {
                var perCent = ((double)distributions[i].TransactionCounts[ageCategory] / _sums[i]) * 100;
                if (Double.IsNaN(perCent))
                    perCent = 0;
                category.Percents.Add(perCent);
            }
            return category;
        }

        private string GetAgeCategoryName (AgeCategory ageCategory)
        {
            string name;
            int year = DateTime.Now.Year;

            switch (ageCategory)
            {
                case AgeCategory.Year0:
                    name = $"{year}";
                    break;
                case AgeCategory.Year1:
                    name = $"{year - 1}";
                    break;
                case AgeCategory.Year2:
                    name = $"{year - 2}";
                    break;
                case AgeCategory.Year3:
                    name = $"{year - 3}";
                    break;
                case AgeCategory.Year4:
                    name = $"{year - 4}";
                    break;
                case AgeCategory.Years5To9:
                    name = $"{year - 9}-{year -5}";
                    break;
                case AgeCategory.Years10To19:
                    name = $"{year - 19}-{year - 10}";
                    break;
                default:
                    name = "Eldre";
                    break;
            }
            return name;
        }

        private long GetSum(IDataAgeDistribution distribution)
        {
            long sum = 0;

            foreach (KeyValuePair<AgeCategory, long> count in distribution.TransactionCounts)
            {
                sum += count.Value;
            }
            return sum;
        }

        private void InitColors(Dictionary<string, string> colors)
        {
           foreach (KeyValuePair<string,string> color in colors)
            {
                AgeCategory ageCategory; 
                
                Enum.TryParse(color.Key, out ageCategory);
                _ageCategoryColors.Add(ageCategory, color.Value);
            }
        }
    }
}
