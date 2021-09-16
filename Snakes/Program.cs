using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snakes.Services;
using Snakes.Utils;

namespace Snakes
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddHostedService<WorldSimulatorService>();

                    services.AddScoped<INameGenerator, NameGenerator>();
                    services.AddScoped<IFoodGenerator, FoodGenerator>();
                    services.AddScoped<ISnakeActionsService, SnakeActionsService>();
                    services.AddScoped<IFileHandler, FileHandler>();

                });

        }

    }
}
