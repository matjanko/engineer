namespace Engineer.Domain
{
    public class Rectangle : ISection
    {
        public double Area { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
            Area = CalculateArea();
        }

        private double CalculateArea()
        {
            return Width * Height;
        }
    }
}