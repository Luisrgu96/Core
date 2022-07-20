using System;
using System.Linq;
using System.Reflection;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;



namespace Core.Main.Classes
{
    
    public static class ServicePluginExtension
    {

        public static IServiceCollection LoadPlugins(this IServiceCollection services)
        {


            AppSettings.Settings.Plugins.ForEach(p =>
            {
                Assembly assembly = Assembly.LoadFrom(p.Path);
                var part = new AssemblyPart(assembly);
                services.AddControllers().PartManager.ApplicationParts.Add(part);

                var atypes = assembly.GetTypes();
                var pluginClass = atypes.SingleOrDefault(t => t.GetInterface(nameof(IPlugin)) != null);

                if (pluginClass != null)
                {
                    var initMethod = pluginClass.GetMethod(nameof(IPlugin.Initialize), BindingFlags.Public | BindingFlags.Instance);
                    var obj = Activator.CreateInstance(pluginClass);
                    initMethod.Invoke(obj, new object[] { services });
                }
            });

            return services;
        }
    }
}
