using Geonorge.Forvaltningsinformasjon.Web.Abstractions.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.Forvaltningsinformasjon.Web.Controllers.Common
{
    [Route("common/context")]
    public class ContextController : Controller
    {
        private IContextViewModelHelper _contextViewModelHelper;

        public ContextController(IContextViewModelHelper contextViewModelHelper)
        {
            _contextViewModelHelper = contextViewModelHelper;
        }

        [HttpGet("select")]
        public IActionResult Select(string key, string aspect)
        {
            string action = "";
            dynamic parameters = null;

            if (string.IsNullOrWhiteSpace(key))
            {
                action = "Country";
            }
            else
            {
                if (_contextViewModelHelper.IsCounty(key))
                {
                    action = "County";
                }
                else
                {
                    action = "Municipality";
                }
                parameters = new { id = _contextViewModelHelper.Key2Id(key)};
            }
            return RedirectToAction(action, aspect, parameters);
        }
    }
}