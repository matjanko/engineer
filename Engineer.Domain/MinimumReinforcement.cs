using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Domain
{
    public class MinimumReinforcement
    {
        public double Asmin { get; private set; }
        public double fcteff { get; private set; }
        public double Act { get; private set; }
        public double k { get; private set; }   
        public double kc { get; private set; }
        public double d { get; private set; }
        public double ds { get; private set; }
        public double a { get; private set; }
        public double sigmaS { get; private set; }

        private Concrete concrete;
        private Rectangle rectangle;
        private ReinforcingBar bar;
        private Crack crack;
        private string stressType;
        private double cover;
       
        private readonly int[] steelStress = new int[]
        {
            160, 200, 240, 280, 320, 360, 400, 450
        };
       
        private readonly int[][] maxBarDiameter = new int[][]
        {
            new int[]{40, 32, 20, 16, 12, 10, 8, 6},
            new int[]{32, 25, 16, 12, 10, 8, 6, 5},
            new int[]{25, 16, 12, 8, 6, 5, 4, 0}
        };

        public MinimumReinforcement(Concrete concrete, Rectangle rectangle, ReinforcingBar bar, string stressType, double cover, Crack crack)
        {
            this.concrete = concrete;
            this.rectangle = rectangle;
            this.bar = bar;
            this.stressType = stressType;
            this.cover = cover;
            this.crack = crack;
            fcteff = EffectiveTensileStrengthValue();
            Act = ConcreteAreaWithinTensileZone();
            kc = StressDistributionCoefficient();
            k = NonUniformStressCoefficient();
            d = bar.Diameter;
            ds = ModifiedMaximumBarDiameter();
        }       

        private double EffectiveTensileStrengthValue()
        {
            return concrete.fctm;
        }

        private double ConcreteAreaWithinTensileZone()
        {
            if (stressType == "zginanie")
            {
                return rectangle.Area * 0.5;
            }
            else
            {
                return rectangle.Area;
            }
        }

        private double StressDistributionCoefficient()
        {
            if (stressType == "zginanie")
            {
                return 0.4;
            }
            else
            {
                return 1.0;
            }
        }
        
        private double NonUniformStressCoefficient()
        {
            if (rectangle.Height <= 300)
            {
                return 1.0;
            }
            else if (rectangle.Height >= 800)
            {
                return 0.65;
            }
            else
            {
                return Interpolations.LinearInterpolate(300, 1, 800, 0.65, rectangle.Height);
            }
        }

        private double ModifiedMaximumBarDiameter()
        {
            double a = cover + bar.Diameter / 2;
            double h = rectangle.Height;
            double omega;

            if (stressType == "zginanie")
            {
                if (a < 0.1 * h)
                {
                    omega = (k * fcteff * kc * 0.5 * h) / (2.9 * 2 * a);
                }
                else
                {
                    omega = (k * fcteff * 5 * kc * 0.5 * h) / (2.9 * h);
                }
            }
            else
            {
                if (a < 0.2 * h)
                {
                    omega = (k * fcteff * h) / (8 * 2.9 * a);
                }
                else
                {
                    omega = (5 * k * fcteff) / (8 * 2.9);
                }
            }

            return Math.Round(bar.Diameter / omega, 0);
        }
    }
}