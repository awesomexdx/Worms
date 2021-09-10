using Snakes.models;

namespace Snakes.moves
{
    internal class MoveNoWhere : IMove
    {
        public Cell Move(Cell cell)
        {
            return cell;
        }
    }
}
