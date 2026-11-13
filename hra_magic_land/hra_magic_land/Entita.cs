using hra_magic_land;
using System;

namespace hra_magic_land
{
    internal class Entita
    {
        public string Jmeno;
        public int Zivoty;
        public int MaxZivoty;
        public int DamageMin;
        public int DamageMax;

        public Entita(string jmeno,int zivoty,int damageMin,int damageMax)
        {
            Jmeno = jmeno;
            Zivoty = zivoty;
            MaxZivoty = zivoty;
            DamageMin = damageMin;
            DamageMax = damageMax;
        }

        public int Utok()
        {
            return Nahoda.Rnd.Next(DamageMin, DamageMax + 1);
        }

        public void PrijmiPoskozeni(int damage)
        {
            Zivoty -= damage;

            if (Zivoty < 0)
            {
                Zivoty = 0;
            }
        }
    }
}