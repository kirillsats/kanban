using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Pakk
{
    public string ID { get; set; }
    public string Aadress { get; set; }
    public string Staatus { get; set; } // "vastu võetud", "teel", "kohale toimetatud"
}

class Programm
{
    static List<Pakk> pakkideNimekiri = new List<Pakk>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nPakkide jälgimise süsteem");
            Console.WriteLine("1. Lisa pakk");
            Console.WriteLine("2. Muuda paki staatust");
            Console.WriteLine("3. Näita pakke staatuse järgi");
            Console.WriteLine("4. Otsi pakke aadressi järgi");
            Console.WriteLine("5. Kuvada statistika");
            Console.WriteLine("6. Kustuta pakk");
            Console.WriteLine("7. Ekspordi andmed");
            Console.WriteLine("8. Välju");
            Console.Write("Valik: ");
            var valik = Console.ReadLine();

            switch (valik)
            {
                case "1": LisaPakk(); break;
                case "2": MuudaStaatus(); break;
                case "3": NäitaPakkeStaatuseJärgi(); break;
                case "4": OtsiAadressiJärgi(); break;
                case "5": KuvadaStatistika(); break;
                case "6": KustutaPakk(); break;
                case "7": EkspordiAndmed(); break;
                case "8": return;
                default: Console.WriteLine("Vale valik."); break;
            }
        }
    }

    static void LisaPakk()
    {
        Console.Write("Sisesta paki ID: ");
        string id = Console.ReadLine();
        if (pakkideNimekiri.Any(p => p.ID == id))
        {
            Console.WriteLine("Viga: See ID on juba olemas.");
            return;
        }

        Console.Write("Sisesta aadress: ");
        string aadress = Console.ReadLine();

        pakkideNimekiri.Add(new Pakk
        {
            ID = id,
            Aadress = aadress,
            Staatus = "vastu võetud"
        });

        Console.WriteLine("Pakk lisatud.");
    }

    static void MuudaStaatus()
    {
        Console.Write("Sisesta paki ID: ");
        string id = Console.ReadLine();
        var pakk = pakkideNimekiri.FirstOrDefault(p => p.ID == id);
        if (pakk == null)
        {
            Console.WriteLine("Pakk ei leitud.");
            return;
        }

        Console.Write("Sisesta uus staatus (vastu võetud, teel, kohale toimetatud): ");
        string uusStaatus = Console.ReadLine().ToLower();
        if (uusStaatus != "vastu võetud" && uusStaatus != "teel" && uusStaatus != "kohale toimetatud")
        {
            Console.WriteLine("Vigane staatus.");
            return;
        }

        pakk.Staatus = uusStaatus;
        Console.WriteLine("Staatus muudetud.");
    }

    static void NäitaPakkeStaatuseJärgi()
    {
        Console.Write("Sisesta staatus: ");
        string staatus = Console.ReadLine().ToLower();
        var tulemused = pakkideNimekiri.Where(p => p.Staatus == staatus).ToList();

        if (tulemused.Count == 0)
        {
            Console.WriteLine("Pakke ei leitud selle staatusega.");
            return;
        }

        foreach (var p in tulemused)
            Console.WriteLine($"{p.ID} | {p.Aadress} | {p.Staatus}");
    }

    static void OtsiAadressiJärgi()
    {
        Console.Write("Sisesta aadress või selle osa: ");
        string otsing = Console.ReadLine().ToLower();
        var tulemused = pakkideNimekiri.Where(p => p.Aadress.ToLower().Contains(otsing)).ToList();

        if (tulemused.Count == 0)
        {
            Console.WriteLine("Pakke ei leitud.");
            return;
        }

        foreach (var p in tulemused)
            Console.WriteLine($"{p.ID} | {p.Aadress} | {p.Staatus}");
    }

    static void KuvadaStatistika()
    {
        var grupp = pakkideNimekiri.GroupBy(p => p.Staatus);
        foreach (var g in grupp)
        {
            Console.WriteLine($"{g.Key}: {g.Count()} tk");
        }
    }

    static void KustutaPakk()
    {
        Console.Write("Sisesta paki ID: ");
        string id = Console.ReadLine();
        var pakk = pakkideNimekiri.FirstOrDefault(p => p.ID == id);
        if (pakk != null)
        {
            pakkideNimekiri.Remove(pakk);
            Console.WriteLine("Pakk kustutatud.");
        }
        else
        {
            Console.WriteLine("Pakk ei leitud.");
        }
    }

    static void EkspordiAndmed()
    {
        string fail = "pakkide_andmed.txt";
        using (StreamWriter sw = new StreamWriter(fail))
        {
            foreach (var p in pakkideNimekiri)
            {
                sw.WriteLine($"{p.ID};{p.Aadress};{p.Staatus}");
            }
        }
        Console.WriteLine($"Andmed eksporditi faili: {fail}");
    }
}
