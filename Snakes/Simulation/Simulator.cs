using Snakes.models;
using Snakes.Simulation;
using System.Collections.Generic;
using System.IO;
using Snakes.DataBase.Models;

namespace Snakes
{
    public class Simulator
    {
        private const int GAME_DURATION = 100;
        private const int SNAKE_REWARD = 10;
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
            GameSession gameSession = new GameSession();
            world.FileHandlerService.TextWriter = File.CreateText("gameSession.txt");

            for (; currentStep < GAME_DURATION; currentStep++)
            {
                gameSession.SetSnakeList(world.Snakes, currentStep);
                gameSession.SetFoodList(world.Foods, currentStep);

                world.FileHandlerService.WriteToTextWriter(world.GetCurrentState());

                Cell generaredCell = world.FoodGenerator.GenerateFood(new List<Food>(world.Foods), new List<Snake>(world.Snakes));

                Food newFood = new Food(generaredCell);
                world.Foods.Add(newFood);

                foreach (Snake snake in world.Snakes)
                {
                    if (snake.Cell.Equals(generaredCell))
                    {
                        snake.HitPoints += SNAKE_REWARD;
                        world.Foods.Remove(newFood);
                    }

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
                        gameSession.DeadFoodCount++;
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

            world.FileHandlerService.TextWriter.Close();
            return gameSession;
        }

        public void startForDb()
        {
            int curStep;
            for (curStep = 0; curStep < GAME_DURATION; curStep++)
            {
                Cell generaredCell = world.FoodGenerator.GenerateFood(new List<Food>(world.Foods), new List<Snake>());
                WorldBehaviourModel worldBehaviour = new WorldBehaviourModel();
                worldBehaviour.Name = world.WorldBehaviourName;
                worldBehaviour.Step = curStep;
                worldBehaviour.FoodX = generaredCell.X;
                worldBehaviour.FoodY = generaredCell.Y;
                world.WorldBehaviour.Create(worldBehaviour);
            }
        }
    }
}
