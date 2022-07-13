using Microsoft.Extensions.DependencyInjection;

using Interfaces;

namespace Plugin
{
    public class Plugin : IPlugin
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddSingleton<IPluginService, PluginService>();
        }
    }
}
