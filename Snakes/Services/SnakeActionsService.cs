using Snakes.models;
using System.Collections.Generic;

namespace Snakes.Services
{
    public class SnakeActionsService : ISnakeActionsService
    {
        public SnakeAction Answer(Snake snake, World world)
        {
            snake.Behaviour.CurrentCell = snake.Cell;
            snake.Behaviour.SnakeHP = snake.HitPoints;
            snake.Behaviour.World = world;
            SnakeAction action = snake.Behaviour.NextStep(new Snake(snake), new List<Food>(world.Foods), new List<Snake>(world.Snakes));

            return action;
        }
    }
}
