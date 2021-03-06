using System.Collections.Generic;

namespace Snakes.models
{
    public class GameSession
    {
        public Dictionary<int, List<Snake>> SnakeList { get; set; }

        public Dictionary<int, List<Food>> FoodList { get; set; }

        public int DeadFoodCount { get; set; }

        public GameSession()
        {
            SnakeList = new Dictionary<int, List<Snake>>();
            FoodList = new Dictionary<int, List<Food>>();
            DeadFoodCount = 0;
        }

        public void SetSnakeList(List<Snake> snakes, int step)
        {
            List<Snake> newSnakes = new List<Snake>();
            foreach (Snake snake in snakes)
            {
                Snake newSnake = new Snake(snake.Name, new Cell(snake.Cell.X, snake.Cell.Y),
                    snake.Behaviour);
                newSnake.HitPoints = snake.HitPoints;
                newSnakes.Add(newSnake);
            }
            SnakeList[step] = newSnakes;
        }

        public void SetFoodList(List<Food> foods, int step)
        {
            List<Food> newFoods = new List<Food>();
            foreach (Food food in foods)
            {
                Food newFood = new Food(new Cell(food.Cell.X, food.Cell.Y));
                newFood.TimeToLive = food.TimeToLive;
                newFoods.Add(newFood);
            }
            FoodList[step] = newFoods;
        }
    }
}
