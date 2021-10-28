using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snakes.DataBase.Repositories;
using Snakes.Services;
using Snakes.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes
{
    class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<INameGenerator, NameGenerator>();
            services.AddTransient<IFoodGenerator, FoodGenerator>();
            services.AddTransient<ISnakeActionsService, SnakeActionsService>();
            services.AddTransient<IFileHandler, FileHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{name}/getAction");
                /*endpoints.MapGet("/{name}/getAction", async context =>
                {
                    await context.Response.WriteAsync("Hello Worlddd!");
                });*/
            });
        }
    }
}
