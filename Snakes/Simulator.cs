﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Snakes.behaviours;
using Snakes.models;
using Snakes.Utils;

namespace Snakes
{
    public class Simulator
    {
        private const int GAME_DURATION = 100;
        private const int SNAKE_REWARD = 10;
        private const int REPRODUCTION_PRICE = 10;
        public int currentStep;

        private List<Snake> newSnakes = new List<Snake>();

        private List<Snake> deadSnakes = new List<Snake>();
        private List<Food> deadFoods = new List<Food>();
        private int ateFood;
        public Simulator()
        {
            currentStep = 0;
        }
        public void generateFood()
        {
            bool isOccupied = true;

            outer:
            while (isOccupied)
            {
                int x = RandomGenerator.NextNormal(new Random());
                int y = RandomGenerator.NextNormal(new Random());

                foreach (var snake in World.Instance().Snakes)
                {
                    if (snake.Cell.X == x && snake.Cell.Y == y)
                    {
                        snake.HitPoints += SNAKE_REWARD;
                        return;
                    }
                }

                foreach (var food in World.Instance().Foods)
                {
                    if (food.Cell.X == x && food.Cell.Y == y)
                    {
                        goto outer;
                    }
                }

                World.Instance().Foods.Add(new Food(new Cell(x,y,CellContent.Food)));
                break;
            }
        }
        private CellContent isCellOccupied(Cell cell)
        {
            foreach (var snake in World.Instance().Snakes)
            {
                if (snake.Cell.X == cell.X && snake.Cell.Y == cell.Y)
                {
                    return CellContent.Snake;
                } 
            }

            foreach (var snake in newSnakes)
            {
                if (snake.Cell.X == cell.X && snake.Cell.Y == cell.Y)
                {
                    return CellContent.Snake;
                }
            }

            foreach (var food in World.Instance().Foods)
            {
                if (food.Cell.X == cell.X && food.Cell.Y == cell.Y)
                {
                    ateFood = World.Instance().Foods.IndexOf(food);
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
                World.Instance().Foods.RemoveAt(ateFood);
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
                newSnakes.Add(new Snake(NameGenerator.GenerateNext(), newCell.X, newCell.Y, new GoToFoodBehaviour(newCell)));
            }
        }
        private void resolveAction(SnakeAction action, Snake snake)
        {
            Cell newCell = action.Move.Move(new Cell(snake.Cell.X,snake.Cell.Y, CellContent.Snake));

            switch (action.ActionType)
            {
                case ActionType.MOVE:
                    resolseMove(newCell,snake);
                    break;
                case ActionType.NOTHING:
                    break;
                case ActionType.REPRODUCE:
                    resolveReproduce(newCell,snake);
                    break;
                default:
                    break;
            }
        }

        public GameSession start()
        {
            FileHandler.CreateNewGameSessionFile();
            GameSession gameSession = new GameSession();

            for (; currentStep < GAME_DURATION; currentStep++)
            {
                gameSession.SetSnakeList(World.Instance().Snakes, currentStep);
                gameSession.SetFoodList(World.Instance().Foods, currentStep);

                Console.WriteLine(World.GetCurrentState());
                FileHandler.WriteToFile(World.GetCurrentState() + "\r\n");
                
                generateFood();

                foreach (var snake in World.Instance().Snakes)
                {
                    SnakeAction action = snake.Answer();
                    resolveAction(action,snake);
                    snake.HitPoints--;
                    if (snake.HitPoints <= 0) { deadSnakes.Add(snake); }
                }


                foreach (var food in World.Instance().Foods)
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

                foreach (var deadSnake in deadSnakes)
                {
                    World.Instance().Snakes.Remove(deadSnake);
                }

                foreach (var deadFood in deadFoods)
                {
                    World.Instance().Foods.Remove(deadFood);
                }

                foreach (var newSnake in newSnakes)
                {
                    World.Instance().Snakes.AddRange(newSnakes);
                }
                
                newSnakes.Clear();
                deadSnakes.Clear();

            }

            return gameSession;
        }
    }
}
