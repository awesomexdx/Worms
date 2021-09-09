using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Snakes.behaviours;

namespace Snakes.models
{
    public class World
    {
        List<Snake> snakesList = new List<Snake>();
        List<Food> foodList = new List<Food>();

        public List<Snake> Snakes
        {
            get { return snakesList; }
        }

        public List<Food> Foods
        {
            get { return foodList; }
        }

        public Simulator simulator;

        private static World instance;
        private World()
        {
            simulator = new Simulator();
        }

        public void AddSnake(Snake snake)
        {
            snakesList.Add(snake);
        }

        public static GameSession Start()
        {
           return Instance().simulator.start();
        }

        public static void Reset()
        {
            instance = null;
        }

        public static World Instance()
        {
            return instance ??= new World();
        }

        public static string GetCurrentState()
        {
            string state = "Worms:[";
            
            foreach (var snake in Instance().snakesList)
            {
                state += snake.Name + "-" + snake.HitPoints + "(" + snake.Cell.X + "," + snake.Cell.Y + ")";
            }

            state += "], Food:[";

            foreach (var food in Instance().foodList)
            {
                state += "(" + food.Cell.X + "," + food.Cell.Y + ")";
            }

            state += "]";
            
            return state;
        }
    }
}
