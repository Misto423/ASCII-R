using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

//try versions of these.
//http://roguebasin.roguelikedevelopment.org/index.php?title=Dungeon-Building_Algorithm
//http://roguebasin.roguelikedevelopment.org/index.php?title=Grid_Based_Dungeon_Generator

namespace ASCIIMazeCS
{
    class Program
    {
        public static readonly int HORZ = 256;
        public static readonly int VERT = 256;
        public static readonly int SCW = 32;
        public static readonly int SCH = 64;


        public static Coord<int, int> cursorClassPos = new Coord<int, int>(27, 14);
        public static Coord<int, int> cursorMenuPos = new Coord<int, int>(25, 18);
        public static Coord<int, int> cursorOpPos = new Coord<int, int>(2, 5);

        //private static bool mapWasGenerated = false;
        protected static bool transAlt = true;
        protected static int count = 0;

        public static InputClass Player;
        static MapGen mg = new MapGen();

        static void Main(string[] args)
        {
            GameLib.map = new char[HORZ, VERT];
            Console.SetWindowSize(80, 33);
            Console.CursorVisible = false;
            Console.Title = "ASCII Roguelike";

            GameLib.SetConsoleIcon(Properties.Resources.gameico.Handle);

            ItemFill.fillData();
            EnemyFill.fillEnemyData();
            BattleMechanics bm = new BattleMechanics();
            AI ai = new AI();
            Random r = new Random();

            for (int x = 0; x < HORZ; x++)
                for (int y = 0; y < VERT; y++)
                    GameLib.map[x, y] = '\x0';

            GameLib.screenPos = new Coord<int, int>(0, 0);

            InputClass inpc = new InputClass(HORZ, VERT);
            Player = inpc;

            GameLib.mth.updateMusic();

            //initMap();
            //test data remove later
            //GameLib.player.PlayerClass = PClass.Cleric;
            //GameLib.player.playerInit();
            //


            if (GameLib.curState == GameLib.GameState.Menu)
            {
                displayMenu();
                inpc.getMenuInput();
            }
            ////remove after testing
            //if (GameLib.curState == GameLib.GameState.Battle)
            //{
            //    //displayBattle();
            //    bm.startBattle(GameLib.enemies[0]);
            //}
            //if (GameLib.curState == GameLib.GameState.GameOver)
            //{
            //    displayGameOver();
            //}

            while (true)
            {

                if (GameLib.curState == GameLib.GameState.Game_Adv || GameLib.curState == GameLib.GameState.Game_Sur ||
                    GameLib.curState == GameLib.GameState.Game_Sth)
                {
                    inpc.getInput();
                    if (GameLib.curState == GameLib.GameState.Game_Adv)
                    {
                        //int battle = r.Next(1, 100);
                        if (inpc.getEncounterChance > 90)
                        {
                            while (count < 4)
                            {
                                drawTransition();
                            }
                            count = 0;
                            GameLib.curState = GameLib.GameState.Battle;
                            bm.startBattle(ai.pickEnemy(mg.curQuadrant(), r.Next(1, 100)));
                        }
                    }
                }
                else if (GameLib.curState == GameLib.GameState.Menu)
                {
                    inpc.getMenuInput();
                }
                else if (GameLib.curState == GameLib.GameState.Options)
                {
                    inpc.getOptionsInput();
                }
                else if (GameLib.curState == GameLib.GameState.ClassChooser)
                {
                    inpc.getPlayerClass();
                }
            }


        }

        public static void initMap()
        {
            mg.generateRooms(HORZ, VERT);
            mg.generateHalls(HORZ, VERT);

            if (GameLib.curState == GameLib.GameState.Game_Adv)
            {
                mg.genKey();
                mg.genAdvItems();
               // mg.generateEnemyPositions();
            }
        }

