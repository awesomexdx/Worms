using FluentAssertions;
using NUnit.Framework;
using Snakes.models;
using Snakes.Services;
using Snakes.Utils;
using System.IO;

namespace Tests
{
    internal class FileHandlerTest
    {
        private World world;
        [SetUp]
        public void Setup()
        {
            world = new World(new NameGenerator(), new FoodGenerator(), new SnakeActionsService(), new FileHandler());
            world.Foods.Clear();
            world.Snakes.Clear();
        }

        [Test]
        public void GetCurrentStateTest()
        {
            world.AddSnake(new Snake("John", 5, 5));
            world.AddSnake(new Snake("John2", 6, 6));
            world.Foods.Add(new Food(new Cell(1, 1)));
            world.Foods.Add(new Food(new Cell(2, 2)));

            string state = "Worms:[John-10(5,5),John2-10(6,6)],Food:[(1,1),(2,2)]";

            world.GetCurrentState().Should().Be(state);
        }

        [Test]
        public void WriteToStreamTest()
        {
            world.AddSnake(new Snake("John", 5, 5));
            world.AddSnake(new Snake("John2", 6, 6));
            world.AddSnake(new Snake("John3", 7, 7));
            world.Foods.Add(new Food(new Cell(1, 1)));
            world.Foods.Add(new Food(new Cell(2, 2)));

            string state = "Worms:[John-10(5,5),John2-10(6,6),John3-10(7,7)],Food:[(1,1),(2,2)]";

            StringWriter stringWriter = new StringWriter();
            world.FileHandlerService.TextWriter = stringWriter;

            world.FileHandlerService.WriteToTextWriter(world.GetCurrentState());

            world.FileHandlerService.TextWriter.Close();

            stringWriter.ToString().Trim().Should().Be(state);
        }

        [Test]
        public void WriteToFileTest()
        {
            world.AddSnake(new Snake("John", 5, 5));
            world.AddSnake(new Snake("John2", 6, 6));
            world.AddSnake(new Snake("John3", 7, 7));
            world.Foods.Add(new Food(new Cell(1, 1)));
            world.Foods.Add(new Food(new Cell(2, 2)));

            string fileName = "test.txt";

            string state = "Worms:[John-10(5,5),John2-10(6,6),John3-10(7,7)],Food:[(1,1),(2,2)]";

            world.FileHandlerService.TextWriter = File.CreateText(fileName);

            world.FileHandlerService.WriteToTextWriter(world.GetCurrentState());

            world.FileHandlerService.TextWriter.Close();
            byte[] bytes = new byte[state.Length];

            using (FileStream fstream = new FileStream(fileName, FileMode.Open))
            {
                fstream.Read(bytes, 0, state.Length);
            }

            System.Text.Encoding.Default.GetString(bytes).Trim().Should().Be(state);
        }
    }
}
