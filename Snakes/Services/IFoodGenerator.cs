using Snakes.models;
using System.Collections.Generic;

namespace Snakes.Services
{
    public interface IFoodGenerator
    {
        public Cell GenerateFood(List<Food> foods, List<Snake> snakes);
    }
}
