using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Snakes.behaviours;
using Snakes.models;
using Snakes.Utils;

namespace Snakes.Services
{
    class WorldSimulatorService : IHostedService
    {
        private IFoodGenerator foodGenerator;
        private INameGenerator nameGenerator;
        private ISnakeActionsService snakeActionsService;
        private IFileHandler fileHandlerService;

        public WorldSimulatorService(IFoodGenerator foodGenerator, 
            INameGenerator nameGenerator,
            ISnakeActionsService snakeActionsService,
            IFileHandler fileHandlerService)
        {
            this.foodGenerator = foodGenerator;
            this.nameGenerator = nameGenerator;
            this.snakeActionsService = snakeActionsService;
            this.fileHandlerService = fileHandlerService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting life...");

            World world = new World(nameGenerator, foodGenerator, snakeActionsService, fileHandlerService);
            //new GoToFoodBehaviour(new Cell(0, 0, CellContent.Snake))

            world.AddSnake(new Snake("John", new Cell(0, 0, CellContent.Snake), new GoToFoodBehaviour(new Cell(0, 0, CellContent.Snake), world)));
            world.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
