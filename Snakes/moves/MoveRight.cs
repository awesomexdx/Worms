using Snakes.models;

namespace Snakes.moves
{
    public class MoveRight : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.X++;
            return cell;
        }
    }
}
