namespace Engineer.Domain
{
    public class Interpolations
    {
        public static double LinearInterpolate(double x1, double y1, double x2, double y2, double x)
        {
            double a, b;

            a = (y2 - y1) / (x2 - x1);
            b = ((-x1) * (y2 - y1) - (x2 - x1) * (-y1)) / (x2 - x1);

            return a * x + b;
        }
    }
}
