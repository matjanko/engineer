using System;
using System.Collections.Generic;
using Engineer.Domain;
using Engineer.Data;

namespace Engineer.Services
{
    public class CrackService
    {
        public CrackService()
        {
            
        }

        public List<Crack> GetCrack()
        {
            return CrackData.Crack;
        }
    }
}