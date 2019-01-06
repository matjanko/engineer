using System;

namespace Engineer.Domain
{
    public class Concrete: IMaterial
    {                                          
        public string Name { get; private set; }                //klasa betonu                                 
        public int fck { get; private set; }                    //charakterystyczna wytrzymalosc walcowa na sciskanie         
        public int fckcube { get; private set; }                //charakterystyczna wytrzymalosc kostkowa na sciskanie
        public double fcd { get; private set; }                 //obliczeniowa wytrzymalosc na sciskanie
        public int fcm { get; private set; }                    //srednia wytrzymalosc na sciskanie
        public double fctm { get; private set; }                //srednia wytrzymalosc na rozciaganie
        public double fctk { get; private set; }                //charakterystyczna wytrzymalosc na rozciaganie
        public double fctk005 { get; private set; }             //charakterystyczna wytrzymalosc na rozciaganie 5%
        public double fctk095 { get; private set; }             //charakterystyczna wytrzymalosc na rozciaganie 95%
        public double fctd { get; private set; }                //obliczeniowa wytrzymalosc na rozciaganie
        public double Ecm { get; private set; }                 //modul sprezystosci betonu
        public double yf { get; private set; }                  //wspolczynnik bezpieczenstwa dla wartosci obliczeniowych

        public Concrete(int fck, int fckcube)
        {           
            this.fck = fck;
            this.fckcube = fckcube;
            Name = ConcreteClassName();
            fcm = MeanCompressiveStrength();
            fctm = MeanTensileStrength();
            fctk005 = FivePercentFractileTensileStrength();
            fctk095 = NinetyFivePercentFractileTensileStrength();
            yf = PartialSafetyFactor();
            fcd = DesignCompressiveStrength();
            fctd = DesignTensileStrength();
            Ecm = ModulusOfElasticity();
        }

        private string ConcreteClassName()
        {
            return "C" + fck.ToString() + "/" + fckcube.ToString();
        }

        private int MeanCompressiveStrength()
        {
            return fck + 8;
        }

        private double MeanTensileStrength()
        {
            if(fck <= 50)
            {
                double a = 2;
                double b = 3;
                double power = a/b;
                return Math.Round(0.3 * Math.Pow(fck, power), 1);
            }
            else
            {
                return Math.Round(2.12 * Math.Log(1+(fcm/10)), 1);
            }           
        }

        private double FivePercentFractileTensileStrength()
        {
            return Math.Round(0.7 * fctm, 1);
        }

        private double NinetyFivePercentFractileTensileStrength()
        {
            return Math.Round(1.3 * fctm, 1);
        }

        private double DesignCompressiveStrength()
        {
            return Math.Round(fck / yf, 2);
        }

        private double DesignTensileStrength()
        {
            return Math.Round(fctk005 / yf, 2);
        }

        private double ModulusOfElasticity()
        {
            return Math.Round(22 * Math.Pow(fcm / 10, 0.3), 0);
        }

        private double PartialSafetyFactor()
        {
            return 1.4;
        }
    }
}