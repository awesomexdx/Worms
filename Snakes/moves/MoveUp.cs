using Snakes.models;

namespace Snakes.moves
{
    internal class MoveUp : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.Y++;
            return cell;
        }
    }
}
