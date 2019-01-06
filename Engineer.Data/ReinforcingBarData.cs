using System;
using System.Collections.Generic;
using Engineer.Domain;

namespace Engineer.Data
{
    public class ReinforcingBarData
    {
        public static List<ReinforcingBar> ReinforcingBar { get; } = new List<ReinforcingBar>
        {
            new ReinforcingBar(6),
            new ReinforcingBar(8),
            new ReinforcingBar(10),
            new ReinforcingBar(12),
            new ReinforcingBar(14),
            new ReinforcingBar(16),
            new ReinforcingBar(18),
            new ReinforcingBar(20),
            new ReinforcingBar(22),
            new ReinforcingBar(25),
            new ReinforcingBar(28),
            new ReinforcingBar(32),
        };
    }
}