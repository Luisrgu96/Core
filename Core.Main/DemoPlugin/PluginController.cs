using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoPlugin
{
    
    [ApiController]    
    [Route("[controller]")]

    public class PluginController
    {
        private IPluginService _pluginService;

        public PluginController(IPluginService pluginService)
        {
            _pluginService = pluginService;
        }
        

        [HttpGet("Version")]
        public object Version()
        {
            return _pluginService.Test();
        }





    }
}