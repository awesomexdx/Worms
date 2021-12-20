using Snakes.models;
using Snakes.Simulation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

                    SnakeAction action = world.SnakeActionsService.Answer(snake, world, currentStep);
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

        private WorldBehaviourModel GenerateWorldBehaviourModel(int step)
        {
            Cell generaredCell = world.FoodGenerator.GenerateFood(new List<Food>(world.Foods), new List<Snake>());
            WorldBehaviourModel worldBehaviour = new WorldBehaviourModel();
            world.Foods.Add(new Food(generaredCell));
            worldBehaviour.Name = world.WorldBehaviourName;
            worldBehaviour.Step = step;
            worldBehaviour.FoodX = generaredCell.X;
            worldBehaviour.FoodY = generaredCell.Y;
            return worldBehaviour;
        }
        public List<WorldBehaviourModel> StartForDb()
        {
            int curStep;
            List<WorldBehaviourModel> models = new(); 
            for (curStep = 0; curStep < GAME_DURATION; curStep++)
            {
                var worldBehaviour = GenerateWorldBehaviourModel(curStep);
                world.WorldBehaviour.Create(worldBehaviour);
                models.Add(worldBehaviour);
            }

            return models;
        }

        public GameSession SimulateBehaviourByName(string worldBehaviourName)
        {
            GameSession gameSession = new GameSession();
            world.FileHandlerService.TextWriter = File.CreateText("bdSession.txt");
            var behaviour = world.WorldBehaviour.Get().Where(x => x.Name == worldBehaviourName);

            world.FileHandlerService.WriteToTextWriter($"Simulation for world behaviour from db: {worldBehaviourName}"); 
            foreach (var step in behaviour)
            {
                gameSession.SetSnakeList(world.Snakes, step.Step);
                gameSession.SetFoodList(world.Foods, step.Step);
                world.FileHandlerService.WriteToTextWriter(world.GetCurrentState());
                Cell generaredCell = new Cell(step.FoodX,step.FoodY);

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
    }
}
