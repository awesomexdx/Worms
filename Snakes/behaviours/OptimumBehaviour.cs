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
        public static int hpAvg = 65;
        public static int stepTo = 33;
        public static int hpMin = 25;
        public static int maxSnakes = 4;
        public override SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes, int step)
        {
            if (foods.Count == 0)
            {
                return new SnakeAction(new MoveUp(), ActionType.MOVE);//new MoveNoWhere(), ActionType.NOTHING);
            }

            List<double> distanceList = new List<double>();
            Dictionary<double,Cell> distanceAndCellList = new Dictionary<double,Cell>();
            foreach (Food food in foods)
            {
                distanceList.Add(Math.Abs(food.Cell.X - snake.Cell.X) + Math.Abs(food.Cell.Y - snake.Cell.Y));
                distanceAndCellList[Math.Abs(food.Cell.X - snake.Cell.X) + Math.Abs(food.Cell.Y - snake.Cell.Y)] = food.Cell;
            }

            int minDistanceIndex = distanceList.IndexOf(distanceList.Min());

            int myIndex = snakes.IndexOf(snakes.Where(x => x.Name == snake.Name).First());

            /*if (snakes.Count <= 4 && snakes.Count >= 3)
            {
                int minX = (myIndex - 2) < 0 ? 0 : 10000;
                int maxX = (myIndex - 2) < 0 ? 10000 : 0;
                int minY = (myIndex % 3) == 0 ? 0 : 10000;
                int maxY = (myIndex % 3) == 0 ? 10000 : 0;

                minDistanceIndex = distanceList.IndexOf(distanceAndCellList.Where(x => x.Value.X > minX && x.Value.X < maxX && x.Value.Y > minY && x.Value.Y < maxY).Select(x => x.Key).FirstOrDefault());
                if (minDistanceIndex == -1)
                {
                    minDistanceIndex = 10000;
                }
            }*/

            int Xcentre = (myIndex - 2) < 0 ? 3 : -3;
            int Ycentre = (myIndex % 3) == 0 ? 3 : -3;

            Cell b = minDistanceIndex != 10000 ? foods.ElementAt(minDistanceIndex).Cell : new Cell() { X = Xcentre, Y = Ycentre };
            int timeToLive = minDistanceIndex != 10000 ? foods.ElementAt(minDistanceIndex).TimeToLive : 10000;

            SnakeAction action = GetNextMove(snake.Cell,
                b,
                snake.HitPoints, timeToLive, Xcentre, Ycentre);

            var nextCell = action.Move.Move(snake.Cell);
            var isFoodThere = foods.Select(x => x.Cell).Contains(nextCell);
            var isSnakeThere = snakes.Where(x => !x.Cell.Equals(snake.Cell)).Select(x => x.Cell).Contains(nextCell);

            if (isSnakeThere)
            {
                for (int i=0; i<3; i++)
                {
                    action = GetRoundMove(action.Move);
                    var newNextCell = action.Move.Move(snake.Cell);
                    var isSnakeThereCheck = snakes.Where(x => x.Cell.X != snake.Cell.X && x.Cell.Y != snake.Cell.Y).Select(x => x.Cell).Contains(newNextCell);
                    if (!isSnakeThere) { break; }
                }
            }

            if (SnakeHP > hpAvg && snakes.Count < 4 && !isFoodThere)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            if (step > 89 && SnakeHP > 10 && !isFoodThere)
            //if (100 - step - 2 <= SnakeHP * 0.1 && SnakeHP > 10 && !isFoodThere)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            if ((step < stepTo && SnakeHP > hpMin) && !isFoodThere)
            {
                action.ActionType = ActionType.REPRODUCE;
            }

            return action;
        }

        private SnakeAction GetNextMove(Cell a, Cell b, int snakeHP, int foodHP, int x, int y)
        {
            var distance = Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

            if (distance > snakeHP || distance > foodHP)
            {
                return GetNextMove(a, new Cell() { X = x, Y = y });
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
                snake.HitPoints, foods.ElementAt(minDistanceIndex).TimeToLive,0,0);

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
