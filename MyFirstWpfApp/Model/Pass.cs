using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWpfApp
{
    public class Pass
    {
        public string Namn { get; set; }
        public string Kategori { get; set; }
        public string Tid { get; set; }
        public int AntalPLatser { get; set; }
        public int BokadePlatser { get; set; }

        public Pass(string namn, string katergori, string tid, int antalplatser)
        {
            Namn = namn;
            Kategori = katergori;
            Tid = tid;
            AntalPLatser = antalplatser;
            BokadePlatser = 0;
        }
    }
}
