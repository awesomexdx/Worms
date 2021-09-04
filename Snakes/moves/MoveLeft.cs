using Snakes.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.moves
{
    class MoveLeft : IMove
    {
        public void Move(Cell cell)
        {
            cell.X--;
        }
    }
}
