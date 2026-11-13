using System;

namespace hra_magic_land
{
    internal class Postava
    {
        public string Jmeno;
        public string Varianta;
        public int Zivoty;
        public int MaxZivoty;
        public int DamageMin;
        public int DamageMax;
        public int Energie;
        public int MaxEnergie;
        public string NazevEnergie;
        public bool JeKouzelnik;
        public int SanceNaObranu;
        private bool LektvarPouzit = false;

        public Postava(
            string jmeno,
            string varianta,
            int zivoty,
            int damageMin,
            int damageMax,
            int energie,
            string nazevEnergie,
            bool jeKouzelnik,
            int sanceNaObranu)
        {
            Jmeno = jmeno;
            Varianta = varianta;

            Zivoty = zivoty;
            MaxZivoty = zivoty;

            DamageMin = damageMin;
            DamageMax = damageMax;

            Energie = energie;
            MaxEnergie = energie;

            NazevEnergie = nazevEnergie;

            JeKouzelnik = jeKouzelnik;
            SanceNaObranu = sanceNaObranu;
        }

        public int ZakladniUtok()
        {
            return Nahoda.Rnd.Next(DamageMin, DamageMax + 1);
        }

        public int SpecialniUtok()
        {
            int min;
            int max;
            int cenaEnergie;

            if (JeKouzelnik == true)
            {
                cenaEnergie = 25;

                min = DamageMin * 2;
                max = DamageMax * 2;
            }
            else
            {
                cenaEnergie = 15;

                min = (int)Math.Ceiling(DamageMin * 1.5);
                max = (int)Math.Ceiling(DamageMax * 1.5);
            }

            if (Energie < cenaEnergie)
            {
                return -1;
            }

            Energie -= cenaEnergie;

            return Nahoda.Rnd.Next(min, max + 1);
        }

        public void PrijmiPoskozeni(int damage)
        {
            int nahoda = Nahoda.Rnd.Next(1, 101);

            if (JeKouzelnik == true)
            {
                if (nahoda <= SanceNaObranu)
                {
                    Console.WriteLine("Kouzelnik se vyhnul utoku!");
                }
                else
                {
                    Zivoty -= damage;
                }
            }
            else
            {
                if (nahoda <= SanceNaObranu)
                {
                    int snizenePoskozeni =
                        (int)Math.Ceiling(damage * 0.5);

                    Console.WriteLine("Bojovnik zablokoval utok!");

                    Zivoty -= snizenePoskozeni;
                }
                else
                {
                    Zivoty -= damage;
                }
            }

            if (Zivoty < 0)
            {
                Zivoty = 0;
            }
        }

        public void RegenerujEnergii()
        {
            Energie += 5;

            if (Energie > MaxEnergie)
            {
                Energie = MaxEnergie;
            }
        }

        public int PouzijLektvar()
        {
            if (LektvarPouzit == true || Zivoty == MaxZivoty)
            {
                return 0;
            }

            int puvodniZivoty = Zivoty;

            Zivoty += 35;

            if (Zivoty > MaxZivoty)
            {
                Zivoty = MaxZivoty;
            }

            LektvarPouzit = true;

            return Zivoty - puvodniZivoty;
        }
    }
}