namespace Engineer.Domain
{
    public class Crack
    {
        public double Width { get; private set; }

        public Crack(double width)
        {
            Width = width;
        }
    }
}