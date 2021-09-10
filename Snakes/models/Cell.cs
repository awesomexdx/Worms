namespace Snakes.models
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CellContent Content { get; set; }

        public Cell(int x, int y, CellContent content)
        {
            X = x;
            Y = y;
            Content = content;
        }
    }
}
