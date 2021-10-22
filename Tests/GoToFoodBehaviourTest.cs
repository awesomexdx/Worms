using FluentAssertions;
using NUnit.Framework;
using Snakes.behaviours;
using Snakes.models;
using Snakes.Services;
using Snakes.Utils;
using System.Collections.Generic;

namespace Tests
{
    internal class GoToFoodBehaviourTest
    {
        private World world;

        [SetUp]
        public void Setup()
        {
            world = new World(new NameGenerator(), new FoodGenerator(), new SnakeActionsService(), new FileHandler());
            world.Foods.Clear();
            world.Snakes.Clear();
            world.AddSnake(new Snake("Test", 0, 0, new GoToFoodBehaviour()));
        }

        [Test]
        public void NoFoodTest()
        {
            Snake snake = world.Snakes[0];
            int steps = 10;
            for (int i = 0; i < steps; i++)
            {
                snake.Cell = snake.Behaviour.NextStep(new Snake(snake), new List<Food>(world.Foods), null).Move.Move(snake.Cell);
                snake.Cell.Should().Be(new Cell(0, 0));
            }
        }

        [Test]
        public void GoToFoodByXTest()
        {
            Snake snake = world.Snakes[0];
            int steps = 10;
            world.Foods.Add(new Food(new Cell(steps, 0)));
            for (int i = 0; i < steps; i++)
            {
                snake.Cell = snake.Behaviour.NextStep(new Snake(snake), new List<Food>(world.Foods), null).Move.Move(snake.Cell);
                snake.Cell.Should().Be(new Cell(i + 1, 0));
            }
        }

        [Test]
        public void GoToFoodByYTest()
        {
            Snake snake = world.Snakes[0];
            int steps = 10;
            world.Foods.Add(new Food(new Cell(0, steps)));
            for (int i = 0; i < steps; i++)
            {
                snake.Cell = snake.Behaviour.NextStep(new Snake(snake), new List<Food>(world.Foods), null).Move.Move(snake.Cell);
                snake.Cell.Should().Be(new Cell(0, i + 1));
            }
        }

        [Test]
        public void GoToFoodByXAndYTest()
        {
            Snake snake = world.Snakes[0];
            int steps = 10;
            world.Foods.Add(new Food(new Cell(steps, steps)));
            for (int i = 0; i < steps * 2; i++)
            {
                snake.Cell = snake.Behaviour.NextStep(new Snake(snake), new List<Food>(world.Foods), null).Move.Move(snake.Cell);
                if (snake.Cell.X < steps)
                {
                    snake.Cell.Should().Be(new Cell(i + 1, 0));
                }
                else
                {
                    snake.Cell.Should().Be(new Cell(steps, i - steps + 1));
                }
            }
        }
    }
}
