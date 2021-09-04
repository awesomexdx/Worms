using Snakes.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.behaviours
{
    class RandomBehaviour : Behaviour, IBehaviour
    {
        public SnakeAction NextStep()
        {
            return new SnakeAction(allMoves[(new Random().Next(0, allMoves.Count))], (ActionType) (new Random().Next(0, 3)));
        }
    }
}