        public static void updateView(int direction)
        {
            switch (direction)
            {
                case 1:
                    if (GameLib.screenPos.YPos >= 32)
                    {
                        GameLib.screenPos.YPos -= 32;
                        //mg.generateEnemyPositions();
                        updateDisplay();
                    }
                    break;
                case 2:
                    if (GameLib.screenPos.YPos < (HORZ - 32))
                    {
                        GameLib.screenPos.YPos += 32;
                        //mg.generateEnemyPositions();
                        updateDisplay();
                    }
                    break;
                case 3:
                    if (GameLib.screenPos.XPos >= 64)
                    {
                        GameLib.screenPos.XPos -= 64;
                        //mg.generateEnemyPositions();
                        updateDisplay();
                    }
                    break;
                case 4:
                    if (GameLib.screenPos.XPos < (VERT - 64))
                    {
                        GameLib.screenPos.XPos += 64;
                        //mg.generateEnemyPositions();
                        updateDisplay();
                    }
                    break;
            }
        }

        private static void drawTransition()
        {
            count++;
            Console.Clear();
            if (transAlt)
            {
                Console.BackgroundColor = ConsoleColor.White;
                displayMap(ConsoleColor.Black);
                transAlt = !transAlt;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                displayMap(GameLib.colors[0]);
                transAlt = !transAlt;
            }
            System.Threading.Thread.Sleep(150);
        }

