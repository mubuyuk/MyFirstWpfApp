using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyFirstWpfApp.Model
{
    class Bokningshantering
    {
        public List<Pass> träningspass = new List<Pass>();
        public List<Användare> bokningar = new List<Användare>();

        public void LäggTillPass(Pass nyttPass)
        {
            träningspass.Add(nyttPass);
        }

        public List<Pass> GetAllaPass()
        {
            return träningspass;
        }

        public bool HarBokatPass(Användare användare, Pass pass)
        {
            return bokningar.Any(b => b.Namn.Equals(användare.Namn) && b.BokadPass == pass);
        }

        public bool BokaPass(Användare användare, Pass pass)
        {
            if (!pass.ÄrFullbokat && !HarBokatPass(användare, pass))
            {
                pass.BokaPlats();
                bokningar.Add(new Användare(användare.Namn, pass));
                return true;
            }
            return false;
        }

        public void AvbokaPass(Användare användare, Pass pass)
        {
            var användareAttTaBort = bokningar.FirstOrDefault(b => b.Namn.Equals(användare.Namn) && b.BokadPass == pass);
            if (användareAttTaBort != null)
            {
                pass.AvbokaPlats();
                bokningar.Remove(användareAttTaBort);
            }
        }
    }
}
