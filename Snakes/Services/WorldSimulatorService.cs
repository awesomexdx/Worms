using Microsoft.Extensions.Hosting;
using Snakes.behaviours;
using Snakes.models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Snakes.DataBase.Repositories;

namespace Snakes.Services
{
    public class WorldSimulatorService : IHostedService
    {
        private readonly IFoodGenerator foodGenerator;
        private readonly INameGenerator nameGenerator;
        private readonly ISnakeActionsService snakeActionsService;
        private readonly IFileHandler fileHandlerService;
        private readonly IWorldBehaviourRepository worldBehaviourRepository;
        private readonly string behaviourName;

        public WorldSimulatorService(IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            ISnakeActionsService snakeActionsService,
            IFileHandler fileHandlerService,
            IWorldBehaviourRepository worldBehaviourRepository,
            string behaviourName
            )
        {
            this.foodGenerator = foodGenerator;
            this.nameGenerator = nameGenerator;
            this.snakeActionsService = snakeActionsService;
            this.fileHandlerService = fileHandlerService;
            this.worldBehaviourRepository = worldBehaviourRepository;
            this.behaviourName = behaviourName;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting life...");

            World world = new World(nameGenerator, foodGenerator, snakeActionsService, fileHandlerService);

            world.AddSnake(new Snake("John", new Cell(0, 0),
                new GoToFoodBehaviour()));
            world.Start();

            World worldDb = new World(foodGenerator, worldBehaviourRepository, behaviourName);
            worldDb.StartForDb();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
