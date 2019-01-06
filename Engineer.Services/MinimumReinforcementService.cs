using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineer.Domain;

namespace Engineer.Services
{
    public class MinimumReinforcementService
    {
        private MinimumReinforcement minimumReinforcement;

        public MinimumReinforcementService()
        {
            
        }

        public MinimumReinforcement AddMinimumReinforcement(Concrete concrete, Rectangle section, ReinforcingBar bar, string typeOfStress)
        {            
            return minimumReinforcement = new MinimumReinforcement(concrete, section, bar, typeOfStress);
        }

        public double GetMinimumReinforcement(Concrete concrete)
        {
            return minimumReinforcement.Asmin;
        }

        public double GetEffectiveTensileStrenght()
        {
            return minimumReinforcement.fcteff;
        }

        public double GetSectionArea()
        {
            return minimumReinforcement.Act / 1000000;
        }

        public double GetStressDistributionCoefficient()
        {
            return minimumReinforcement.kc;
        }

        public double GetNonUniformStressCoefficient()
        {
            return minimumReinforcement.k;
        }
    }
}