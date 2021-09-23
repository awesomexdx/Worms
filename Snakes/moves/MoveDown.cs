using Snakes.models;

namespace Snakes.moves
{
    public class MoveDown : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.Y--;
            return cell;
        }
    }
}
