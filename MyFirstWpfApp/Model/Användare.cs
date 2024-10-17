using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyFirstWpfApp
{
    class Användare
    {
        public string Namn {  get; set; }

        public Användare(string namn)
        {
            Namn = namn;
        }

        // Metod för att boka ett pass
        public void Boka()
        {
            MessageBox.Show($"{Namn}, ditt pass är bokat!");
        }

        public void AvBoka()
        {
            MessageBox.Show($"{Namn}, ditt pass är Avbokat!");
        }
    }
}
