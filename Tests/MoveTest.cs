using FluentAssertions;
using NUnit.Framework;
using Snakes.behaviours;
using Snakes.models;
using Snakes.moves;
using Snakes.Services;
using Snakes.Simulation;
using Snakes.Utils;
using System.Collections.Generic;

namespace Tests
{
    internal class MoveTest
    {
        private List<Snake> newSnakes = new List<Snake>() { new Snake("New", new Cell(5, 5), new RandomBehaviour()) };
        private World world;

        [SetUp]
        public void Setup()
        {
            world = new World(new NameGenerator(), new FoodGenerator(), new SnakeActionsService(), new FileHandler());
            world.Foods = new List<Food>() { new Food(new Cell(1, 0)) };
            world.AddSnake(new Snake("John", new Cell(0, 0),
                new GoToFoodBehaviour(new Cell(0, 0), world)));
        }

        [Test]
        public void IsCellOccupiedTest()
        {
            CellContent cellContent = SimulationHelper.isCellOccupied(world, new Cell(2, 0), newSnakes);
            cellContent.Should().Be(CellContent.Void);

            cellContent = SimulationHelper.isCellOccupied(world, new Cell(1000, 1000), newSnakes);
            cellContent.Should().Be(CellContent.Void);

            cellContent = SimulationHelper.isCellOccupied(world, new Cell(1, 0), newSnakes);
            cellContent.Should().Be(CellContent.Food);

            cellContent = SimulationHelper.isCellOccupied(world, new Cell(0, 0), newSnakes);
            cellContent.Should().Be(CellContent.Snake);

            cellContent = SimulationHelper.isCellOccupied(world, new Cell(5, 5), newSnakes);
            cellContent.Should().Be(CellContent.Snake);
        }

        [Test]
        public void ActionMoveTest()
        {
            SnakeAction actionMoveDown = new SnakeAction(new MoveDown(), ActionType.MOVE);
            SnakeAction actionMoveUp = new SnakeAction(new MoveUp(), ActionType.MOVE);
            SnakeAction actionMoveLeft = new SnakeAction(new MoveLeft(), ActionType.MOVE);
            SnakeAction actionMoveRight = new SnakeAction(new MoveRight(), ActionType.MOVE);
            SnakeAction actionMoveNoWhere = new SnakeAction(new MoveNoWhere(), ActionType.MOVE);

            Cell newCellDown = actionMoveDown.Move.Move(new Cell(0, 0));
            Cell newCellUp = actionMoveUp.Move.Move(new Cell(0, 0));
            Cell newCellLeft = actionMoveLeft.Move.Move(new Cell(0, 0));
            Cell newCellRight = actionMoveRight.Move.Move(new Cell(0, 0));
            Cell newCellNoWhere = actionMoveNoWhere.Move.Move(new Cell(0, 0));

            newCellDown.X.Should().Be(0);
            newCellDown.Y.Should().Be(-1);

            newCellUp.X.Should().Be(0);
            newCellUp.Y.Should().Be(1);

            newCellLeft.X.Should().Be(-1);
            newCellLeft.Y.Should().Be(0);

            newCellRight.X.Should().Be(1);
            newCellRight.Y.Should().Be(0);

            newCellNoWhere.X.Should().Be(0);
            newCellNoWhere.Y.Should().Be(0);
        }

        [Test]
        public void ResolveMoveToFoodTest()
        {
            int oldHp = world.Snakes[0].HitPoints;
            Cell newCell = new Cell(1, 0);
            SimulationHelper.ResolseMove(world, newCell, world.Snakes[0], newSnakes);

            world.Snakes[0].HitPoints.Should().Be(oldHp + 10);
            world.Foods.Count.Should().Be(0);
            world.Snakes[0].Cell.Should().Be(newCell);
        }

        [Test]
        public void ResolveMoveToSnakeTest()
        {
            Cell startCell = new Cell(9, 10);
            Cell oldCell = new Cell(10, 10);
            world.Snakes[0].Cell = startCell;
            world.Snakes.Add(new Snake("test", oldCell, new RoundBehaviour()));
            SimulationHelper.ResolseMove(world, startCell, world.Snakes[1], newSnakes);

            world.Snakes[1].HitPoints.Should().Be(10);
            world.Snakes[1].Cell.Should().Be(oldCell);
        }

        [Test]
        public void ResolveMoveToFreeCellTest()
        {
            Cell oldCell = new Cell(10, 10);
            world.Snakes[0].Cell = oldCell;
            Cell freeCell = new Cell(11, 10);
            SimulationHelper.ResolseMove(world, freeCell, world.Snakes[0], newSnakes);
            world.Snakes[0].Cell.Should().Be(freeCell);
        }

        [Test]
        public void ResolveReproduceToFoodTest()
        {
            int starthp = 15;
            newSnakes = new List<Snake>();

            world.Snakes[0].HitPoints = starthp;
            Cell oldCell = world.Snakes[0].Cell;
            Cell newCell = new Cell(1, 0);
            SimulationHelper.ResolveReproduce(world, newCell, world.Snakes[0], newSnakes);

            newSnakes.Count.Should().Be(0);
            world.Snakes[0].HitPoints.Should().Be(starthp);
            world.Foods.Count.Should().Be(1);
            world.Snakes[0].Cell.Should().Be(oldCell);
        }

        [Test]
        public void ResolseReproduceToVoidTest()
        {
            int starthp = 15;
            Cell newCell = new Cell(-9, 0);
            Cell oldCell = new Cell(-10, 0);
            world.Snakes[0].Cell = oldCell;
            world.Snakes[0].HitPoints = starthp;
            newSnakes.Clear();
            SimulationHelper.ResolveReproduce(world, newCell, world.Snakes[0], newSnakes);

            world.Snakes[0].HitPoints.Should().Be(starthp - 10);
            world.Snakes[0].Cell.Should().Be(oldCell);
            newSnakes.Count.Should().Be(1);
            newSnakes[0].Cell.Should().Be(newCell);
            newSnakes[0].HitPoints.Should().Be(10);
        }

        [Test]
        public void ResolveReproduceToSnakeTest()
        {
            int startHp = 15;
            Cell startCell = new Cell(9, 10);
            Cell newCell = new Cell(10, 10);
            world.Snakes[0].Cell = startCell;
            world.Snakes[0].HitPoints = startHp;
            newSnakes.Clear();
            SimulationHelper.ResolveReproduce(world, newCell, world.Snakes[0], newSnakes);

            newSnakes.Count.Should().Be(1);
            newSnakes[0].HitPoints.Should().Be(10);
            newSnakes[0].Cell.Should().Be(newCell);

            world.Snakes[0].HitPoints.Should().Be(startHp - 10);
            world.Snakes[0].Cell.Should().Be(startCell);
        }

        [Test]
        public void ResolveReproduceWnenNoHPTest()
        {

            Cell newCell = new Cell(0, -1);
            Cell oldCell = world.Snakes[0].Cell;
            world.Snakes[0].HitPoints = 5;
            SimulationHelper.ResolveReproduce(world, newCell, world.Snakes[0], newSnakes);

            newSnakes.Count.Should().Be(1);
            world.Snakes[0].HitPoints.Should().Be(5);
            world.Foods.Count.Should().Be(1);
            world.Snakes[0].Cell.Should().Be(oldCell);
        }
    }
}
