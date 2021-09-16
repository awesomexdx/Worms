using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.models;

namespace Snakes.Services
{
    public interface ISnakeActionsService
    {
        public SnakeAction Answer(Snake snake, World world);
    }
}
