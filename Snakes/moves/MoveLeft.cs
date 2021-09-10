using Snakes.models;

namespace Snakes.moves
{
    internal class MoveLeft : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.X--;
            return cell;
        }
    }
}
