using Microsoft.Extensions.DependencyInjection;

namespace Core.Interfaces
{
    public interface IPlugin
    {
        void Initialize(IServiceCollection services);
    }
}