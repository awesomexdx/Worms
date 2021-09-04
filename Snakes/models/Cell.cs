using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.models
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CellContent Content { get; set; }

        public Cell(int x, int y, CellContent content)
        {
            X = x;
            Y = y;
            Content = content;
        }
    }
}
