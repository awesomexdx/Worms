using Snakes.behaviours;

namespace Snakes.models
{
    public class Snake
    {
        public string Name { get; set; }
        public Cell Cell { get; set; }
        public Behaviour Behaviour { get; set; }
        public int HitPoints { get; set; }

        public int FoodToGoIndex { get; set; }
        public Snake(string name, int x, int y, IBehaviour behaviour)
        {
            Name = name;
            Cell = new Cell(x, y);
            Behaviour = (Behaviour)behaviour;
            HitPoints = 10;
        }

        public Snake(string name, Cell cell, IBehaviour behaviour)
        {
            Name = name;
            Cell = cell;
            Behaviour = (Behaviour)behaviour;
            HitPoints = 10;
        }

        public Snake(string name, int x, int y)
        {
            Name = name;
            Cell = new Cell(x, y);
            Behaviour = new OptimumBehaviour();
            HitPoints = 10;
        }

        public Snake(Snake snake)
        {
            Name = snake.Name;
            Cell = snake.Cell;
            HitPoints = snake.HitPoints;
        }
    }
}
