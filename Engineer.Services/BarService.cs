using System.Collections.Generic;
using Engineer.Data;
using Engineer.Domain;

namespace Engineer.Services
{
    public class BarService
    {
        public BarService()
        {

        }

        public List<ReinforcingBar> GetReinforcingBarList()
        {
            return ReinforcingBarData.ReinforcingBar;
        }
    }
}