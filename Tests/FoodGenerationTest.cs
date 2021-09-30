using FluentAssertions;
using NUnit.Framework;
using Snakes.behaviours;
using Snakes.models;
using Snakes.Services;
using Snakes.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    internal class FoodGenerationTest
    {
        private World world;

        private class PartialComparer : IEqualityComparer<Food>
        {
            public bool Equals(Food x, Food y)
            {
                return x.Cell.Equals(y.Cell);
            }

            public int GetHashCode(Food obj)
            {
                return obj.GetHashCode();
            }
        }

        [SetUp]
        public void Setup()
        {
            world = new World(new NameGenerator(), new FoodGenerator(), new SnakeActionsService(), new FileHandler());
            world.Foods.Clear();
            world.Snakes.Clear();
        }

        [Test]
        public void FoodGenerationOneTest()
        {
            Cell generatedCell = world.FoodGenerator.GenerateFood(new List<Food>(world.Foods), new List<Snake>(world.Snakes));
            world.Foods.Add(new Food(generatedCell));
            world.Foods.Count.Should().Be(1);
        }

        [Test]
        public void FoodGenerationOneHundredTest()
        {
            int oneHundred = 100;
            for (int i = 0; i < oneHundred; i++)
            {
                Cell generatedCell = world.FoodGenerator.GenerateFood(new List<Food>(world.Foods), new List<Snake>(world.Snakes));
                world.Foods.Add(new Food(generatedCell));
            }

            List<Food> uniqValuesList = world.Foods.Distinct<Food>(new FoodGenerationTest.PartialComparer()).ToList();
            uniqValuesList.Count.Should().Be(oneHundred);
            world.Foods.Count.Should().Be(oneHundred);
        }

        [Test]
        public void FoodGenerationOnSnakeTest()
        {
            int foodAmount = 900;
            Cell snakeCell = new Cell(0, 0);
            world.AddSnake(new Snake("test", snakeCell, new RandomBehaviour()));
            int i = 0;
            bool found = false;

            for (; i < foodAmount; i++)
            {
                Cell generatedCell = world.FoodGenerator.GenerateFood(new List<Food>(world.Foods), new List<Snake>(world.Snakes));
                Food newFood = new Food(generatedCell);
                world.Foods.Add(newFood);

                if (generatedCell.Equals(snakeCell))
                {
                    found = true;
                    world.Foods.Remove(newFood);
                    world.Snakes[0].HitPoints += 10;
                    break;
                }
            }

            if (!found)
            {
                world.Foods.Count.Should().Be(i + 1);
                world.Snakes[0].HitPoints.Should().Be(10);
            }
            else
            {
                world.Foods.Count.Should().Be(i);
                world.Snakes[0].HitPoints.Should().Be(20);
            }
        }
    }
}
