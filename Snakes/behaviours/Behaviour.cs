using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.models;
using Snakes.moves;

namespace Snakes.behaviours
{
    public abstract class Behaviour : IBehaviour
    {
        protected List<IMove> allMoves = new List<IMove>(){new MoveDown(), new MoveUp(), new MoveLeft(), new MoveRight()};
        public Cell CurrentCell { get; set; }
        public SnakeAction PrevAction { get; set; }
        public int SnakeHP { get; set; } 
        public virtual SnakeAction GetNextMove(IMove prevMove)
        {
            return new SnakeAction(prevMove, ActionType.MOVE);
        }
        public abstract SnakeAction NextStep();
    }
}
