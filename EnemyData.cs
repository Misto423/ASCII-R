using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    public class EnemyData
    {
        public string[] battleView;
        public string name;
        public char mapView;
        private int curhp, hp, str, def, intel, fort, spd;
        private int expw;

        public EnemyData() { }

        public EnemyData(string[] batV, string name, char mapView, int hp, int str,
                            int def, int intel, int fort, int spd, int exp)
        {
            battleView = batV;
            this.name = name;
            this.mapView = mapView;
            HP = hp;
            CurrentHP = hp;
            Strength = str;
            Defense = def;
            Intelligence = intel;
            Fortitude = fort;
            Speed = spd;
            Experience = exp;
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
        public int Experience
        {
            get { return expw; }
            set { expw = value; }
        }

        public char MapView
        {
            get { return mapView; }
        }
    }

    public class EnemyFill
    {
        public static void fillEnemyData()
        {
            string[] batV = {"   _____  ",
                             "  /     \\ ",
                             " (  o o  )",
                             "  \\_____/ "};
            GameLib.enemies.Add(new EnemyData(batV, "Ooze", 'o', 5, 3, 2, 0, 2, 1, 5));
            //
            batV = new string[] {"     _.._    _ ",
                                 "   .\"\\__/\"./`_\\",
                                 " _/__<__>__\\/  ",
                                 " \"/_/\"\"\"\"\\_\\\\  "};
            GameLib.enemies.Add(new EnemyData(batV, "Turtle", 't', 10, 8, 10, 3, 2, 1, 15));
            //
            batV = new string[] {"    __    ",
                                 " | /  \\ | ",
                                 "\\_\\\\  //_/",
                                 " .'/()\\'. ",
                                 "  \\\\  //  "};
            GameLib.enemies.Add(new EnemyData(batV, "Spider", 'p', 25, 5, 5, 5, 5, 3, 30));
            //
            batV = new string[] {"                        .     ",
                                 "              /^\\     .       ",
                                 "         /\\   \"V\"             ",
                                 "        /__\\   I      O  o    ",
                                 "       //..\\\\  I     .        ",
                                 "       \\].`[/  I              ",
                                 "       /l\\/j\\  (]    .  O     ",
                                 "      /. ~~ ,\\/I          .   ",
                                 "      \\\\L__j^\\/I       o      ",
                                 "       \\/--v}  I     o   .    ",
                                 "       |    |  I   _________  ",
                                 "       |    |  I c(`       ')o",
                                 "       |    l  I   \\.     ,/  ",
                                 "     _/j  L l\\_!  _//^---^\\\\_ "};
            GameLib.enemies.Add(new EnemyData(batV, "Wizzard", 'w', 50, 2, 4, 10, 8, 3, 50)); 
            //
            batV = new string[] {" <>=======()                           ",
                                 "(/\\___   /|\\\\          ()==========<>_ ",
                                 "      \\_/ | \\\\        //|\\   ______/ \\)",
                                 "        \\_|  \\\\      // | \\_/          ",
                                 "          \\|\\/|\\_   //  /\\/            ",
                                 "           (oo)\\ \\_//  /               ",
                                 "           //_/\\_\\/ /  |               ",
                                 "          @@/  |=\\  \\  |               ",
                                 "               \\_=\\_ \\ |               ",
                                 "                 \\==\\ \\|\\_             ",
                                 "              __(\\===\\(  )\\            ",
                                 "             (((~) __(_/   |           ",
                                 "                  (((~) \\  /           ",
                                 "                  ______/ /            "};
            GameLib.enemies.Add(new EnemyData(batV, "Dragon", 'd', 75, 10, 7, 7, 5, 4, 75));
        }
    }
}
