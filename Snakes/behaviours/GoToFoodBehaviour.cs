using Snakes.models;
using Snakes.moves;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snakes.behaviours
{
    public class GoToFoodBehaviour : Behaviour
    {
        private readonly List<Food> foods;
        public GoToFoodBehaviour(Cell startPosition)
        {
            this.foods = World.Instance().Foods;
            this.CurrentCell = new Cell(startPosition.X, startPosition.Y, startPosition.Content);
        }
        public override SnakeAction NextStep()
        {
            if (foods.Count == 0)
            {
                return new SnakeAction(new MoveNoWhere(), ActionType.NOTHING);
            }

            List<double> distanceList = new List<double>();
            foreach (var food in foods)
            {
                distanceList.Add(Math.Abs(food.Cell.X - CurrentCell.X) + Math.Abs(food.Cell.Y - CurrentCell.Y));
            }

            int minDistanceIndex = distanceList.IndexOf(distanceList.Min());

            SnakeAction action = GetNextMove(CurrentCell, foods.ElementAt(minDistanceIndex).Cell);

            if (this.SnakeHP > 30)
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
