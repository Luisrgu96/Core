using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Interfaces;

using Demo.Classes;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Public : ControllerBase
    {
        private IPluginService ps;

        public Public(IPluginService ps)
        {
            this.ps = ps;
        }

        [AllowAnonymous]
        [HttpGet("Version")]
        public object Version()
        {
            return new { Version = "1.00", PluginSays = ps.Test() };
        }

        [AllowAnonymous]
        [HttpGet("AppSettings")]
        public object GetAppSettings()
        {
            return AppSettings.Settings;
        }

        [AllowAnonymous]
        [HttpGet("TestException")]
        public void TestException()
        {
            throw new Exception("Exception occurred!");
        }

        [Authorize]
        [HttpGet("TestAuthentication")]
        public ActionResult TestAuthentication()
        {
            return Ok();
        }
    }
}
