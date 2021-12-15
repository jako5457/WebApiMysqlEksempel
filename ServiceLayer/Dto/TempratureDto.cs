using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class TempratureDto : ValueBaseDto
    {
        public double GetF() => (Value * 1.8) + 32;

        public double GetC() => Value;

        public double GetK() => Value + 273.15;
    }
}
