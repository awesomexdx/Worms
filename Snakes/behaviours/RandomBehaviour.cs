using Snakes.models;
using System;
using System.Collections.Generic;

namespace Snakes.behaviours
{
    public class RandomBehaviour : Behaviour
    {
        public override SnakeAction NextStep(Snake snake, List<Food> foods, List<Snake> snakes)
        {
            return new SnakeAction(allMoves[(new Random().Next(0, allMoves.Count))], (ActionType)(new Random().Next(0, 3)));
        }
    }
}
