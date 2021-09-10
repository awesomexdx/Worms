using Snakes.moves;

namespace Snakes.models
{
    public class SnakeAction
    {
        public IMove Move { get; set; }
        public ActionType ActionType { get; set; }

        public SnakeAction(IMove move, ActionType type)
        {
            Move = move;
            ActionType = type;
        }
    }
}
