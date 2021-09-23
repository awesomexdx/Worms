using Snakes.Services;
using System.Collections.Generic;

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

        public void AddSnake(Snake snake)
        {
            snakesList.Add(snake);
        }

        public GameSession Start()
        {
            return simulator.start();
        }

        public string GetCurrentState()
        {
            string state = "Worms:[";

            foreach (Snake snake in snakesList)
            {
                state += snake.Name + "-" + snake.HitPoints + "(" + snake.Cell.X + "," + snake.Cell.Y + ")";
            }

            state += "], Food:[";

            foreach (Food food in foodList)
            {
                state += "(" + food.Cell.X + "," + food.Cell.Y + ")";
            }

            state += "]";

            return state;
        }
    }
}
