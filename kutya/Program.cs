using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace kutya
{
    class Program
    {
        static List<KutyaNev> kutyaNevek = new List<KutyaNev>();
        static List<KutyaFajta> kutyafajta = new List<KutyaFajta>();
        static List<Kutya> kutya = new List<Kutya>();

        static void KutyaNevekBeolvas()
        {
            StreamReader be = new StreamReader("KutyaNevek.csv");

            be.ReadLine();

            while (!be.EndOfStream)
            {
                string[] adat = be.ReadLine().Split(';');

                kutyaNevek.Add(new KutyaNev(
                  Convert.ToInt32(adat[0]),
                  adat[1]
                ));
            }

            be.Close();

            
        }



        static void KutyaFajtaBeolv()
        {
            StreamReader be = new StreamReader("KutyaFajtak.csv");

            be.ReadLine();

            while (!be.EndOfStream)
            {
                string[] adat = be.ReadLine().Split(';');
                kutyafajta.Add(new KutyaFajta(int.Parse(adat[0]), adat[1], adat[2]));
                
            }

            be.Close();
        }

        static void kutyaBeolv()
        {
            StreamReader be = new StreamReader("Kutyak.csv");

            be.ReadLine();

            while (!be.EndOfStream)
            {
                string[] adat = be.ReadLine().Split(';');
                kutya.Add(new Kutya(int.Parse(adat[0]), int.Parse(adat[1]), int.Parse(adat[2]),
                    int.Parse(adat[3]), adat[4]));

            }

            be.Close();
        }

        static void harmas()
        {
            Console.WriteLine($"3. feladat: Kutyanevek száma: {kutyaNevek.Count()}");
        }
        static void hatodik()
        {
            double osszeg = 0;
            foreach (var k in kutya)
            {
                osszeg += k.Eletkor;
            }
            
            Console.WriteLine($"6. feladat: Kutyák átlagos életkora: {osszeg/kutya.Count:N2}");

        }

        static void hetedik()
        {
            int max = kutya[0].Eletkor;
            int fajta = 0;
            int neve = 0;

            for (int i = 0; i < kutya.Count; i++)
            {
                if (max < kutya[i].Eletkor)
                {
                    max = kutya[i].Eletkor;
                    fajta = kutya[i].Fajta_id;
                    neve = kutya[i].Nev_id;
                }
            }

            
            int j = 0;
            string fajtaja = "";
            bool megvan = false;
            while (j<kutyafajta.Count && !megvan)
            {
                if (kutyafajta[j].ID == fajta)
                {
                    fajtaja =kutyafajta[j].Nev;
                    megvan = true;
                }
                j++;
            }


            j = 0;

            string rendesneve = "";
            megvan = false;
            while (j < kutyaNevek.Count && !megvan)
            {
                if (kutyaNevek[j].Id == neve)
                {
                    rendesneve = kutyaNevek[j].Nev;
                    megvan = true;
                }
                j++;
            }

            Console.WriteLine($"7. feladat: Legidősebb kutya neve és fajtája: {rendesneve}, {fajtaja}");

        }


        static void nyolcadik()
        {

            Console.WriteLine("8. feladat: Január 10. -én vizsgált kutya fajták:");
            
            Dictionary<string, int> nyolcas = new Dictionary<string, int>();
            
            for (int i = 0; i < kutya.Count; i++)
            {
                if (kutya[i].Vizsgalat == "2018.01.10")
                {
                    for (int j = 0; j < kutyafajta.Count; j++)
                    {
                        if (kutyafajta[j].ID == kutya[i].Fajta_id && !nyolcas.ContainsKey(kutyafajta[i].Nev))
                        {
                            nyolcas.Add(kutyafajta[j].Nev,1);
                        }
                        if (nyolcas.ContainsKey(kutyafajta[i].Nev))
                        {
                            nyolcas[kutyafajta[j].Nev]++;
                        }
                    }

                }
            }



            foreach (var item in nyolcas)
            {
                Console.WriteLine($"\t{item.Key} {item.Value}");
            }

            
            

            


        }


        static void kilencedik()
        {
            Dictionary<string, int> kilences = new Dictionary<string, int>();
            foreach (var item in kutya)
            {
                if (!kilences.ContainsKey(item.Vizsgalat))
                {
                    kilences.Add(item.Vizsgalat,0);
                }
            }

            foreach (var item in kutya)
            {
                kilences[item.Vizsgalat]++;

            }
            int max = kilences.Values.First();
            string max2 = kilences.Keys.First();
            foreach (var item in kilences)
            {
                if (max < item.Value)
                {
                    max = item.Value;
                    max2 = item.Key;
                }
            }
            Console.WriteLine($"9. feladat: Legjobban leterhelt nap: {max2}: {max} kutya");
        }


        static void tizedik()
        {
            Dictionary<string, int> tizes = new Dictionary<string, int>();
            foreach (var item in kutyaNevek)
            {
                if (!tizes.ContainsKey(item.Id.ToString()))
                {
                    tizes.Add(item.Id.ToString(), 0);
                }
            }
            foreach (var item in kutya)
            {
                tizes[item.Nev_id.ToString()]++;
                
            }
            List<Dictionary<string,int>> tizeslista = new List<Dictionary<string,int>>();
            tizeslista.Add(tizes);

            //Feladom



            foreach (var item in tizeslista)
            {
                Console.WriteLine(tizeslista);
            }

            var rendezes = from entry in tizes orderby entry.Value descending select entry;


            StreamWriter sw = new StreamWriter("névstatisztika.txt");


            sw.Close();
        }
        static void Main(string[] args)
        {

            KutyaNevekBeolvas();
            harmas();
            KutyaFajtaBeolv();
            kutyaBeolv();
            hatodik();
            hetedik();
            nyolcadik();
            kilencedik();
            tizedik();



           

            Console.ReadKey();

        }
    }
}
