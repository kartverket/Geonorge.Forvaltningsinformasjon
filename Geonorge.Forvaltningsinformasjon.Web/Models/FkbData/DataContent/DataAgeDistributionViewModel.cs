using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.DataContent
{
    public class DataAgeDistributionViewModel
    {
        public enum AdministrativeUnitType
        {
            Country,
            County,
            Municipality
        }

        public class Category
        {
            public string Name { get; set; }
            public string RgbColor { get; set; }
            public List<double> Percents { get; set; } = new List<double>();
        }

        private List<long> _sums = new List<long>();
        private Dictionary<AgeCategory,string> _ageCategoryColors = new Dictionary<AgeCategory, string>();

        public string DataSetNames { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        public string AdministrativeUnitName { get; set; }
        public AdministrativeUnitType Type { get; set; }


        public DataAgeDistributionViewModel(List<IDataAgeDistribution> distributions, Dictionary<string,string> ageCategoryColors)
        {
            InitColors(ageCategoryColors);

            distributions.ForEach(d => {
                DataSetNames += $"'{d.DataSetName}',";
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
                long db = distributions[i].TransactionCounts[ageCategory];
                long db2 = _sums[i];
                double db3 = db2 != 0.0 ? (double)db / db2 : 0;
                double db4 = db3 * 100;
                category.Percents.Add(db4);
                //category.Percents.Add(((double)distributions[i].TransactionCounts[ageCategory] / _sums[i]) * 100);
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
