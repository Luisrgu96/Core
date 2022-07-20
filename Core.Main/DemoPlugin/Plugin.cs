using Microsoft.Extensions.DependencyInjection;

using Core.Interfaces;

namespace DemoPlugin
{
    public class Plugin : IPlugin
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddSingleton<IPluginService, PluginService>();
        }
    }
}