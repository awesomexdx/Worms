using Snakes.behaviours;
using Snakes.models;
using System.Collections.Generic;
using System.Linq;

namespace Snakes.Simulation
{
    public class SimulationHelper
    {
        private const int SNAKE_REWARD = 10;
        private const int REPRODUCTION_PRICE = 10;

        public static CellContent isCellOccupied(World world, Cell cell, List<Snake> newSnakes)
        {
            foreach (Snake snake in world.Snakes)
            {
                if (snake.Cell.X == cell.X && snake.Cell.Y == cell.Y)
                {
                    return CellContent.Snake;
                }
            }

            foreach (Snake snake in newSnakes)
            {
                if (snake.Cell.X == cell.X && snake.Cell.Y == cell.Y)
                {
                    return CellContent.Snake;
                }
            }

            foreach (Food food in world.Foods)
            {
                if (food.Cell.X == cell.X && food.Cell.Y == cell.Y)
                {
                    return CellContent.Food;
                }
            }

            return CellContent.Void;
        }

        public static void ResolseMove(World world, Cell newCell, Snake snake, List<Snake> newSnakes)
        {
            CellContent cellContent = SimulationHelper.isCellOccupied(world, newCell, newSnakes);
            if (cellContent == CellContent.Food)
            {
                snake.HitPoints += SNAKE_REWARD;
                world.Foods.RemoveAt(world.Foods.IndexOf(world.Foods.First(x => x.Cell.X == newCell.X && x.Cell.Y == newCell.Y)));
                snake.Cell = newCell;
            }
            else if (cellContent == CellContent.Void)
            {
                snake.Cell = newCell;
            }
        }

        public static void ResolveReproduce(World world, Cell newCell, Snake snake, List<Snake> newSnakes)
        {
            CellContent cellContent = isCellOccupied(world, newCell, newSnakes);
            if (cellContent == CellContent.Void && snake.HitPoints >= 11)
            {
                snake.HitPoints -= REPRODUCTION_PRICE;
                newSnakes.Add(new Snake(world.NameGenerator.GenerateNext(), newCell.X, newCell.Y, new GoToFoodBehaviour()));
            } else if (cellContent == CellContent.Food && snake.HitPoints >= 11)
            {
                snake.HitPoints -= REPRODUCTION_PRICE;
                var newSnake = new Snake(world.NameGenerator.GenerateNext(), newCell.X, newCell.Y,
                    new GoToFoodBehaviour());
                newSnake.HitPoints += SNAKE_REWARD;
                world.Foods.RemoveAt(world.Foods.IndexOf(world.Foods.First(x => x.Cell.X == newCell.X && x.Cell.Y == newCell.Y)));
                newSnakes.Add(newSnake);
            }
        }
        public static void ResolveAction(World world, SnakeAction action, Snake snake, List<Snake> newSnakes)
        {
            Cell newCell = action.Move.Move(new Cell(snake.Cell.X, snake.Cell.Y));

            switch (action.ActionType)
            {
                case ActionType.MOVE:
                    ResolseMove(world, newCell, snake, newSnakes);
                    break;
                case ActionType.NOTHING:
                    break;
                case ActionType.REPRODUCE:
                    ResolveReproduce(world, newCell, snake, newSnakes);
                    break;
                default:
                    break;
            }
        }
    }
}
