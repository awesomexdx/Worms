using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Snakes.behaviours;
using Snakes.DataBase.Base;
using Snakes.DataBase.Repositories;
using Snakes.models;
using Snakes.Services;
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
            world.WorldBehaviour = new WorldBehaviourRepository(context);
            var models = world.simulator.StartForDb();

            return context;
        }

        private MainDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new MainDbContext(options);

            return context;
        }

        private World GetNewWorld()
        {
            World localWorld = new World(new FoodGenerator(), worldBehaviourName);
            localWorld.FileHandlerService = new FileHandler();
            localWorld.SnakeActionsService = new SnakeActionsService();
            localWorld.NameGenerator = new NameGenerator();

            localWorld.WorldBehaviour = new WorldBehaviourRepository(GetContext());
            localWorld.AddSnake(new Snake("John", new Cell(0, 0),
                new GoToFoodBehaviour()));
            return localWorld;
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
            this.world.FileHandlerService = new FileHandler();
            this.world.SnakeActionsService = new SnakeActionsService();
            this.world.NameGenerator = new NameGenerator();
            
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

        [Test]
        public void TestWormBehaviourInDbWorld()
        {
            var localWorld = GetNewWorld();
            var gameSession1 = localWorld.simulator.SimulateBehaviourByName(worldBehaviourName);
            localWorld = GetNewWorld();
            var gameSession2 = localWorld.simulator.SimulateBehaviourByName(worldBehaviourName);

            gameSession1.FoodList.Should().BeEquivalentTo(gameSession2.FoodList);
            gameSession1.SnakeList.Should().BeEquivalentTo(gameSession2.SnakeList);
        }
    }
}
