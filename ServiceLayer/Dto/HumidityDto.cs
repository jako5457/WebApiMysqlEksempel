using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    internal class HumidityDto : ValueBaseDto
    {
        public int GetProc() => Convert.ToInt32(Value); 
    }
}
