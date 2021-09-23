using Snakes.models;

namespace Snakes.moves
{
    public class MoveNoWhere : IMove
    {
        public Cell Move(Cell cell)
        {
            return cell;
        }
    }
}
