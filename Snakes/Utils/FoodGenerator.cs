using Snakes.models;
using Snakes.Services;
using System;
using System.Collections.Generic;

namespace Snakes.Utils
{
    public class FoodGenerator : IFoodGenerator
    {

        public Cell GenerateFood(List<Food> foods, List<Snake> snakes)
        {
            bool isOccupied = true;

        outer:
            while (isOccupied)
            {
                int x = RandomGenerator.NextNormal(new Random());
                int y = RandomGenerator.NextNormal(new Random());

                foreach (Snake snake in snakes)
                {

                }

                foreach (Food food in foods)
                {
                    if (food.Cell.X == x && food.Cell.Y == y)
                    {
                        goto outer;
                    }
                }

                return new Cell(x, y);
            }

            return new Cell(500, 500);
        }
    }
}
