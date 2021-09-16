using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.models;

namespace Snakes.Services
{
    public interface IFoodGenerator
    {
        public void GenerateFood(World world);
    }
}
