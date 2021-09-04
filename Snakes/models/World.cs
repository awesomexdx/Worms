using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Snakes.behaviours;

namespace Snakes.models
{
    class World
    {
        List<Snake> snakesList = new List<Snake>();
        List<Cell> foodList = new List<Cell>();

        private Simulator simulator;

        private static World instance;
        private World()
        {
            simulator = new Simulator(snakesList);
        }

        public void AddSnake(Snake snake)
        {
            simulator.addSnake(snake);
        }

        public static void Start()
        {
            Instance().simulator.start();
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
                state += snake.Name + "(" + snake.Cell.X + "," + snake.Cell.Y + ")";
            }

            state += "]";
            
            return state;
        }
    }
}
