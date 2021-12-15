using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Entities
{
    public class Pressure
    {
        public int PressureId { get; set; }

        public DateTime Time { get; set; }

        public double Value { get; set; }

    }
}
