using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers;
using System.Collections.Generic;

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

            _countyService.Get().ForEach(c => viewModel.Counties.Add($"F{c.Id}", c.Name));
            _municipalityService.Get().ForEach(m => viewModel.Municipalities.Add($"M{m.Id}", m.Name));

            viewModel.SelectedKey = selectedKey;

            return viewModel;
        }

        public bool IsCounty(string key)
        {
            return key[0] == 'F';
        }

        public string Key2Id(string key)
        {
            return key.Remove(0, 1);
        }

        public string Id2Key(string id, bool isCounty)
        {
            return isCounty ? $"F{id}" : $"M{id}";
        }
    }
}