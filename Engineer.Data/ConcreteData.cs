using System;
using System.Collections;
using System.Collections.Generic;
using Engineer.Domain;

namespace Engineer.Data
{
    public class ConcreteData
    {
        public static List<Concrete> Concrete { get; } = new List<Concrete>
        {
            new Concrete(12, 15),
            new Concrete(16, 20),
            new Concrete(20, 25),
            new Concrete(25, 30),
            new Concrete(30, 37),
            new Concrete(35, 45),
            new Concrete(40, 50),
            new Concrete(45, 55),
            new Concrete(50, 60),
            new Concrete(55, 67),
            new Concrete(60, 75),
            new Concrete(70, 85),
            new Concrete(80, 95),
            new Concrete(90, 105)
        };
    }
}