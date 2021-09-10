using Snakes.models;

namespace Snakes.moves
{
    internal class MoveDown : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.Y--;
            return cell;
        }
    }
}
