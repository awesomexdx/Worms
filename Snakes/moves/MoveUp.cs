using Snakes.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.moves
{
    class MoveUp : IMove
    {
        public Cell Move(Cell cell)
        {
            cell.Y++;
            return cell;
        }
    }
}
