using System.Diagnostics;
using Geonorge.Forvaltningsinformasjon.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;

namespace Geonorge.Forvaltningsinformasjon.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("~/login")]
        public ActionResult LogIn()
        {
            // Instruct the OIDC client middleware to redirect the user agent to the identity provider.
            // Note: the authenticationType parameter must match the value configured in Startup.cs
            var redirectUrl = Request.Headers["Referer"];
            if(Request.Query.ContainsKey("redirectUrl"))
            {
                redirectUrl = Request.Query["redirectUrl"];
            }

            if(string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = "/";
            }

            Log.Debug("Login redirectUrl: {redirectUrl}", redirectUrl);

            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet("~/logout"), HttpPost("~/logout")]
        public ActionResult LogOut()
        {
            CookieOptions options = new CookieOptions
            {
                Domain =  "geonorge.no", // Set the domain for the cookie
                HttpOnly = false,
            };
            Response.Cookies.Delete("_loggedIn");
            Response.Cookies.Append("_loggedIn", "false", options);

            // Instruct the cookies middleware to delete the local cookie created when the user agent
            // is redirected from the identity provider after a successful authorization flow and
            // to redirect the user agent to the identity provider to sign out.
            return SignOut(new AuthenticationProperties { RedirectUri = "/signout" }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// This is the action responding to signout callback route after logout at the identity provider
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/signout")]
        public ActionResult SignOutCallback()
        {
            return Redirect("/");
        }
    }
}
