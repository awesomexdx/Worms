using Snakes.models;
using System;

namespace Snakes.behaviours
{
    public class RandomBehaviour : Behaviour
    {
        public override SnakeAction NextStep()
        {
            return new SnakeAction(allMoves[(new Random().Next(0, allMoves.Count))], (ActionType)(new Random().Next(0, 3)));
        }
    }
}
