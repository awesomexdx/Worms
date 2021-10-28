using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.models
{
    public class JsonWorm
    {
        public string name { get; set; }
        public int lifeStrength { get; set; }
        public JsonCell position { get; set; }
    }
}
