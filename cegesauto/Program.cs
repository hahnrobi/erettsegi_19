using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace cegesauto
{
    class Statusz
    {
        public int Nap;
        public string OraPerc;
        public string Rendszam;
        public int Szemely;
        public int Km;
        public bool Behajtas;

        public override string ToString()
        {
            return string.Format("{0} - {1} \t {2} ({3})\t{4} {5}", Nap, OraPerc, Rendszam, Szemely, Km, (Behajtas ? "be" : "ki"));

        }
    }
    class Program
    {
        static int feladatNum = 0;
        static void F()
        {
            Console.WriteLine(++feladatNum + ". feladat");
        }
        static void Main(string[] args)
        {
            #region 1. feladat
            F();
            List<Statusz> adat = new List<Statusz>();
            using (StreamReader sr = new StreamReader("autok.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split();
                    adat.Add(new Statusz { Nap = int.Parse(sor[0]), OraPerc = sor[1], Rendszam = sor[2], Szemely = int.Parse(sor[3]), Km = int.Parse(sor[4]), Behajtas = (sor[5] == "1" ? true : false) });
                }
            }
            #endregion
            #region 2. feladat
            F();
            Statusz utolso = null;
            foreach (Statusz item in adat)
            {
                if (item.Behajtas == false)
                {
                    utolso = item;
                }
            }
            Console.WriteLine("{0}. nap rendszám: {1}", utolso.Nap, utolso.Rendszam);
            #endregion
            #region 3. feladat
            F();
            Console.Write("Nap: ");
            int beNap = int.Parse(Console.ReadLine());
            Console.WriteLine("Forgalom a(z) {0}. napon:", beNap);
            List<Statusz> beKi = new List<Statusz>();
            foreach (Statusz statusz in adat)
            {
                if (statusz.Nap == beNap)
                {
                    Console.WriteLine("{0} {1} {2} {3}", statusz.OraPerc, statusz.Rendszam, statusz.Szemely, statusz.Behajtas ? "be" : "ki");
                }
            }
            #endregion
            #region 4. feladat
            F();
            //MEGOLDHATÓ ÍGY:
            Dictionary<string, bool> bentVanE = new Dictionary<string, bool>();
            foreach (Statusz statusz in adat)
            {
                bentVanE[statusz.Rendszam] = statusz.Behajtas;
            }
            int kintVan = 0;
            foreach (KeyValuePair<string, bool> item in bentVanE)
            {
                if (!item.Value)
                {
                    kintVan++;
                }
            }
            Console.WriteLine("A hónap végén {0} autót nem hoztak vissza.", kintVan);

            //VAGY ÍGY:
            List<string> rendszamok = new List<string>();
            foreach (Statusz statusz in adat)
            {
                if (!rendszamok.Contains(statusz.Rendszam))
                {
                    rendszamok.Add(statusz.Rendszam);
                }
            }
            bool[] allapotok = new bool[rendszamok.Count];
            foreach (Statusz statusz in adat)
            {
                allapotok[rendszamok.IndexOf(statusz.Rendszam)] = statusz.Behajtas;
            }
            kintVan = 0;
            for (int i = 0; i < allapotok.Length; i++)
            {
                if (!allapotok[i]) kintVan++;
            }
            Console.WriteLine("A hónap végén {0} autót nem hoztak vissza.", kintVan);
            #endregion
            #region 5. feladat
            F();
            Dictionary<string, int> StartTav = new Dictionary<string, int>();
            Dictionary<string, int> VegeTav = new Dictionary<string, int>();
            foreach (Statusz statusz in adat)
            {   
                if (!StartTav.ContainsKey(statusz.Rendszam))
                {
                    StartTav[statusz.Rendszam] = statusz.Km;
                }
                VegeTav[statusz.Rendszam] = statusz.Km;
            }

            Dictionary<string, int> Tavok = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> item in StartTav)
            {
                Tavok[item.Key] = VegeTav[item.Key] - StartTav[item.Key];
            }
            foreach (KeyValuePair<string, int> item in Tavok)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            #endregion
            #region 6. feladat
            F();
            Dictionary<int, int> megtettUt = new Dictionary<int, int>();
            Dictionary<int, int> megtettTemp = new Dictionary<int, int>();
            foreach (Statusz statusz in adat)
            {
                if(!statusz.Behajtas)
                {
                    megtettTemp[statusz.Szemely] = statusz.Km;
                }
                else
                {
                    int ut = statusz.Km - megtettTemp[statusz.Szemely];
                    if (megtettUt.ContainsKey(statusz.Szemely))
                    {
                        if (ut > megtettUt[statusz.Szemely])
                        {
                            megtettUt[statusz.Szemely] = ut;
                        }
                    }
                    else
                    {
                        megtettUt[statusz.Szemely] = ut;
                    }
                }
            }
            KeyValuePair<int, int> maxUt = new KeyValuePair<int, int>();
            foreach (KeyValuePair<int, int> item in megtettUt)
            {
                if(item.Value > maxUt.Value)
                {
                    maxUt = item;
                }
            }
            Console.WriteLine("Leghosszabb út: {0} km, személy: {1}", maxUt.Value, maxUt.Key);
            #endregion
            #region 7. feladat
            F();
            Console.Write("Rendszám: ");
            string beRendszam = Console.ReadLine();
            using (StreamWriter sw = new StreamWriter(beRendszam + "_menetlevel.txt"))
            {
                foreach (Statusz statusz in adat)
                {
                    if (statusz.Rendszam == beRendszam)
                    {
                        if (statusz.Behajtas)
                        {
                            sw.Write("\t" + statusz.Nap + ". " + statusz.OraPerc + "\t" + statusz.Km + " km\n\r");
                        }
                        else
                        {
                            sw.Write(statusz.Szemely + "\t" + statusz.Nap + ". " + statusz.OraPerc + "\t" + statusz.Km + " km");
                        }
                    }

                }
                Console.WriteLine("Menetlevél kész.");
            }
            #endregion  
            Console.ReadLine();
        }
    }
}
