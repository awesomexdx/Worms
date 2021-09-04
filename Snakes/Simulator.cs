using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.models;

namespace Snakes
{
    class Simulator
    {
        private const int GAME_DURATION = 100;
        private List<Snake> snakes;
        private int currentStep;
        public Simulator(List<Snake> snakes)
        {
            currentStep = 0;
            this.snakes = snakes;
        }
        public void addSnake(Snake snake)
        {
            this.snakes.Add(snake);
        }
        public void start()
        {
            for (; currentStep < GAME_DURATION; currentStep++)
            {
                foreach (var snake in snakes)
                {
                    snake.Answer();
                }

                string state = World.GetCurrentState();
                Console.WriteLine(World.GetCurrentState());
            }
        }
    }
}
