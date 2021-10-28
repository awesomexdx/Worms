using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakes.models
{
    public class JsonFood
    {
        public int expiresIn { get; set; }
        public JsonCell position { get; set; }
    }
}
