using Microsoft.Extensions.Hosting;
using Snakes.behaviours;
using Snakes.models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snakes.Services
{
    public class WorldSimulatorService : IHostedService
    {
        private readonly IFoodGenerator foodGenerator;
        private readonly INameGenerator nameGenerator;
        private readonly ISnakeActionsService snakeActionsService;
        private readonly IFileHandler fileHandlerService;

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

            world.AddSnake(new Snake("John", new Cell(0, 0, CellContent.Snake),
                new GoToFoodBehaviour(new Cell(0, 0, CellContent.Snake), world)));
            world.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
