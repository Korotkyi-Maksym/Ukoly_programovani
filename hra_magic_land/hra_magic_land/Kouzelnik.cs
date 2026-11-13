using hra_magic_land;

namespace hra_magic_land
{
    internal class Kouzelnik
    {
        public Postava VytvorKouzelnik(string jmeno)
        {
            TypMaga typ = (TypMaga)Nahoda.Rnd.Next(1, 4);
            int hp = 0;
            int mana = 0;
            int damageMin = 0;
            int damageMax = 0;
            int dodge = 0;
            switch (typ)
            {
                case TypMaga.Zacatecnik:
                    hp = 80;
                    mana = 100;
                    damageMin = 8;
                    damageMax = 12;
                    dodge = 25;
                    break;
                case TypMaga.MiddleArch:
                    hp = 88;
                    mana = 130;
                    damageMin = 11;
                    damageMax = 15;
                    dodge = 28;
                    break;
                case TypMaga.Archmag:
                    hp = 96;
                    mana = 160;
                    damageMin = 14;
                    damageMax = 19;
                    dodge = 30;
                    break;
            }

            Postava postava = new Postava(
                jmeno,
                typ.ToString(),
                hp,
                damageMin,
                damageMax,
                mana,
                "Mana",
                true,
                dodge
            );

            return postava;
        }
    }
}