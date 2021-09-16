using Snakes.models;
using Snakes.Services;
using System;

namespace Snakes.Utils
{
    internal class FoodGenerator : IFoodGenerator
    {
        private const int SNAKE_REWARD = 10;
        public void GenerateFood(World world)
        {
            bool isOccupied = true;

        outer:
            while (isOccupied)
            {
                int x = RandomGenerator.NextNormal(new Random());
                int y = RandomGenerator.NextNormal(new Random());

                foreach (Snake snake in world.Snakes)
                {
                    if (snake.Cell.X == x && snake.Cell.Y == y)
                    {
                        snake.HitPoints += SNAKE_REWARD;
                        return;
                    }
                }

                foreach (Food food in world.Foods)
                {
                    if (food.Cell.X == x && food.Cell.Y == y)
                    {
                        goto outer;
                    }
                }

                world.Foods.Add(new Food(new Cell(x, y, CellContent.Food)));
                break;
            }
        }
    }
}
