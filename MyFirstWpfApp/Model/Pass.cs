using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstWpfApp;

namespace MyFirstWpfApp.Model;

public class Pass
{
    public string Namn { get; set; }
    public string Kategori { get; set; }
    public DateTime Tid { get; set; }
    public int AntalPlatser { get; set; }
    public int BokadePlatser { get; set; }

    // Returnerar true om passet är fullbokat annars false.
    public bool ÄrFullbokat => BokadePlatser >= AntalPlatser;

    // Konstruktor som initierar ett pass med ett namn, kategori, tid och antal platser
    public Pass(string namn, string kategori, DateTime tid, int antalPlatser)
    {
        Namn = namn;
        Kategori = kategori;
        Tid = tid;
        AntalPlatser = antalPlatser;
        BokadePlatser = 0;
    }

    // Konstruktor som initierar ett pass med ett namn, kategori, tid, antal platser och bokade platser
    // För att kunna simulera ett pass som är fullbokat
    public Pass(string namn, string kategori, DateTime tid, int antalPlatser, int bokadePlatser)
    {
        Namn = namn;
        Kategori = kategori;
        Tid = tid;
        AntalPlatser = antalPlatser;
        BokadePlatser = bokadePlatser;
    }

    // Metod för att boka en plats på passet
    public void BokaPlats()
    {
        if (!ÄrFullbokat)
        {
            BokadePlatser++;
        }
    }

    // Metod för att avboka en plats på passet
    public void AvbokaPlats()
    {
        if (BokadePlatser > 0)
        {
            BokadePlatser--;
        }

    }

    //returnerar den formaterade tiden i formatet "HH:mm" & datumet i formatet "dd/MM"
    public string FormateradTid => Tid.ToString("HH:mm");
    public string FormateratDatum => Tid.ToString("dd/MM");
}
