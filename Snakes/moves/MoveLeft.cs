using Snakes.models;

namespace Snakes.moves
{
    public class MoveLeft : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.X--;
            return cell;
        }
    }
}
