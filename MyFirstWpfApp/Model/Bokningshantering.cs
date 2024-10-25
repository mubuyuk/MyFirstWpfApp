using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace MyFirstWpfApp.Model
{
    class Bokningshantering
    {
        // Lista som innehåller alla träningspass
        public List<Pass> träningspass = new List<Pass>();

        // Lista som innehåller alla bokningar (knytning mellan användare och pass)
        public List<Användare> bokningar = new List<Användare>();

        // Metod för att lägga till ett nytt pass till listan över träningspass
        public void LäggTillPass(Pass nyttPass)
        {
            träningspass.Add(nyttPass);
        }

        // Metod som returnerar alla pass som finns i listan över träningspass
        public List<Pass> GetAllaPass()
        {
            return träningspass;
        }

        // Metod som kontrollerar om en användare redan har bokat ett specifikt pass
        public bool HarBokatPass(Användare användare, Pass pass)
        {
            // Kontrollera om det finns en bokning där användarens namn och bokade pass matchar
            return bokningar.Any(b => b.Namn.Equals(användare.Namn) && b.BokadPass == pass);
        }

        // Metod för att boka ett pass för en specifik användare
        public bool BokaPass(Användare användare, Pass pass)
        {
            // Kontrollera om passet inte är fullbokat och att användaren inte redan har bokat detta pass
            if (!pass.ÄrFullbokat && !HarBokatPass(användare, pass))
            {
                pass.BokaPlats();
                bokningar.Add(new Användare(användare.Namn, pass)); // Skapa en ny bokning genom att koppla användarens namn och passet
                return true;
            }
            return false; // Bokningen misslyckades (antingen fullbokat eller redan bokat)
        }

        // Metod för att avboka ett pass för en specifik användare
        public void AvbokaPass(Användare användare, Pass pass)
        {
            // Hitta bokningen som motsvarar användaren och passet
            var användareAttTaBort = bokningar.FirstOrDefault(b => b.Namn.Equals(användare.Namn) && b.BokadPass == pass);

            // Kontrollera om bokningen hittades
            if (användareAttTaBort != null)
            {
                pass.AvbokaPlats(); // Minska antalet bokade platser för passet
                bokningar.Remove(användareAttTaBort); // Ta bort bokningen från listan
            }
        }
    }
}
