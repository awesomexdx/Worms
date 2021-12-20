using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.models;
using Snakes.moves;

namespace Snakes.behaviours
{
    public class OptimumBehaviour : Behaviour
    {
        public static int max_steps = 100;
        public static int hpAvg = 88;//65;
        public static int stepTo = 21;//33;
        public static int hpMin = 20;//25;
        public static int maxSnakes = 4;
        public override SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes, int step)
        {
            if (foods.Count == 0)
            {
                return new SnakeAction(new MoveUp(), ActionType.MOVE);//new MoveNoWhere(), ActionType.NOTHING);
            }

            List<double> distanceList = new List<double>();
            foreach (Food food in foods)
            {
                distanceList.Add(Math.Abs(food.Cell.X - snake.Cell.X) + Math.Abs(food.Cell.Y - snake.Cell.Y));
            }

            int minDistanceIndex = distanceList.IndexOf(distanceList.Min());

            SnakeAction action = GetNextMove(snake.Cell,
                foods.ElementAt(minDistanceIndex).Cell,
                snake.HitPoints, foods.ElementAt(minDistanceIndex).TimeToLive);

            var nextCell = action.Move.Move(snake.Cell);
            var isFoodThere = foods.Select(x => x.Cell).Contains(nextCell);
            var isSnakeThere = snakes.Where(x => x.Cell.X != snake.Cell.X && x.Cell.Y != snake.Cell.Y).Select(x => x.Cell).Contains(nextCell);

            if (isSnakeThere)
            {
                for (int i=0; i<4; i++)
                {
                    action = GetRoundMove(action.Move);
                    var newNextCell = action.Move.Move(snake.Cell);
                    var isSnakeThereCheck = snakes.Where(x => x.Cell.X != snake.Cell.X && x.Cell.Y != snake.Cell.Y).Select(x => x.Cell).Contains(newNextCell);
                    if (!isSnakeThere) { break; }
                }
            }

            if (SnakeHP > hpAvg && !isFoodThere)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            if (step > 89 && (100 - step) < SnakeHP && SnakeHP > 10 && !isFoodThere)
            //if (max_steps - 1 - step > SnakeHP/10 && !isFoodThere)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            if ((/*step < stepTo &&*/ SnakeHP > hpMin && snakes.Count < maxSnakes) && !isFoodThere)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            return action;
        }

        private SnakeAction GetNextMove(Cell a, Cell b, int snakeHP, int foodHP)
        {
            var distance = Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

            if (distance > snakeHP || distance > foodHP)
            {
                return GetNextMove(a, new Cell() { X = 0, Y = 0 });
            }

            if (a.X == b.X)
            {
                if (a.Y == b.Y)
                {
                    return new SnakeAction(new MoveUp(), ActionType.MOVE);//new MoveNoWhere(), ActionType.NOTHING);
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

        private SnakeAction GetRoundMove(IMove prevMove)
        {
            switch (prevMove)
            {
                case MoveLeft:
                    return new SnakeAction(new MoveUp(), ActionType.MOVE);
                case MoveUp:
                    return new SnakeAction(new MoveRight(), ActionType.MOVE);
                case MoveRight:
                    return new SnakeAction(new MoveDown(), ActionType.MOVE);
                case MoveDown:
                    return new SnakeAction(new MoveLeft(), ActionType.MOVE);
                default:
                    return new SnakeAction(new MoveUp(), ActionType.MOVE);
            }
        }

        public SnakeAction GetNextMove(Cell a, Cell b)
        {

            if (a.X == b.X)
            {
                if (a.Y == b.Y)
                {
                    return new SnakeAction(new MoveUp(), ActionType.MOVE);//new MoveNoWhere(), ActionType.NOTHING);
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

        public override SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes)
        {
            if (foods.Count == 0)
            {
                return new SnakeAction(new MoveUp(), ActionType.MOVE);//new MoveNoWhere(), ActionType.NOTHING);
            }

            List<double> distanceList = new List<double>();
            foreach (Food food in foods)
            {
                distanceList.Add(Math.Abs(food.Cell.X - snake.Cell.X) + Math.Abs(food.Cell.Y - snake.Cell.Y));
            }

            int minDistanceIndex = distanceList.IndexOf(distanceList.Min());

            SnakeAction action = GetNextMove(snake.Cell,
                foods.ElementAt(minDistanceIndex).Cell,
                snake.HitPoints, foods.ElementAt(minDistanceIndex).TimeToLive);

            var nextCell = action.Move.Move(snake.Cell);
            var isFoodThere = foods.Select(x => x.Cell).Contains(nextCell);
            var isSnakeThere = snakes.Where(x => x.Cell.X != snake.Cell.X && x.Cell.Y != snake.Cell.Y).Select(x => x.Cell).Contains(nextCell);

            if (isSnakeThere)
            {
                action = GetRoundMove(action.Move);
            }

            if (SnakeHP > 15 && !isFoodThere)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            return action;
        }
    }
}
