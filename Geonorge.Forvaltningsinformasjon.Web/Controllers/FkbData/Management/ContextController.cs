using Geonorge.Forvaltningsinformasjon.Core.Abstractions.Services;
using Geonorge.Forvaltningsinformasjon.Web.Abstractions.FkbData.Management.Helpers;
using Geonorge.Forvaltningsinformasjon.Web.Models.FkbData.Management;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.FkbData.Management
{
    [Route("fkb-data/management/context")]
    public class ContextController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;

        public ContextController(IContextViewModelHelper contextViewModelHelper)
        {
            _contextViewModelHelper = contextViewModelHelper;
        }

        [HttpGet("select")]
        public IActionResult Select(string key, ContextViewModel.EnumAspect aspect)
        {
            if (aspect == ContextViewModel.EnumAspect.Management)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return RedirectToAction("Index", "Counties");
                }
                else if (_contextViewModelHelper.IsCounty(key))
                {
                    return RedirectToAction("Index", "Municipalities", new { id = _contextViewModelHelper.Key2Id(key) });
                }
                throw new NotImplementedException("Page for municaplity details not implemented yet");
            }
            else if (aspect == ContextViewModel.EnumAspect.ActivityOverview)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return RedirectToAction("Index", "Activities");
                }
                return RedirectToAction("Index", "Activities", new { id = _contextViewModelHelper.Key2Id(key), isCounty = _contextViewModelHelper.IsCounty(key)});
            }
            throw new Exception();
        }
    }
}