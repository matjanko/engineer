using System;
using Engineer.Utilities;

namespace Engineer.Domain
{
    public class ReinforcingBar : ViewModelBase, IBar
    {
        public int Diameter { get; private set; }
        public double Area { get; private set; }        
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged(() => IsSelected);               
            }
        }
        private bool isSelected = false;

        public ReinforcingBar(int diameter)
        {
            Diameter = diameter;
            Area = CalculateArea();
        }

        private double CalculateArea()
        {
            double radius = Diameter / 2;
            return Math.PI * Math.Pow(radius, 2);
        }       
    }
}