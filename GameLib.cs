using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    class GameLib
    {
        //possible states
        public enum GameState
        {
            Battle,
            Menu,
            Game_Adv,
            Game_Sur,
            Game_Sth,
            Options,
            ClassChooser,
            GameOver
        }

        //determine the difficulty settings
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }

        // Define the frequencies of notes in an octave, as well as  
        // silence (rest). 
        public enum Tone
        {
            REST = 0,
            GbelowC = 196,
            A = 220,
            Asharp = 233,
            B = 247,
            C = 262,
            Csharp = 277,
            D = 294,
            Dsharp = 311,
            E = 330,
            F = 349,
            Fsharp = 370,
            G = 392,
            Gsharp = 415,
        }

        // Define the duration of a note in units of milliseconds. 
        public enum Duration
        {
            WHOLE = 1600,
            HALF = WHOLE / 2,
            QUARTER = HALF / 2,
            EIGHTH = QUARTER / 2,
            SIXTEENTH = EIGHTH / 2,
        }

        public static MusicThread mth = new MusicThread();

        public static char[,] map;
        public static Coord<int, int> playerPosition = new Coord<int,int>();
        public static Coord<int, int> playerScreenPosition = new Coord<int,int>();
        public static PlayerInfo player = new PlayerInfo();
        public static readonly string[] playerBattle = {"  _  ",
                                                        " (_) ",
                                                        "__|__",
                                                        "  |  ",
                                                        " / \\ "};
        public static readonly string[] playerVictory = {"  _  ",
                                                        " (_) ",
                                                        "\\_|_/",
                                                        "  |  ",
                                                        " / \\ "};
        public static Coord<int, int> screenPos;

        //options
        public static bool mysteryMode = false;
        public static GameState curState = GameState.Menu;
        public static Difficulty diff = Difficulty.Normal;
        public static List<char> keyInv = new List<char>();
        public static List<Coord<int, int>> keyPositions = new List<Coord<int, int>>();

        public static readonly string[] titleASCII =  {" ______  ______  ______  __  __  ______    ",
                                                       "/\\  __ \\/\\  ___\\/\\  ___\\/\\ \\/\\ \\/\\  == \\   ",
                                                       "\\ \\  __ \\ \\___  \\ \\ \\___\\ \\ \\ \\ \\ \\  __<   ",
                                                       " \\ \\_\\ \\_\\/\\_____\\ \\_____\\ \\_\\ \\_\\ \\_\\ \\_\\ ",
                                                       "  \\/_/\\/_/\\/_____/\\/_____/\\/_/\\/_/\\/_/ /_/ "};
        public static readonly string[] gameOverASCII = {" _______  _______  _______  _______    _______           _______  _______ ",
                                                         "(  ____ \\(  ___  )(       )(  ____ \\  (  ___  )|\\     /|(  ____ \\(  ____ )",
                                                         "| (    \\/| (   ) || () () || (    \\/  | (   ) || )   ( || (    \\/| (    )|",
                                                         "| |      | (___) || || || || (__      | |   | || |   | || (__    | (____)|",
                                                         "| | ____ |  ___  || |(_)| ||  __)     | |   | |( (   ) )|  __)   |     __)",
                                                         "| | \\_  )| (   ) || |   | || (        | |   | | \\ \\_/ / | (      | (\\ (   ",
                                                         "| (___) || )   ( || )   ( || (____/\\  | (___) |  \\   /  | (____/\\| ) \\ \\__",
                                                         "(_______)|/     \\||/     \\|(_______/  (_______)   \\_/   (_______/|/   \\__/"};

        public static readonly string[] MenuOptions = {"Survival Mode\n",
                                                       "Stealth Mode\n",
                                                       "Adventure Mode\n",
                                                       "Options\n",
                                                       "Exit"};
        public static ConsoleKey[] controls = { ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D, ConsoleKey.Spacebar };
        public static readonly string[] controlDesc = { "Up     ", "Down  ", "Left  ", "Right ", "Action" };
        public static readonly ConsoleColor[] allColors = {ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkGray,
                                                              ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkYellow,
                                                              ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Yellow};
        public static ConsoleColor[] colors = { ConsoleColor.White, ConsoleColor.DarkCyan, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow };
        public static readonly string[] colorDesc = {"Walls     ", "Player    ", "Bad Things", "Doors     ", "Loot      "};
        public static int[] colorIndex = { 13, 3, 12, 10, 14 };

        //items
        public static List<ItemData> weapons = new List<ItemData>();
        public static List<ItemData> mWeapons = new List<ItemData>();
        public static List<ItemData> armor = new List<ItemData>();
        //food
        public static List<Food> food = new List<Food>();

        //exp chart
        public static readonly int[] expChart = {10, 25, 40, 75, 120, 185, 250, 400, 590, 800, 
                                                 1050, 1400, 2000, 2900, 3850, 5000, 6700, 8800, 11500, 15000,
                                                 20000, 27500, 38900, 50000, 65000, 80000, 100000, 125000, 150000, 200000};

        //Enemies
        public static List<EnemyData> enemies = new List<EnemyData>();


        public static void resetOptions()
        {
            controls[0] = ConsoleKey.W;
            controls[1] = ConsoleKey.S;
            controls[2] = ConsoleKey.A;
            controls[3] = ConsoleKey.D;
            controls[4] = ConsoleKey.Spacebar;
            colors[0] = ConsoleColor.White;
            colors[1] = ConsoleColor.DarkCyan;
            colors[2] = ConsoleColor.Red;
            colors[3] = ConsoleColor.Green;
            colors[4] = ConsoleColor.Yellow;
            colorIndex[0] = 13;
            colorIndex[1] = 3;
            colorIndex[2] = 12;
            colorIndex[3] = 10;
            colorIndex[4] = 14;
            diff = Difficulty.Normal;
            mysteryMode = false;
            Program.updateDisplay();
        }

        public static void gameOverReset()
        {
            player = new PlayerInfo();
            screenPos = new Coord<int, int>(0, 0);
            curState = GameState.Menu;
            Program.initMap();
        }

        public static void playSelectionTune()
        {
            Console.Beep((int)GameLib.Tone.E, (int)GameLib.Duration.SIXTEENTH);
            Console.Beep((int)GameLib.Tone.G, (int)GameLib.Duration.QUARTER);
        }

        //Icon Changing Function

        [DllImport("kernel32")]
        public static extern bool SetConsoleIcon(IntPtr hIcon);

// ______  ______  ______  __  __  ______    
///\  __ \/\  ___\/\  ___\/\ \/\ \/\  == \   
//\ \  __ \ \___  \ \ \___\ \ \ \ \ \  __<   
// \ \_\ \_\/\_____\ \_____\ \_\ \_\ \_\ \_\ 
//  \/_/\/_/\/_____/\/_____/\/_/\/_/\/_/ /_/
    }
}
