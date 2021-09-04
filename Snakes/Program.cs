using System;
using Snakes.behaviours;
using Snakes.models;

namespace Snakes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting life...");
            World.Instance().AddSnake(new Snake("Jonh", new Cell(0,0,CellContent.Snake), new RoundBehaviour()));
            World.Start();
        }
    }
}
