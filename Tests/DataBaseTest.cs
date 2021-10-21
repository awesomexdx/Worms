using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Snakes.DataBase.Base;
using Snakes.DataBase.Repositories;
using Snakes.models;
using Snakes.Utils;

namespace Tests
{
    class DataBaseTest
    {
        private World world;
        private const int NUMBER_OF_STEPS = 100;
        private const string worldBehaviourName = "TestBehaviour";
        private MainDbContext GetContextWithData(World world)
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new MainDbContext(options);
            var models = world.simulator.StartForDbInMemory();

            foreach (var worldBehaviourModel in models)
            {
                context.WorldBehaviours.Add(worldBehaviourModel);
            }
            context.SaveChanges();

            return context;
        }

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
            this.world = new World(new FoodGenerator(), worldBehaviourName);
            this.world.WorldBehaviour = new WorldBehaviourRepository(GetContextWithData(this.world));
        }

        [Test]
        public void GenerationTestNumberOfSteps()
        {
            var rows = this.world.WorldBehaviour.Get();
            rows.Count().Should().Be(NUMBER_OF_STEPS);
        }

        [Test]
        public void GenerationTestUniqFood()
        {
            var rows = this.world.WorldBehaviour.Get();
            List<Food> foods = new();
            foreach (var worldBehaviourModel in rows)
            {
                foods.Add(new Food(new Cell(worldBehaviourModel.FoodX,worldBehaviourModel.FoodY)));
            }

            int count = 0;
            for (int i = 0; i < foods.Count; i++)
            {
                for (int j = i + 1; j < foods.Count; j++)
                {
                    if (foods[i].Cell.Equals(foods[j].Cell) && j - i < 11)
                    {
                        count++;
                        Console.WriteLine($"first: {foods[i].Cell.X} {foods[i].Cell.Y} second {foods[j].Cell.X} {foods[j].Cell.Y}");
                        Console.WriteLine($"first: {i} second {j}");
                    }
                }
            }

            count.Should().Be(0);
        }

        [Test]
        public void GenerationTestBehaviourName()
        {
            var rows = this.world.WorldBehaviour.Get();
            foreach (var worldBehaviourModel in rows)
            {
                worldBehaviourModel.Name.Should().Be(worldBehaviourName);
            }
        }
    }
}
