using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.models;
using Snakes.moves;

namespace Snakes.behaviours
{
    class OptimumBehaviour : Behaviour
    {
        public override SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes)
        {
            if (foods.Count == 0)
            {
                return new SnakeAction(new MoveNoWhere(), ActionType.NOTHING);
            }

            List<double> distanceList = new List<double>();
            foreach (Food food in foods)
            {
                distanceList.Add(Math.Abs(food.Cell.X - snake.Cell.X) + Math.Abs(food.Cell.Y - snake.Cell.Y));
            }

            int minDistanceIndex = distanceList.IndexOf(distanceList.Min());

            SnakeAction action = GetNextMove(snake.Cell, foods.ElementAt(minDistanceIndex).Cell);

            if (SnakeHP > 15)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            return action;
        }

        public SnakeAction GetNextMove(Cell a, Cell b)
        {
            if (a.X == b.X)
            {
                if (a.Y == b.Y)
                {
                    return new SnakeAction(new MoveNoWhere(), ActionType.NOTHING);
                }
                else
                {
                    if (a.Y > b.Y)
                    {
                        return new SnakeAction(new MoveDown(), ActionType.MOVE);
                    }
                    else
                    {
                        return new SnakeAction(new MoveUp(), ActionType.MOVE);
                    }
                }
            }
            else
            {
                if (a.X > b.X)
                {
                    return new SnakeAction(new MoveLeft(), ActionType.MOVE);
                }
                else
                {
                    return new SnakeAction(new MoveRight(), ActionType.MOVE);
                }
            }
        }
    }
}
