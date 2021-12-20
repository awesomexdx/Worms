using Snakes.models;
using Snakes.moves;
using System.Collections.Generic;

namespace Snakes.behaviours
{
    public abstract class Behaviour : IBehaviour
    {
        protected List<IMove> allMoves = new List<IMove>() { new MoveDown(), new MoveUp(), new MoveLeft(), new MoveRight() };
        public Cell CurrentCell { get; set; }
        public SnakeAction PrevAction { get; set; }
        public World World { get; set; }
        public int SnakeHP { get; set; }
        public virtual SnakeAction GetNextMove(IMove prevMove)
        {
            return new SnakeAction(prevMove, ActionType.MOVE);
        }
        public abstract SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes);
        public virtual SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes, int step) {
            return null;
        }
    }
}
