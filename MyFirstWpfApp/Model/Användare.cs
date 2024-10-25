using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyFirstWpfApp.Model
{
    public class Användare
    {
        public string Namn { get; set; }
        public Pass BokadPass { get; set; }

        public Användare(string namn, Pass bokadPass)
        {
            Namn = namn;
            BokadPass = bokadPass;
        }

        public Användare(string namn)
        {
            Namn = namn;
        }
    }
}
