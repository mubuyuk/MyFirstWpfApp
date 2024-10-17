using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWpfApp
{
    class Bokningshantering
    {
        public List<Pass> träningspass = new List<Pass>();

        public void LäggTillPass(Pass nyttPass)
        {
            träningspass.Add(nyttPass);
        }

        public List<Pass> GetAllaPass()
        {
            return träningspass;
        }


    }


}
