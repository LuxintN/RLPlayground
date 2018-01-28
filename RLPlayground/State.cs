namespace RLPlayground
{
    public class State
    {
        public int Row { get; }
        public int Column { get; }
        
        public State(int row, int column)
        {
            Column = column;
            Row = row;
        }
    }
}
