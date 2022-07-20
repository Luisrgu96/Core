using Core.Interfaces;

namespace DemoPlugin
{
    
    public class PluginService : IPluginService
    {
        public string Test()
        {
            return "Tested!";
        }
    }
    
}