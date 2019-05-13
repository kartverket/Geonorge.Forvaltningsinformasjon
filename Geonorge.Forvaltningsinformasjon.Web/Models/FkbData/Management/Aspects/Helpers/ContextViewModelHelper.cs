using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Aspects.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Aspects.Helpers
{
    public class ContextViewModelHelper : IContextViewModelHelper
    {
        ICountyService _countyService;
        IMunicipalityService _municipalityService;

        public ContextViewModelHelper(ICountyService countyService, IMunicipalityService municipalityService)
        {
            _countyService = countyService;
            _municipalityService = municipalityService;
        }

        public ContextViewModel Create(string selectedKey = "")
        {
            ContextViewModel viewModel = new ContextViewModel();

            List<ICounty> counties = _countyService.Get();
            List<IMunicipality> municipalities = _municipalityService.Get();

            counties.Sort((x, y) => x.Name.CompareTo(y.Name));
            municipalities.Sort((x, y) => x.Name.CompareTo(y.Name));

            counties.ForEach(c => viewModel.Counties.Add($"F{c.Id}", c.Name));

            int count = municipalities.Count;
            CultureInfo cultureInfo = new System.Globalization.CultureInfo("nb-NO");

            for (int i = 0; i < municipalities.Count; ++i)
            {
                if (i < count - 1 && municipalities[i].Name == municipalities[i + 1].Name)
                {
                    string county1 = _countyService.GetByMunicipalityId(municipalities[i].Id).Name;
                    string county2 = _countyService.GetByMunicipalityId(municipalities[i + 1].Id).Name;

                    if (string.Compare(county1, county2, cultureInfo, CompareOptions.None) > 0)
                    {
                        viewModel.Municipalities.Add($"M{municipalities[i + 1].Id}", $"{municipalities[i + 1].Name} i {county2}");
                        viewModel.Municipalities.Add($"M{municipalities[i].Id}", $"{municipalities[i].Name} i {county1}");
                    }
                    else
                    {
                        viewModel.Municipalities.Add($"M{municipalities[i].Id}", $"{municipalities[i].Name} i {county1}");
                        viewModel.Municipalities.Add($"M{municipalities[i + 1].Id}", $"{municipalities[i + 1].Name} i {county2}");
                    }
                    ++i;
                }
                else
                {
                    viewModel.Municipalities.Add($"M{municipalities[i].Id}", municipalities[i].Name);
                }
            }
            viewModel.SelectedKey = selectedKey;

            return viewModel;
        }

        public bool IsCounty(string key)
        {
            return key[0] == 'F';
        }

        public int Key2Id(string key)
        {
            return int.Parse(key.Remove(0, 1));
        }

        public string Id2Key(int id, bool isCounty)
        {
            if (id > 0)
            {
                return isCounty ? $"F{id}" : $"M{id}";
            }
            return "";
        }
    }
}