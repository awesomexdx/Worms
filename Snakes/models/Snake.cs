using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.behaviours;
using Snakes.moves;

namespace Snakes.models
{
    class Snake
    {
        public string Name { get; set; }
        public Cell Cell { get; set; }
        public IBehaviour Behaviour { get; set; }
        public int HitPoints { get; set; }
        public Snake(string name, int x, int y, IBehaviour behaviour)
        {
            this.Name = name;
            this.Cell = new Cell(x, y, CellContent.Snake);
            this.Behaviour = behaviour;
            this.HitPoints = 10;
        }

        public Snake(string name, Cell cell, IBehaviour behaviour)
        {
            if (cell.Content == CellContent.Snake)
            {
                this.Name = name;
                this.Cell = cell;
                this.Behaviour = behaviour;
                this.HitPoints = 10;
            }
            else
            {
                throw new ArgumentException("Unable to spawn snake in non snake cell");
            }
        }

        public void Answer()
        {
            this.Behaviour.NextStep().Move.Move(this.Cell);
        }
    }
}
