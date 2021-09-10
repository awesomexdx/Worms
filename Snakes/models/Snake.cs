using Snakes.behaviours;
using System;

namespace Snakes.models
{
    public class Snake
    {
        public string Name { get; set; }
        public Cell Cell { get; set; }
        public Behaviour Behaviour { get; set; }
        public int HitPoints { get; set; }
        public Snake(string name, int x, int y, IBehaviour behaviour)
        {
            Name = name;
            Cell = new Cell(x, y, CellContent.Snake);
            Behaviour = (Behaviour)behaviour;
            HitPoints = 10;
        }

        public Snake(string name, Cell cell, IBehaviour behaviour)
        {
            if (cell.Content == CellContent.Snake)
            {
                Name = name;
                Cell = cell;
                Behaviour = (Behaviour)behaviour;
                HitPoints = 10;
            }
            else
            {
                throw new ArgumentException("Unable to spawn snake in non snake cell");
            }
        }

        public SnakeAction Answer()
        {
            Behaviour.CurrentCell = Cell;
            Behaviour.SnakeHP = HitPoints;
            SnakeAction action = Behaviour.NextStep();
            //action.Move.Move(this.Cell);
            return action;
        }
    }
}
