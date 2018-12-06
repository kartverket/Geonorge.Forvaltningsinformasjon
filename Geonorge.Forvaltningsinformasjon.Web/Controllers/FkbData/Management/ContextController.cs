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
            string action;
            string controller;
            dynamic parameters = null;

            switch (aspect)
            {
                case ContextViewModel.EnumAspect.Management:
                    {
                        if (string.IsNullOrWhiteSpace(key))
                        {
                            action = "Index";
                            controller = "Counties";
                        }
                        else if (_contextViewModelHelper.IsCounty(key))
                        {
                            action = "Index";
                            controller = "Municipalities";
                            parameters = new { id = _contextViewModelHelper.Key2Id(key) };
                        }
                        else
                        {
                            action = "Get";
                            controller = "Municipalities";
                            parameters = new { id = _contextViewModelHelper.Key2Id(key) };
                        }
                    }
                    break;
                case ContextViewModel.EnumAspect.ActivityOverview:
                    {
                        action = "Index";
                        controller = "Activities";

                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            parameters = new { id = _contextViewModelHelper.Key2Id(key), isCounty = _contextViewModelHelper.IsCounty(key) };
                        }
                    }
                    break;
                case ContextViewModel.EnumAspect.OperationalStatus:
                    {
                        action = "Index";
                        controller = "OperationalStatus";

                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            parameters = new { id = _contextViewModelHelper.Key2Id(key), isCounty = _contextViewModelHelper.IsCounty(key) };
                        }
                    }
                    break;
                default:
                    throw new Exception("Invalid aspect");
            }
            return RedirectToAction(action, controller, parameters);
        }
    }
}