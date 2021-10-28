using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.models
{
    public class JsonWorld
    {
        public List<JsonWorm> worms { get; set; }
        public List<JsonFood> foods { get; set; }
    }
}
