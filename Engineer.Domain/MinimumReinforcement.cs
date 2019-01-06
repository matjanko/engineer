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

        private Concrete concrete;
        private Rectangle rectangle;
        private ReinforcingBar bar;
        private string stressType;

        public MinimumReinforcement(Concrete concrete, Rectangle rectangle, ReinforcingBar bar, string stressType)
        {
            this.concrete = concrete;
            this.rectangle = rectangle;
            this.bar = bar;
            this.stressType = stressType;
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
            if (stressType == "zginanie")
            {

            }
            else
            {

            }
            return 0;
        }
    }
}