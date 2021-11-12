using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snakes.DataBase;
using Snakes.DataBase.Base;
using Snakes.DataBase.Repositories;
using Snakes.models;
using Snakes.Services;
using Snakes.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace Snakes
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread serverThread = new Thread(new ThreadStart(() =>
            {
                CreateWebHostBuilder(args).Build().Run();
            }));

            Thread clientThread = new Thread(new ThreadStart(() =>
            {
                CreateHostBuilder(args).Build().Run();
            }));
            clientThread.Start();
            //serverThread.Start();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<StartUp>();
               });
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<Settings>(options =>
                    {
                        options.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";
                    });


                    services.AddDbContext<MainDbContext>(options =>
                        options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;"));

                    services.AddTransient<IWorldBehaviourRepository, WorldBehaviourRepository>();
                    services.AddTransient<INameGenerator, NameGenerator>();
                    services.AddTransient<IFoodGenerator, FoodGenerator>();
                    services.AddTransient<ISnakeActionsService, SnakeActionsWebService>();
                    services.AddTransient<IFileHandler, FileHandler>();

                    services.AddHostedService<WorldSimulatorService>(s => new WorldSimulatorService(
                        new FoodGenerator(),
                        new NameGenerator(),
                        new SnakeActionsWebService(),
                        new FileHandler(),
                        new WorldBehaviourRepository(s.GetService<MainDbContext>()),
                        args[0]
                    ));

                });
        }

    }
}