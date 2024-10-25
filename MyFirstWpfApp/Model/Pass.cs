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
    public string Tid { get; set; }
    public int AntalPlatser { get; set; }
    public int BokadePlatser { get; set; }

    public bool ÄrFullbokat => BokadePlatser >= AntalPlatser;

    public Pass(string namn, string kategori, string tid, int antalPlatser)
    {
        Namn = namn;
        Kategori = kategori;
        Tid = tid;
        AntalPlatser = antalPlatser;
        BokadePlatser = 0;
    }

    public Pass(string namn, string kategori, string tid, int antalPlatser, int bokadePlatser)
    {
        Namn = namn;
        Kategori = kategori;
        Tid = tid;
        AntalPlatser = antalPlatser;
        BokadePlatser = bokadePlatser;
    }

    public void BokaPlats()
    {
        if (!ÄrFullbokat)
        {
            BokadePlatser++;
        }
    }

    public void AvbokaPlats()
    {
        if (BokadePlatser > 0)
        {
            BokadePlatser--;
        }

    }
}
