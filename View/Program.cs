using Snakes.Services;
using Snakes.Utils;
using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snakes.DataBase;
using Snakes.DataBase.Base;
using Snakes.DataBase.Repositories;

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

            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();

            var options = optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;")
                .Options;

            Application.Run(new MainForm(new FoodGenerator(), 
                new NameGenerator(),
                new SnakeActionsService(),
                new FileHandler(), 
                new WorldBehaviourRepository(new MainDbContext(options))));
        }
    }
}
