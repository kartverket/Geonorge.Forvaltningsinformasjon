using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Entities;
using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management.Helpers
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
            municipalities.ForEach(m => viewModel.Municipalities.Add($"M{m.Id}", m.Name));

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