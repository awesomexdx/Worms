using Snakes.models;

namespace Snakes.Services
{
    internal class SnakeActionsService : ISnakeActionsService
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
