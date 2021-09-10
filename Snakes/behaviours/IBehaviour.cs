using Snakes.models;

namespace Snakes.behaviours
{
    public interface IBehaviour
    {
        SnakeAction NextStep();
    }
}
