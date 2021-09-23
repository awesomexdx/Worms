using Snakes.models;

namespace Snakes.Services
{
    public interface IFoodGenerator
    {
        public Cell GenerateFood(World world);
    }
}
