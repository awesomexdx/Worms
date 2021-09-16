using Snakes.behaviours;
using Snakes.models;
using Snakes.Utils;
using System;
using System.Collections.Generic;
using Snakes.Services;

namespace Snakes
{
    public class Simulator
    {
        private const int GAME_DURATION = 100;
        private const int SNAKE_REWARD = 10;
        private const int REPRODUCTION_PRICE = 10;
        public int currentStep;

        private readonly List<Snake> newSnakes = new List<Snake>();

        private readonly List<Snake> deadSnakes = new List<Snake>();
        private readonly List<Food> deadFoods = new List<Food>();
        private int ateFood;

        private World world;

        public Simulator(World world)
        {
            this.world = world;
            currentStep = 0;
        }
        
        private CellContent isCellOccupied(Cell cell)
        {
            foreach (Snake snake in world.Snakes)
            {
                if (snake.Cell.X == cell.X && snake.Cell.Y == cell.Y)
                {
                    return CellContent.Snake;
                }
            }

            foreach (Snake snake in newSnakes)
            {
                if (snake.Cell.X == cell.X && snake.Cell.Y == cell.Y)
                {
                    return CellContent.Snake;
                }
            }

            foreach (Food food in world.Foods)
            {
                if (food.Cell.X == cell.X && food.Cell.Y == cell.Y)
                {
                    ateFood = world.Foods.IndexOf(food);
                    return CellContent.Food;
                }
            }

            return CellContent.Void;
        }

        private void resolseMove(Cell newCell, Snake snake)
        {
            CellContent cellContent = isCellOccupied(newCell);
            if (cellContent == CellContent.Food)
            {
                snake.HitPoints += SNAKE_REWARD;
                world.Foods.RemoveAt(ateFood);
                snake.Cell = newCell;
            }
            else if (cellContent == CellContent.Void)
            {
                snake.Cell = newCell;
            }
        }

        private void resolveReproduce(Cell newCell, Snake snake)
        {
            CellContent cellContent = isCellOccupied(newCell);
            if (cellContent == CellContent.Void && snake.HitPoints >= 11)
            {
                snake.HitPoints -= REPRODUCTION_PRICE;
                newSnakes.Add(new Snake(world.NameGenerator.GenerateNext(), newCell.X, newCell.Y, new GoToFoodBehaviour(newCell, world)));
            }
        }
        private void resolveAction(SnakeAction action, Snake snake)
        {
            Cell newCell = action.Move.Move(new Cell(snake.Cell.X, snake.Cell.Y, CellContent.Snake));

            switch (action.ActionType)
            {
                case ActionType.MOVE:
                    resolseMove(newCell, snake);
                    break;
                case ActionType.NOTHING:
                    break;
                case ActionType.REPRODUCE:
                    resolveReproduce(newCell, snake);
                    break;
                default:
                    break;
            }
        }

        public GameSession start()
        {
            world.FileHandlerService.CreateNewGameSessionFile();
            GameSession gameSession = new GameSession();

            for (; currentStep < GAME_DURATION; currentStep++)
            {
                gameSession.SetSnakeList(world.Snakes, currentStep);
                gameSession.SetFoodList(world.Foods, currentStep);

                Console.WriteLine(world.GetCurrentState());
                world.FileHandlerService.WriteToFile(world.GetCurrentState() + "\r\n");

                world.FoodGenerator.GenerateFood(world);

                foreach (Snake snake in world.Snakes)
                {
                    SnakeAction action = world.SnakeActionsService.Answer(snake,world);
                    resolveAction(action, snake);
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

                foreach (Snake newSnake in newSnakes)
                {
                    world.Snakes.AddRange(newSnakes);
                }

                newSnakes.Clear();
                deadSnakes.Clear();

            }

            return gameSession;
        }
    }
}
