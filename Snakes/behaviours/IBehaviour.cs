using Snakes.models;
using System.Collections.Generic;

namespace Snakes.behaviours
{
    public interface IBehaviour
    {
        SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes);
    }
}
