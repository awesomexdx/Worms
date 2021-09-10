using Snakes.models;
using Snakes.moves;

namespace Snakes.behaviours
{
    public class RoundBehaviour : Behaviour
    {
        public RoundBehaviour()
        {
            PrevAction = new SnakeAction(allMoves[0], ActionType.NOTHING);
        }
        public override SnakeAction NextStep()
        {
            switch (PrevAction.ActionType)
            {
                case ActionType.NOTHING:
                    PrevAction = new SnakeAction(new MoveLeft(), ActionType.MOVE);
                    return PrevAction;
                case ActionType.MOVE:
                    return GetNextMove(PrevAction.Move);
                default:
                    return new SnakeAction(new MoveLeft(), ActionType.NOTHING);
            }
        }

        public override SnakeAction GetNextMove(IMove prevMove)
        {
            switch (prevMove)
            {
                case MoveLeft:
                    PrevAction = new SnakeAction(new MoveUp(), ActionType.MOVE);
                    return PrevAction;
                case MoveUp:
                    PrevAction = new SnakeAction(new MoveRight(), ActionType.MOVE);
                    return PrevAction;
                case MoveRight:
                    PrevAction = new SnakeAction(new MoveDown(), ActionType.MOVE);
                    return PrevAction;
                case MoveDown:
                    PrevAction = new SnakeAction(new MoveLeft(), ActionType.MOVE);
                    return PrevAction;
                default:
                    return PrevAction;
            }
        }
    }
}
