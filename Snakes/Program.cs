using System;
using Snakes.behaviours;
using Snakes.models;
using Snakes.Utils;

namespace Snakes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting life...");
            World.Instance().AddSnake(new Snake(NameGenerator.GenerateNext(), new Cell(0,0,CellContent.Snake), new GoToFoodBehaviour(new Cell(0,0,CellContent.Snake))));
            World.Start();
        }
    }
}
