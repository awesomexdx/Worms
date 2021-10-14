using Snakes.Services;
using System.Collections.Generic;
using System.Text;
using Snakes.DataBase.Repositories;

namespace Snakes.models
{
    public class World
    {
        private List<Snake> snakesList = new List<Snake>();
        private List<Food> foodList = new List<Food>();
        public List<Snake> Snakes
        {
            get => snakesList;
            set => snakesList = value;
        }

        public List<Food> Foods
        {
            get => foodList;
            set => foodList = value;
        }

        public Simulator simulator;

        public INameGenerator NameGenerator;
        public IFoodGenerator FoodGenerator;
        public ISnakeActionsService SnakeActionsService;
        public IFileHandler FileHandlerService;
        public IWorldBehaviourRepository WorldBehaviour;
        public string WorldBehaviourName;
        public World(INameGenerator nameGenerator,
            IFoodGenerator foodGenerator,
            ISnakeActionsService snakeActionsService,
            IFileHandler fileHandlerService)
        {
            NameGenerator = nameGenerator;
            FoodGenerator = foodGenerator;
            SnakeActionsService = snakeActionsService;
            FileHandlerService = fileHandlerService;
            simulator = new Simulator(this);
        }

        public World(IFoodGenerator foodGenerator, IWorldBehaviourRepository worldBehaviour, string Name)
        {
            FoodGenerator = foodGenerator;
            WorldBehaviour = worldBehaviour;
            WorldBehaviourName = Name;
            simulator = new Simulator(this);
        }

        public void AddSnake(Snake snake)
        {
            snakesList.Add(snake);
        }

        public GameSession Start()
        {
            return simulator.start();
        }

        public void StartForDb()
        {
            simulator.startForDb();
        }

        public string GetCurrentState()
        {
            StringBuilder state = new StringBuilder("Worms:[");

            foreach (Snake snake in snakesList)
            {
                state.Append($"{snake.Name}-{snake.HitPoints}({snake.Cell.X},{snake.Cell.Y}),");
            }

            if (snakesList.Count != 0)
            {
                state.Remove(state.Length - 1, 1);
            }

            state.Append("],Food:[");

            foreach (Food food in foodList)
            {
                state.Append($"({food.Cell.X},{food.Cell.Y}),");
            }

            state.Remove(state.Length - 1, 1);
            state.Append("]");

            return state.ToString();
        }
    }
}
