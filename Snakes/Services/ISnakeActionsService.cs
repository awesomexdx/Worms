using Snakes.models;

namespace Snakes.Services
{
    public interface ISnakeActionsService
    {
        public SnakeAction Answer(Snake snake, World world);
    }
}
