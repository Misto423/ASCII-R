using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    public enum PClass
    {
        Warrior,
        Knight,
        Cleric,
        Mage,
        Rogue,
    };

    class PlayerInfo
    {
        //stats
        private int level;
        private long exp;
        private int hp, str, def, intel, fort, spd;
        private int curhungr, curdet, curhp;
        private int hungrMax, detecMax;
        private PClass c;
        //equipped stuff for adventure mode
        private ItemData weapon;
        private ItemData magWeap;
        private ItemData armor;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public long Experience
        {
            get { return exp; }
            set { exp = value; }
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public int CurrentHP
        {
            get { return curhp; }
            set { curhp = value; }
        }
        public int Strength
        {
            get { return str; }
            set { str = value; }
        }
        public int Defense
        {
            get { return def; }
            set { def = value; }
        }
        public int Intelligence
        {
            get { return intel; }
            set { intel = value; }
        }
        public int Fortitude
        {
            get { return fort; }
            set { fort = value; }
        }
        public int Speed
        {
            get { return spd; }
            set { spd = value; }
        }
        public int CurrentHunger
        {
            get { return curhungr; }
            set { curhungr = value; }
        }
        public int MaxHunger
        {
            get { return hungrMax; }
            set { hungrMax = value; }
        }
        public int CurrentDetection
        {
            get { return curdet; }
            set { curdet = value; }
        }
        public int MaxDetection
        {
            get { return detecMax; }
            set { detecMax = value; }
        }
        public PClass PlayerClass
        {
            get { return c; }
            set { c = value; }
        }
        public ItemData EquippedWeapon
        {
            get { return weapon; }
            set { weapon = value; }
        }
        public ItemData EquippedMagicWeapon
        {
            get { return magWeap; }
            set { magWeap = value; }
        }
        public ItemData EquippedArmor
        {
            get { return armor; }
            set { armor = value; }
        }

        public void playerInit()
        {
            Level = 1;
            Experience = 0;
            switch (PlayerClass)
            {
                case PClass.Warrior:
                    HP = 50;
                    CurrentHP = 50;
                    Strength = 6;
                    Defense = 2;
                    Intelligence = 1;
                    Fortitude = 1;
                    Speed = 3;
                    break;
                case PClass.Knight:
                    HP = 40;
                    CurrentHP = 40;
                    Strength = 4;
                    Defense = 5;
                    Intelligence = 2;
                    Fortitude = 2;
                    Speed = 1;
                    break;
                case PClass.Cleric:
                    HP = 25;
                    CurrentHP = 25;
                    Strength = 1;
                    Defense = 2;
                    Intelligence = 6;
                    Fortitude = 4;
                    Speed = 3;
                    break;
                case PClass.Mage:
                    HP = 30;
                    CurrentHP = 30;
                    Strength = 2;
                    Defense = 1;
                    Intelligence = 5;
                    Fortitude = 3;
                    Speed = 4;
                    break;
                case PClass.Rogue:
                    HP = 35;
                    CurrentHP = 35;
                    Strength = 3;
                    Defense = 1;
                    Intelligence = 3;
                    Fortitude = 2;
                    Speed = 5;
                    break;
            }
        }

        public void levelUp()
        {
            Random r = new Random();
            int percentage;
            Level += 1;

            switch (PlayerClass)
            {
                case PClass.Warrior:
                    #region Warrior Growths
                    percentage = r.Next(1, 100);
                    if (percentage > 25)
                    {
                        HP += 5;
                        CurrentHP = HP;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 10)
                    {
                        Strength += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 20)
                    {
                        Defense += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 90)
                    {
                        Intelligence += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 70)
                    {
                        Fortitude += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 50)
                    {
                        Speed += 1;
                    }
                    #endregion
                    break;
                case PClass.Knight:
                    #region Knight Growths
                    percentage = r.Next(1, 100);
                    if (percentage > 35)
                    {
                        HP += 5;
                        CurrentHP = HP;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 50)
                    {
                        Strength += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 10)
                    {
                        Defense += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 65)
                    {
                        Intelligence += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 35)
                    {
                        Fortitude += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 85)
                    {
                        Speed += 1;
                    }
                    #endregion
                    break;
                case PClass.Cleric:
                    #region Cleric Growths
                    percentage = r.Next(1, 100);
                    if (percentage > 50)
                    {
                        HP += 5;
                        CurrentHP = HP;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 90)
                    {
                        Strength += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 75)
                    {
                        Defense += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 15)
                    {
                        Intelligence += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 25)
                    {
                        Fortitude += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 50)
                    {
                        Speed += 1;
                    }
                    #endregion
                    break;
                case PClass.Mage:
                    #region Mage Growths
                    percentage = r.Next(1, 100);
                    if (percentage > 60)
                    {
                        HP += 5;
                        CurrentHP = HP;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 85)
                    {
                        Strength += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 70)
                    {
                        Defense += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 5)
                    {
                        Intelligence += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 45)
                    {
                        Fortitude += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 45)
                    {
                        Speed += 1;
                    }
                    #endregion
                    break;
                case PClass.Rogue:
                    #region Rouge Growths
                    percentage = r.Next(1, 100);
                    if (percentage > 55)
                    {
                        HP += 5;
                        CurrentHP = HP;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 40)
                    {
                        Strength += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 75)
                    {
                        Defense += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 50)
                    {
                        Intelligence += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 80)
                    {
                        Fortitude += 1;
                    }
                    percentage = r.Next(1, 100);
                    if (percentage > 10)
                    {
                        Speed += 1;
                    }
                    #endregion
                    break;
            }
        }

        public void updateStats()
        {
            //update stats based on equipped stuff
        }
    }
}
