using Snakes.behaviours;
using Snakes.models;
using Snakes.Utils;
using System;

namespace Snakes
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting life...");
            World.Instance().AddSnake(new Snake(NameGenerator.GenerateNext(), new Cell(0, 0, CellContent.Snake), new GoToFoodBehaviour(new Cell(0, 0, CellContent.Snake))));
            World.Start();
        }
    }
}
