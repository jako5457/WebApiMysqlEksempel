using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Data
{
    public interface IAmqpListenerService
    {

        public void Start();

        public void SetConnection(string host,string user,string pass);

    }
}
