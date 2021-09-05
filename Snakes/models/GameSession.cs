using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.models
{
    public class GameSession
    {
        public Dictionary<int, List<Snake>> SnakeList { get; set; }

        public Dictionary<int, List<Food>> FoodList { get; set; }

        public GameSession()
        {
            SnakeList = new Dictionary<int, List<Snake>>();
            FoodList = new Dictionary<int, List<Food>>();
        }

        public void SetSnakeList(List<Snake> snakes, int step)
        {
            List<Snake> newSnakes = new List<Snake>();
            foreach (var snake in snakes)
            {
                Snake newSnake = new Snake(snake.Name, new Cell(snake.Cell.X, snake.Cell.Y, CellContent.Snake),
                    snake.Behaviour);
                newSnake.HitPoints = snake.HitPoints;
                newSnakes.Add(newSnake);
            }
            SnakeList[step] = newSnakes;
        }

        public void SetFoodList(List<Food> foods, int step)
        {
            List<Food> newFoods = new List<Food>();
            foreach (var food in foods)
            {
                Food newFood = new Food(new Cell(food.Cell.X, food.Cell.Y, CellContent.Snake));
                newFood.TimeToLive = food.TimeToLive;
                newFoods.Add(newFood);
            }
            FoodList[step] = newFoods;
        }
    }
}
