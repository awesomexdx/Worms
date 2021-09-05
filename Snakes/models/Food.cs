using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.models
{
    public class Food
    {
        public Cell Cell { get; set; }
        public int TimeToLive { get; set; }

        public Food(Cell cell)
        {
            this.Cell = cell;
            TimeToLive = 10;
        }
    }
}
