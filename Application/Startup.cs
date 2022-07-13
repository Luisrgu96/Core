using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

using Interfaces;

using Demo.Classes;
using Demo.Models;
using Demo.Services;

//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ApplicationParts;
//using Microsoft.AspNetCore.Mvc.Controllers;

namespace Demo
{
    // With:
    // services.AddControllers().PartManager.FeatureProviders.Add(new GenericControllerFeatureProvider());
    /*
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            AppSettings.Settings.Plugins.ForEach(p =>
            {
                Assembly assembly = Assembly.LoadFrom(p.Path);
                var atypes = assembly.GetTypes();
                var types = atypes.Where(t => t.BaseType == typeof(ControllerBase)).ToList();
                feature.Controllers.Add(types[0].GetTypeInfo());
            });
        }
    }
    */

    public class Startup
    {
        public AppSettings AppSettings { get; } = new AppSettings();
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.Bind(AppSettings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                // must be version 3.1.13 -- version 5's support .NET 5 only.
                // https://anthonygiretti.com/2020/05/10/why-model-binding-to-jobject-from-a-request-doesnt-work-anymore-in-asp-net-core-3-1-and-whats-the-alternative/
                .AddNewtonsoftJson(options => options.SerializerSettings.Formatting = Formatting.Indented);

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services
                .AddDbContext<MyDbContext>(options => options.UseSqlServer("myConnectionString"))
                .AddAuthentication("tokenAuth")
                .AddScheme<TokenAuthenticationSchemeOptions, AuthenticationService>("tokenAuth", ops => { });

            services.AddSingleton<IApplicationService, ApplicationService>();

            services.LoadPlugins();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Do not halt execution.  I don't fully understand this.
                // See http://netitude.bc3tech.net/2017/07/31/using-middleware-to-trap-exceptions-in-asp-net-core/
                // "Notice the difference in order when in development mode vs not. This is important as the Developer Exception page
                // passes through the exception to our handler so in order to get the best of both worlds, you want the Developer Page handler first.
                // In production, however, since the default Exception Page halts execution, we definitely to not want that one first."
                app.UseDeveloperExceptionPage();
                app.UseHttpStatusCodeExceptionMiddleware();
            }
            else
            {
                app.UseHttpStatusCodeExceptionMiddleware();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/demo/swagger/v1/swagger.json", "Demo API V1");
                });

            app
                .UseAuthentication()
                .UseRouting()
                .UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}