using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.behaviours;
using Snakes.models;
using Snakes.Utils;

namespace Snakes
{
    class Simulator
    {
        private const int GAME_DURATION = 100;
        private const int SNAKE_REWARD = 10;
        private const int REPRODUCTION_PRICE = 10;
        private List<Snake> snakes;
        private List<Food> foods;
        private int currentStep;

        private List<Snake> newSnakes = new List<Snake>();

        private List<Snake> deadSnakes = new List<Snake>();
        private List<Food> deadFoods = new List<Food>();
        private int ateFood;
        public Simulator(List<Snake> snakes, List<Food> foods)
        {
            currentStep = 0;
            this.snakes = snakes;
            this.foods = foods;
        }
        public void addSnake(Snake snake)
        {
            this.snakes.Add(snake);
        }
        public void generateFood()
        {
            bool isOccupied = true;

            outer:
            while (isOccupied)
            {
                int x = RandomGenerator.NextNormal(new Random());
                int y = RandomGenerator.NextNormal(new Random());

                foreach (var snake in snakes)
                {
                    if (snake.Cell.X == x && snake.Cell.Y == y)
                    {
                        snake.HitPoints += SNAKE_REWARD;
                        break;
                    }
                }

                foreach (var food in foods)
                {
                    if (food.Cell.X == x && food.Cell.Y == y)
                    {
                        goto outer;
                    }
                }

                foods.Add(new Food(new Cell(x,y,CellContent.Food)));
                break;
            }
        }
        private CellContent isCellOccupied(Cell cell)
        {
            foreach (var snake in snakes)
            {
                if (snake.Cell.X == cell.X && snake.Cell.Y == cell.Y)
                {
                    return CellContent.Snake;
                } 
            }

            foreach (var food in foods)
            {
                if (food.Cell.X == cell.X && food.Cell.Y == cell.Y)
                {
                    ateFood = foods.IndexOf(food);
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
                foods.RemoveAt(ateFood);
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
                newSnakes.Add(new Snake(snake.Name + snakes.Count, newCell.X, newCell.Y, new RandomBehaviour()));
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

        public void start()
        {
            for (; currentStep < GAME_DURATION; currentStep++)
            {
                Console.WriteLine(World.GetCurrentState());
                
                foreach (var snake in snakes)
                {
                    SnakeAction action = snake.Answer();
                    resolveAction(action,snake);
                    snake.HitPoints--;
                    if (snake.HitPoints <= 0) { deadSnakes.Add(snake); }
                }

                generateFood();

                foreach (var food in foods)
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
                    snakes.Remove(deadSnake);
                }

                foreach (var deadFood in deadFoods)
                {
                    foods.Remove(deadFood);
                }

                foreach (var newSnake in newSnakes)
                {
                    snakes.AddRange(newSnakes);
                }
                
                newSnakes.Clear();
                deadSnakes.Clear();

            }
        }
    }
}
