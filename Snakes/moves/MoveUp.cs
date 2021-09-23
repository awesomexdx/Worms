using Snakes.models;

namespace Snakes.moves
{
    public class MoveUp : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.Y++;
            return cell;
        }
    }
}
