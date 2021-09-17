using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snakes.Services;
using Snakes.Utils;

namespace View
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateHostBuilder(new string[] { "" }).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    //services.AddHostedService<WorldSimulatorService>();
                    services.AddHostedService<MainForm>();

                    services.AddScoped<INameGenerator, NameGenerator>();
                    services.AddScoped<IFoodGenerator, FoodGenerator>();
                    services.AddScoped<ISnakeActionsService, SnakeActionsService>();
                    services.AddScoped<IFileHandler, FileHandler>();

                });

        }
    }
}
