using hra_magic_land;
using System;

namespace hra_magic_land
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== RPG GAME ===");
            Postava hero = VytvoreniPostavy();
            Console.WriteLine("\n=== TVOJE POSTAVA ===");
            Console.WriteLine("Jmeno: " + hero.Jmeno);
            Console.WriteLine("Varianta: " + hero.Varianta);
            Console.WriteLine("HP: " + hero.Zivoty + "/" + hero.MaxZivoty);
            Console.WriteLine(
                hero.NazevEnergie + ": " +
                hero.Energie + "/" + hero.MaxEnergie
            );
            Entita nepritel = new Entita(
                "Dark Entity",
                120,
                7,
                12
            );
            Console.WriteLine("\nProti tobe stoji: " + nepritel.Jmeno);
            Souboj(hero, nepritel);
        }
        static Postava VytvoreniPostavy()
        {
            Console.Write("Napis jmeno sve postavy: ");
            string jmeno = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(jmeno))
            {
                jmeno = "Hero";
            }
            Console.WriteLine("\nVyber si povolani:");
            Console.WriteLine("1 - Kouzelnik");
            Console.WriteLine("2 - Bojovnik");
            int vyber;
            Console.Write("Tvuj vyber: ");
            while (!int.TryParse(Console.ReadLine(), out vyber) || vyber < 1 || vyber > 2)
            {
                Console.Write("Zadej 1 nebo 2: ");
            }
            if (vyber == 1)
            {
                Kouzelnik mag = new Kouzelnik();
                Postava hero = mag.VytvorKouzelnik(jmeno);
                Console.WriteLine("\nByl ti nahodne prirazen: " + hero.Varianta);
                return hero;
            }
            else
            {
                Bojovnik bojovnik = new Bojovnik();
                Postava hero = bojovnik.VytvorBojovnika(jmeno);
                Console.WriteLine("\nByl ti nahodne prirazen: " + hero.Varianta);
                return hero;
            }
        }
        static int KritickyUtok(int damage)
        {
            if (Nahoda.Rnd.Next(1, 101) <= 10)
            {
                Console.WriteLine("KRITICKY ZASAH!");

                damage *= 2;
            }
            return damage;
        }
        static void Souboj(Postava hrac, Entita entita)
        {
            bool utekl = false;
            while (hrac.Zivoty > 0 && entita.Zivoty > 0)
            {
                Console.WriteLine("\n========== SOUBOJ ==========");
                Console.WriteLine(
                    hrac.Jmeno + ": " +
                    hrac.Zivoty + "/" + hrac.MaxZivoty + " HP"
                );
                Console.WriteLine(
                    hrac.NazevEnergie + ": " +
                    hrac.Energie + "/" + hrac.MaxEnergie
                );
                Console.WriteLine(
                    entita.Jmeno + ": " +
                    entita.Zivoty + "/" + entita.MaxZivoty + " HP"
                );
                Console.WriteLine("\n1 - Zakladni utok");
                Console.WriteLine("2 - Specialni utok");
                Console.WriteLine("3 - Lecivy lektvar");
                Console.WriteLine("0 - Utect");
                int volba;
                Console.Write("\nCo udelas? ");
                while (!int.TryParse(Console.ReadLine(), out volba) || volba < 0 || volba > 3)
                {
                    Console.Write("Zadej cislo od 0 do 3: ");
                }
                if (volba == 0)
                {
                    utekl = true;
                    break;
                }
                int damage = 0;
                if (volba == 1)
                {
                    damage = hrac.ZakladniUtok();
                    damage = KritickyUtok(damage);
                }
                else if (volba == 2)
                {
                    damage = hrac.SpecialniUtok();
                    if (damage == -1)
                    {
                        Console.WriteLine("Nemas dostatek energie!");
                        continue;
                    }
                    damage = KritickyUtok(damage);
                }
                else if (volba == 3)
                {
                    int uzdraveni = hrac.PouzijLektvar();
                    if (uzdraveni == 0)
                    {
                        Console.WriteLine(
                            "Nemuzes pouzit dalsi lektvar nebo mas plne HP!"
                        );
                        continue;
                    }
                    Console.WriteLine("Uzdravil ses o " + uzdraveni + " HP!");
                }
                if (volba == 1 || volba == 2)
                {
                    entita.PrijmiPoskozeni(damage);
                    Console.WriteLine("Zpusobil jsi " + damage + " damage!");
                }
                if (entita.Zivoty <= 0)
                {
                    break;
                }
                int damageEntity = entita.Utok();
                Console.WriteLine(
                    "\n" + entita.Jmeno +
                    " utoci za " + damageEntity + " damage!"
                );
                hrac.PrijmiPoskozeni(damageEntity);
                if (hrac.Zivoty > 0)
                {
                    hrac.RegenerujEnergii();
                }
            }
            Console.WriteLine("\n========== KONEC SOUBOJE ==========");
            if (utekl)
            {
                Console.WriteLine("Utekl jsi ze souboje!");
            }
            else if (hrac.Zivoty <= 0)
            {
                Console.WriteLine("Prohral jsi!");
            }
            else
            {
                Console.WriteLine("Vyhral jsi!");
            }
        }
    }
}