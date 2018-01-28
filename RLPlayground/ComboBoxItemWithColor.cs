using System.Drawing;

namespace RLPlayground
{
    public class ComboBoxItemWithColor
    {
        public string Text { get; }
        public States State { get; }
        public Color Color { get; }

        public ComboBoxItemWithColor(string text, States state, Color color)
        {
            Text = text;
            State = state;
            Color = color;
        }
    }
}
