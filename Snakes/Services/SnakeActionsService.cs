using Snakes.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.behaviours;

namespace Snakes.Services
{
    class SnakeActionsService : ISnakeActionsService
    {
        public SnakeAction Answer(Snake snake, World world)
        {
            snake.Behaviour.CurrentCell = snake.Cell;
            snake.Behaviour.SnakeHP = snake.HitPoints;
            snake.Behaviour.World = world;
            SnakeAction action = snake.Behaviour.NextStep();
            
            return action;
        }
    }
}
