using Snakes.models;
using Snakes.Simulation;
using System.Collections.Generic;

namespace Snakes
{
    public class Simulator
    {
        private const int GAME_DURATION = 100;
        public int currentStep;

        private readonly List<Snake> newSnakes = new List<Snake>();

        private readonly List<Snake> deadSnakes = new List<Snake>();
        private readonly List<Food> deadFoods = new List<Food>();

        private readonly World world;

        public Simulator(World world)
        {
            this.world = world;
            currentStep = 0;
        }

        public GameSession start()
        {
            world.FileHandlerService.CreateNewGameSessionFile();
            GameSession gameSession = new GameSession();

            for (; currentStep < GAME_DURATION; currentStep++)
            {
                gameSession.SetSnakeList(world.Snakes, currentStep);
                gameSession.SetFoodList(world.Foods, currentStep);


                world.FileHandlerService.WriteToFile(world.GetCurrentState() + "\r\n");

                world.FoodGenerator.GenerateFood(world);

                foreach (Snake snake in world.Snakes)
                {
                    SnakeAction action = world.SnakeActionsService.Answer(snake, world);
                    SimulationHelper.ResolveAction(world, action, snake, newSnakes);
                    snake.HitPoints--;
                    if (snake.HitPoints <= 0) { deadSnakes.Add(snake); }
                }


                foreach (Food food in world.Foods)
                {
                    if (food.TimeToLive <= 0)
                    {
                        deadFoods.Add(food);
                    }
                    else
                    {
                        food.TimeToLive--;
                    }
                }

                foreach (Snake deadSnake in deadSnakes)
                {
                    world.Snakes.Remove(deadSnake);
                }

                foreach (Food deadFood in deadFoods)
                {
                    world.Foods.Remove(deadFood);
                }

                world.Snakes.AddRange(newSnakes);

                newSnakes.Clear();
                deadSnakes.Clear();

            }

            return gameSession;
        }
    }
}
