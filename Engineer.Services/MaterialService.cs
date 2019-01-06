using System.Collections.Generic;
using Engineer.Data;
using Engineer.Domain;

namespace Engineer.Services
{
    public class MaterialService
    {
        public MaterialService()
        {

        }

        public List<Concrete> GetConcreteList()
        {           
            return ConcreteData.Concrete;
        }
    }
}