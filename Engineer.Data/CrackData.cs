using System;
using System.Collections.Generic;
using Engineer.Domain;

namespace Engineer.Data
{
    public class CrackData
    {
        public static List<Crack> Crack { get; } = new List<Crack>
        {
            new Crack(0.1),
            new Crack(0.2),
            new Crack(0.3),
            new Crack(0.4)
        };
    }
}