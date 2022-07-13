using Microsoft.Extensions.DependencyInjection;

using Interfaces;

namespace Plugin2
{
    public class Plugin2 : IPlugin
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddSingleton<IPlugin2Service, Plugin2Service>();
        }
    }
}
