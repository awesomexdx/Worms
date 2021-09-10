using Snakes.models;

namespace Snakes.moves
{
    internal class MoveRight : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.X++;
            return cell;
        }
    }
}
