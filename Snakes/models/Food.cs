namespace Snakes.models
{
    public class Food
    {
        public Cell Cell { get; set; }
        public int TimeToLive { get; set; }

        public Food(Cell cell)
        {
            Cell = cell;
            TimeToLive = 10;
        }
    }
}