        public static void updateDisplay()
        {
            Console.Clear();
            switch (GameLib.curState)
            {
                case GameLib.GameState.Menu:
                    //MapGen.TurnOffAI();
                    displayMenu();
                    break;
                case GameLib.GameState.Options:
                    //MapGen.TurnOffAI();
                    displayOptions();
                    break;
                case GameLib.GameState.Game_Sur:
                    displayMap(GameLib.colors[0]);
                    //MapGen.TurnOffAI();
                    Console.ForegroundColor = GameLib.colors[1];
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos);
                    Console.Write('P');
                    Console.ForegroundColor = GameLib.colors[0];
                    break;
                case GameLib.GameState.Game_Sth:
                    displayMap(GameLib.colors[0]);
                    //MapGen.TurnOffAI();
                    Console.ForegroundColor = GameLib.colors[1];
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos);
                    Console.Write('P');
                    Console.ForegroundColor = GameLib.colors[0];
                    break;
                case GameLib.GameState.ClassChooser:
                    //MapGen.TurnOffAI();
                    displayClassSelect();
                    break;
                case GameLib.GameState.Game_Adv:
                    displayMap(GameLib.colors[0]);
                    displayInfoBar();
                    // MapGen.TurnOnAI();
                    break;
                case GameLib.GameState.GameOver:
                    //MapGen.TurnOffAI();
                    displayGameOver();
                    break;
                default:
                    //MapGen.TurnOffAI();
                    displayMenu();
                    break;
            }
        }

        public static void displayMap(ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            for (int x = GameLib.screenPos.YPos; x < GameLib.screenPos.YPos + SCW; x++)
            {
                for (int y = GameLib.screenPos.XPos; y < GameLib.screenPos.XPos + SCH; y++)
                {
                    if (GameLib.map[y, x] >= '\x30' && GameLib.map[y, x] <= '\x39')
                    {
                        for (int k = 0; k < GameLib.keyPositions.Count; k++)
                        {
                            if (x == GameLib.keyPositions[k].XPos && y == GameLib.keyPositions[k].YPos)
                            {
                                Console.ForegroundColor = GameLib.colors[4];
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = GameLib.colors[3];
                            }
                        } 
                    }
                    else if (GameLib.map[y, x] >= '\x40')
                    {
                        Console.ForegroundColor = GameLib.colors[4];
                    }
                    else
                    {
                        Console.ForegroundColor = cc;
                    }
                    Console.Write(GameLib.map[y, x]);
                }
                Console.WriteLine();
            }

            //if (!mapWasGenerated)
            //{
            //    //mg.GenerateEnemyPlacement();
            //    mapWasGenerated = true;
            //}
            //else
            //{
            //    //mg.LoadEnemyPlacement();
            //}
        }

        public static void displayMenu()
        {
            int yPos = 3;
            for (int s = 0; s < GameLib.titleASCII.Length; s++)
            {
                Console.SetCursorPosition(18, yPos);
                Console.WriteLine(GameLib.titleASCII[s]);
                yPos++;
            }
            Console.SetCursorPosition(30, 15);
            Console.WriteLine("Select Your Game");
            Console.SetCursorPosition(30, 16);
            Console.WriteLine("----------------");
            yPos = 18;
            for (int s = 0; s < GameLib.MenuOptions.Length; s++)
            {
                Console.SetCursorPosition(31, yPos);
                Console.WriteLine(GameLib.MenuOptions[s]);
                yPos += 2;
            }
            Console.SetCursorPosition(cursorMenuPos.XPos, cursorMenuPos.YPos);
            Console.Write("->");
        }

        public static void displayOptions()
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                if (x != 40)
                {
                    Console.SetCursorPosition(x, 15);

                    Console.Write('=');
                }
            }
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                Console.SetCursorPosition(40, y);
                Console.Write('|');
            }

            int yControlPos = 5;
            //top left square
            Console.SetCursorPosition(15, 1);
            Console.Write("Input");
            Console.SetCursorPosition(5, 2);
            Console.Write("Press Action, Then Press Key");
            for (int c = 0; c < GameLib.controls.Length; c++)
            {
                Console.SetCursorPosition(5, yControlPos);
                Console.Write(GameLib.controlDesc[c] + "\t\t" + GameLib.controls[c].ToString());
                yControlPos += 2;
            }
            //top right square
            yControlPos = 5;
            Console.SetCursorPosition(55, 1);
            Console.Write("Colors");
            Console.SetCursorPosition(42, 2);
            Console.Write("Press Action, Left/Right, then Action");
            for (int c = 0; c < GameLib.colors.Length; c++)
            {
                Console.SetCursorPosition(45, yControlPos);
                Console.ForegroundColor = GameLib.colors[c];
                Console.Write(GameLib.colorDesc[c] + "\t\t" + GameLib.colors[c].ToString());
                yControlPos += 2;
            }
            Console.ForegroundColor = ConsoleColor.White;
            //bottom right square
            Console.SetCursorPosition(55, 18);
            Console.Write("Written By");
            Console.SetCursorPosition(55, 19);
            Console.Write("----------");
            Console.SetCursorPosition(52, 21);
            Console.Write("Shane Shafferman");
            Console.SetCursorPosition(52, 22);
            Console.Write("Eric Deas");
            Console.SetCursorPosition(52, 23);
            Console.Write("Sam Wheat");
            //bottom left square
            Console.SetCursorPosition(15, 17);
            Console.Write("Difficulty");
            Console.SetCursorPosition(8, 18);
            if (GameLib.mysteryMode)
            {
                Console.Write("Selected: " + GameLib.diff.ToString() + " With Fog");
            }
            else
            {
                Console.Write("Selected: " + GameLib.diff.ToString() + " With No Fog");
            }
            Console.SetCursorPosition(17, 21);
            Console.Write(GameLib.Difficulty.Easy.ToString());
            Console.SetCursorPosition(17, 23);
            Console.Write(GameLib.Difficulty.Normal.ToString());
            Console.SetCursorPosition(17, 25);
            Console.Write(GameLib.Difficulty.Hard.ToString());
            Console.SetCursorPosition(17, 27);
            Console.Write("Toggle Fog");
            Console.SetCursorPosition(13, 29);
            Console.Write("Reset All Options");

            Console.SetCursorPosition(cursorOpPos.XPos, cursorOpPos.YPos);
            Console.Write("->");
        }

        public static void displayClassSelect()
        {
            Console.SetCursorPosition(30, 10);
            Console.Write("Choose Your Class");
            Console.SetCursorPosition(30, 11);
            Console.Write("-----------------");
            Console.SetCursorPosition(34, 14);
            Console.Write("Warrior");
            Console.SetCursorPosition(34, 16);
            Console.Write("Knight");
            Console.SetCursorPosition(34, 18);
            Console.Write("Cleric");
            Console.SetCursorPosition(34, 20);
            Console.Write("Mage");
            Console.SetCursorPosition(34, 22);
            Console.Write("Rogue");
            Console.SetCursorPosition(cursorClassPos.XPos, cursorClassPos.YPos);
            Console.Write("->");
        }

        public static void displayInfoBar()
        {
            Console.ForegroundColor = GameLib.colors[0];
            for (int y = 0; y < 33; y++)
            {
                Console.SetCursorPosition(66, y);
                Console.Write('|');
            }
            Console.SetCursorPosition(67, 2);
            Console.Write("Class:");
            Console.SetCursorPosition(68, 3);
            Console.Write(GameLib.player.PlayerClass.ToString());
            Console.SetCursorPosition(67, 6);
            Console.Write("Level: " + GameLib.player.Level);
            Console.SetCursorPosition(67, 7);
            Console.Write(" HP: " + GameLib.player.CurrentHP + "/" + GameLib.player.HP);
            Console.SetCursorPosition(67, 8);
            Console.Write("Str: " + GameLib.player.Strength);
            Console.SetCursorPosition(67, 9);
            Console.Write("Def: " + GameLib.player.Defense);
            Console.SetCursorPosition(67, 10);
            Console.Write("Int: " + GameLib.player.Intelligence);
            Console.SetCursorPosition(67, 11);
            Console.Write("Ftd: " + GameLib.player.Fortitude);
            Console.SetCursorPosition(67, 12);
            Console.Write("Spd: " + GameLib.player.Speed);
            Console.SetCursorPosition(67, 15);
            Console.Write("Eqp Weapon:");
            Console.SetCursorPosition(68, 16);
            if (GameLib.player.EquippedWeapon != null)
            {
                Console.Write(GameLib.player.EquippedWeapon.Name);
            }
            else
            {
                Console.Write("None");
            }
            Console.SetCursorPosition(67, 19);
            Console.Write("Eqp Magic:");
            Console.SetCursorPosition(68, 20);
            if (GameLib.player.EquippedMagicWeapon != null)
            {
                Console.Write(GameLib.player.EquippedMagicWeapon.Name);
            }
            else
            {
                Console.Write("None");
            }
            Console.SetCursorPosition(67, 23);
            Console.Write("Eqp Armor:");
            Console.SetCursorPosition(68, 24);
            if (GameLib.player.EquippedArmor != null)
            {
                Console.Write(GameLib.player.EquippedArmor.Name);
            }
            else
            {
                Console.Write("None");
            }

            for (int i = 0; i < GameLib.keyInv.Count; i++)
            {
                if (i < 8)
                {
                    Console.SetCursorPosition(67 + i, 27);
                    Console.Write(GameLib.keyInv[i]);
                }
                else
                {
                    Console.SetCursorPosition(67 + i, 28);
                    Console.Write(GameLib.keyInv[i]);
                }
            }
        }

        public static void displayGameOver()
        {
            for (int y = 10; y < 10 + GameLib.gameOverASCII.Length; y++)
            {
                Console.SetCursorPosition(3, y);
                Console.Write(GameLib.gameOverASCII[y - 10]);
            }
            Console.SetCursorPosition(30, 20);
            Console.Write("Press Action Key");
            ConsoleKeyInfo waiting;
            do
            {
                waiting = Console.ReadKey(true);
            } while (waiting.Key != GameLib.controls[4]);
            GameLib.curState = GameLib.GameState.Menu;
            GameLib.gameOverReset();
            updateDisplay();
        }

        public static void endEVERYTHING()
        {
            Environment.Exit(23);
        }
    }


}
