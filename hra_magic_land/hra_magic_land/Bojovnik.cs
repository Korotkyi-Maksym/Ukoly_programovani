using hra_magic_land;

namespace hra_magic_land
{
    internal class Bojovnik
    {
        public Postava VytvorBojovnika(string jmeno)
        {
            TypBojovnika typ = (TypBojovnika)Nahoda.Rnd.Next(1, 4);
            int hp = 0;
            int stamina = 0;
            int damageMin = 0;
            int damageMax = 0;
            int block = 0;
            switch (typ)
            {
                case TypBojovnika.Hero:
                    hp = 100;
                    stamina = 50;
                    damageMin = 10;
                    damageMax = 15;
                    block = 25;
                    break;

                case TypBojovnika.KingRoyalty:
                    hp = 115;
                    stamina = 70;
                    damageMin = 14;
                    damageMax = 20;
                    block = 30;
                    break;
                case TypBojovnika.DemonSlayer:
                    hp = 130;
                    stamina = 90;
                    damageMin = 17;
                    damageMax = 24;
                    block = 35;
                    break;
            }
            Postava postava = new Postava(
                jmeno,
                typ.ToString(),
                hp,
                damageMin,
                damageMax,
                stamina,
                "Stamina",
                false,
                block
            );
            return postava;
        }
    }
}