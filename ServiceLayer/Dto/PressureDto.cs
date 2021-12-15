using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class PressureDto : ValueBaseDto
    {

        public double GetPa() => Value;

        public double GetHpa() => Math.Pow(Value, -2);

        public double GetBar() => Value / 100000;

    }
}
