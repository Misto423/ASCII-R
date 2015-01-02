using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    class InputClass
    {
        private int maxX, maxY;
        private string characterSymbol;
        private int ec = -1;

        public int getEncounterChance
        {
            get { return ec; }
        }

        public InputClass(int maxX, int maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.characterSymbol = "P";
        }

        protected String CharacterSymbol
        {
            get { return this.characterSymbol; }
            set { characterSymbol = value; }
        }

        protected int MaxY
        {
            get { return maxY; }
        }

        protected int MaxX
        {
            get { return maxX; }
        }

        public virtual bool collisionDetect(char dir)
        {
            if (dir == 'a')
            {
                if ((GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] >= '\x30' &&
                    GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] <= '\x7A') ||
                    (GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] == '#'))
                {
                    return true;
                }
            }
            if (dir == 'd')
            {
                if ((GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos] >= '\x30' &&
                    GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos] <= '\x7A') ||
                    (GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos] == '#'))
                {
                    return true;
                }
            }
            if (dir == 'w')
            {
                if ((GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1] >= '\x30' &&
                    GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1] <= '\x7A') ||
                 (GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1] == '#'))
                {
                    return true;
                }
            }
            if (dir == 's')
            {
                if ((GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1] >= '\x30' &&
                    GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1] <= '\x7A') ||
                    GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1] == '#')
                {
                    return true;
                }
            }
            return false;
        }

        private bool getDoorStatus(char door)
        {
            //check to see if the player has the key for this door
            if (GameLib.keyInv.Contains(door))
            {
                return true;
            }
            return false;
        }

        public void getInput()
        {
            ConsoleKeyInfo control;
            Random r = new Random();
            ec = -1;
            control = Console.ReadKey(true);
            if (control.Key == GameLib.controls[2])
            {
                ec = r.Next(0, 100);
                if (!collisionDetect('a'))
                {
                    GameLib.playerPosition.XPos--;
                    GameLib.playerScreenPosition.XPos--;
                    if (GameLib.playerPosition.XPos < 0)
                    {
                        GameLib.playerPosition.XPos = 0;
                        GameLib.playerScreenPosition.XPos = 0;
                    }
                    if (GameLib.playerScreenPosition.XPos == 0)
                    {
                        Program.updateView(3);
                        GameLib.playerScreenPosition.XPos = 63;
                        GameLib.playerPosition.XPos--;
                    }
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos);
                    Console.ForegroundColor = GameLib.colors[1];
                    Console.Write("P ");
                }
            }
            else if (control.Key == GameLib.controls[1])
            {
                ec = r.Next(0, 100);
                if (!collisionDetect('s'))
                {
                    GameLib.playerPosition.YPos++;
                    GameLib.playerScreenPosition.YPos++;
                    if (GameLib.playerPosition.YPos > maxY)
                    {
                        GameLib.playerPosition.YPos = maxY;
                        GameLib.playerScreenPosition.YPos = 32;
                    }
                    if (GameLib.playerScreenPosition.YPos == 32)
                    {
                        Program.updateView(2);
                        GameLib.playerScreenPosition.YPos = 1;
                        GameLib.playerPosition.YPos++;
                    }
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos);
                    Console.ForegroundColor = GameLib.colors[1];
                    Console.Write("P");
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos - 1);
                    Console.Write(" ");
                }
            }
            else if (control.Key == GameLib.controls[3])
            {
                ec = r.Next(0, 100);
                if (!collisionDetect('d'))
                {
                    GameLib.playerPosition.XPos++;
                    GameLib.playerScreenPosition.XPos++;
                    if (GameLib.playerPosition.XPos > maxX)
                    {
                        GameLib.playerPosition.XPos = maxX;
                        GameLib.playerScreenPosition.XPos = 64;
                    }
                    if (GameLib.playerScreenPosition.XPos == 64)
                    {
                        Program.updateView(4);
                        GameLib.playerScreenPosition.XPos = 1;
                        GameLib.playerPosition.XPos++;
                    }
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos - 1, GameLib.playerScreenPosition.YPos);
                    Console.ForegroundColor = GameLib.colors[1];
                    Console.Write(" ");
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos);
                    Console.Write("P");
                }
            }
            else if (control.Key == GameLib.controls[0])
            {
                ec = r.Next(0, 100);
                if (!collisionDetect('w'))
                {
                    GameLib.playerPosition.YPos--;
                    GameLib.playerScreenPosition.YPos--;
                    if (GameLib.playerPosition.YPos < 0)
                    {
                        GameLib.playerPosition.YPos = 0;
                        GameLib.playerScreenPosition.YPos = 0;
                    }
                    if (GameLib.playerScreenPosition.YPos == 0)
                    {
                        Program.updateView(1);
                        GameLib.playerScreenPosition.YPos = 31;
                        GameLib.playerPosition.YPos--;
                    }
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos);
                    Console.ForegroundColor = GameLib.colors[1];
                    Console.Write("P");
                    GameLib.playerScreenPosition.YPos++;
                    Console.SetCursorPosition(GameLib.playerScreenPosition.XPos, GameLib.playerScreenPosition.YPos);
                    Console.Write(" ");
                    GameLib.playerScreenPosition.YPos--;
                }
            }
            else if (control.Key == GameLib.controls[4])
            {
                //unlock doors
                //pickup keys/items
                if (!nextToKey())
                {
                    //check for door
                    #region Unlock Doors
                    if (GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] >= '\x30' &&
                        GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] <= '\x39')
                    {
                        if (getDoorStatus(GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos]))
                        {
                            GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] = ' ';
                            Program.updateDisplay();
                        }
                    }
                    else if (GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos] >= '\x30' &&
                            GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos] <= '\x39')
                    {
                        if (getDoorStatus(GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos]))
                        {
                            GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos] = ' ';
                            Program.updateDisplay();
                        }
                    }
                    else if (GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1] >= '\x30' &&
                            GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1] <= '\x39')
                    {
                        if (getDoorStatus(GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1]))
                        {
                            GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1] = ' ';
                            Program.updateDisplay();
                        }
                    }
                    else if (GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1] >= '\x30' &&
                            GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1] <= '\x39')
                    {
                        if (getDoorStatus(GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1]))
                        {
                            GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1] = ' ';
                            Program.updateDisplay();
                        }
                    }
                    #endregion
                }

                //check if next to item
                if (GameLib.curState == GameLib.GameState.Game_Adv)
                { //only check in adventure mode
                    //pick up item and equip in appropriate slot
                    //then drop current item in spot to pick up if player changes mind
                    nextToWeapon();

                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private bool nextToWeapon()
        {
            for (int i = 0; i < GameLib.weapons.Count; i++)
            {
                if (GameLib.playerPosition.XPos - 1 == GameLib.weapons[i].Location.XPos && GameLib.playerPosition.YPos == GameLib.weapons[i].Location.YPos)
                {
                    swapItem(GameLib.weapons[i], 1, GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos);
                    return true;
                }
                else if (GameLib.playerPosition.XPos + 1 == GameLib.weapons[i].Location.XPos && GameLib.playerPosition.YPos == GameLib.weapons[i].Location.YPos)
                {
                    swapItem(GameLib.weapons[i], 1, GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos);
                    return true;
                }
                else if (GameLib.playerPosition.YPos - 1 == GameLib.weapons[i].Location.YPos && GameLib.playerPosition.XPos == GameLib.weapons[i].Location.XPos)
                {
                    swapItem(GameLib.weapons[i], 1, GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1);
                    return true;
                }
                else if (GameLib.playerPosition.YPos + 1 == GameLib.weapons[i].Location.YPos && GameLib.playerPosition.XPos == GameLib.weapons[i].Location.XPos)
                {
                    swapItem(GameLib.weapons[i], 1, GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1);
                    return true;
                }
            }
            return false;
        }

        public void swapItem(ItemData i, int type, int x, int y)
        {
            switch (type)
            {
                case 1: //weapon
                    if (GameLib.player.EquippedWeapon != null)
                    {
                        GameLib.map[x, y] = GameLib.player.EquippedWeapon.MapChar;
                        int index = GameLib.weapons.IndexOf(GameLib.player.EquippedWeapon);
                        GameLib.weapons[index].Location.XPos = x;
                        GameLib.weapons[index].Location.YPos = y;
                    }
                    else
                    {
                        GameLib.map[x, y] = ' ';
                    }
                    GameLib.player.EquippedWeapon = i;
                    GameLib.player.updateStats();
                    Program.updateDisplay();
                    break;
                case 2: //magic
                    if (GameLib.player.EquippedMagicWeapon != null)
                    {
                        GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] = GameLib.player.EquippedMagicWeapon.MapChar;
                    }
                    else
                    {
                        GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] = ' ';
                    }
                    GameLib.player.EquippedMagicWeapon = i;
                    GameLib.player.updateStats();
                    Program.updateDisplay();
                    break;
                case 3: //armor
                    if (GameLib.player.EquippedArmor != null)
                    {
                        GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] = GameLib.player.EquippedArmor.MapChar;
                    }
                    else
                    {
                        GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] = ' ';
                    }
                    GameLib.player.EquippedArmor = i;
                    GameLib.player.updateStats();
                    Program.updateDisplay();
                    break;
            }
        }

        private bool nextToKey()
        {
            for (int i = 0; i < GameLib.keyPositions.Count; i++)
            {
                if (GameLib.playerPosition.XPos - 1 == GameLib.keyPositions[i].YPos && GameLib.playerPosition.YPos == GameLib.keyPositions[i].XPos)
                {
                    GameLib.keyInv.Add(GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos]);
                    GameLib.map[GameLib.playerPosition.XPos - 1, GameLib.playerPosition.YPos] = ' ';
                    GameLib.keyPositions.RemoveAt(i);
                    Program.updateDisplay();
                    return true;
                }
                else if (GameLib.playerPosition.XPos + 1 == GameLib.keyPositions[i].YPos && GameLib.playerPosition.YPos == GameLib.keyPositions[i].XPos)
                {
                    GameLib.keyInv.Add(GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos]);
                    GameLib.map[GameLib.playerPosition.XPos + 1, GameLib.playerPosition.YPos] = ' ';
                    GameLib.keyPositions.RemoveAt(i);
                    Program.updateDisplay();
                    return true;
                }
                else if (GameLib.playerPosition.YPos - 1 == GameLib.keyPositions[i].XPos && GameLib.playerPosition.XPos == GameLib.keyPositions[i].YPos)
                {
                    GameLib.keyInv.Add(GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1]);
                    GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos - 1] = ' ';
                    GameLib.keyPositions.RemoveAt(i);
                    Program.updateDisplay();
                    return true;
                }
                else if (GameLib.playerPosition.YPos + 1 == GameLib.keyPositions[i].XPos && GameLib.playerPosition.XPos == GameLib.keyPositions[i].YPos)
                {
                    GameLib.keyInv.Add(GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1]);
                    GameLib.map[GameLib.playerPosition.XPos, GameLib.playerPosition.YPos + 1] = ' ';
                    GameLib.keyPositions.RemoveAt(i);
                    Program.updateDisplay();
                    return true;
                }
            }
            return false;
        }

        public void getMenuInput()
        {
            ConsoleKeyInfo control;
            control = Console.ReadKey(true);

            if (control.Key == GameLib.controls[0])
            {
                if (Program.cursorMenuPos.YPos == 18)
                {
                    Console.SetCursorPosition(25, Program.cursorMenuPos.YPos);
                    Console.Write("  ");
                    Program.cursorMenuPos.YPos = 26;
                }
                else
                {
                    Console.SetCursorPosition(25, Program.cursorMenuPos.YPos);
                    Console.Write("  ");
                    Program.cursorMenuPos.YPos -= 2;
                }
                Program.updateDisplay();
            }
            else if (control.Key == GameLib.controls[1])
            {
                if (Program.cursorMenuPos.YPos == 26)
                {
                    Console.SetCursorPosition(25, Program.cursorMenuPos.YPos);
                    Console.Write("  ");
                    Program.cursorMenuPos.YPos = 18;
                }
                else
                {
                    Console.SetCursorPosition(25, Program.cursorMenuPos.YPos);
                    Console.Write("  ");
                    Program.cursorMenuPos.YPos += 2;
                }
                Program.updateDisplay();
            }
            else if (control.Key == GameLib.controls[4])
            {
                if (Program.cursorMenuPos.YPos == 24)
                {
                    GameLib.curState = GameLib.GameState.Options;
                    GameLib.mth.updateMusic();
                    Program.updateDisplay();
                }
                else if (Program.cursorMenuPos.YPos == 22)
                {
                    GameLib.curState = GameLib.GameState.ClassChooser;
                    Program.updateDisplay();
                }
                else if (Program.cursorMenuPos.YPos == 20)
                {
                    GameLib.curState = GameLib.GameState.Game_Sth;
                    GameLib.mth.updateMusic();
                    Program.initMap();
                    Program.updateDisplay();
                }
                else if (Program.cursorMenuPos.YPos == 18)
                {
                    GameLib.curState = GameLib.GameState.Game_Sur;
                    GameLib.mth.updateMusic();
                    Program.initMap();
                    Program.updateDisplay();
                }
                else if (Program.cursorMenuPos.YPos == 26)
                {
                    Program.endEVERYTHING();
                }
            }
        }

        public void getPlayerClass()
        {
            ConsoleKeyInfo control;
            control = Console.ReadKey(true);

            if (control.Key == GameLib.controls[0])
            {
                if (Program.cursorClassPos.YPos == 14)
                {
                    Console.SetCursorPosition(27, Program.cursorClassPos.YPos);
                    Console.Write("  ");
                    Program.cursorClassPos.YPos = 22;
                }
                else
                {
                    Console.SetCursorPosition(27, Program.cursorClassPos.YPos);
                    Console.Write("  ");
                    Program.cursorClassPos.YPos -= 2;
                }
                Program.updateDisplay();
            }
            else if (control.Key == GameLib.controls[1])
            {
                if (Program.cursorClassPos.YPos == 22)
                {
                    Console.SetCursorPosition(27, Program.cursorClassPos.YPos);
                    Console.Write("  ");
                    Program.cursorClassPos.YPos = 14;
                }
                else
                {
                    Console.SetCursorPosition(27, Program.cursorClassPos.YPos);
                    Console.Write("  ");
                    Program.cursorClassPos.YPos += 2;
                }
                Program.updateDisplay();
            }
            else if (control.Key == GameLib.controls[4])
            {
                switch (Program.cursorClassPos.YPos)
                {
                    case 14:
                        GameLib.player.PlayerClass = PClass.Warrior;
                        break;
                    case 16:
                        GameLib.player.PlayerClass = PClass.Knight;
                        break;
                    case 18:
                        GameLib.player.PlayerClass = PClass.Cleric;
                        break;
                    case 20:
                        GameLib.player.PlayerClass = PClass.Mage;
                        break;
                    case 22:
                        GameLib.player.PlayerClass = PClass.Rogue;
                        break;
                    default:
                        GameLib.player.PlayerClass = PClass.Rogue;
                        break;
                }
                GameLib.player.playerInit();
                GameLib.curState = GameLib.GameState.Game_Adv;
                GameLib.mth.updateMusic();
                Program.initMap();
                Program.updateDisplay();
            }
        }

        public void getOptionsInput()
        {
            ConsoleKeyInfo control;
            control = Console.ReadKey(true);

            if (control.Key == GameLib.controls[0])
            {
                if (Program.cursorOpPos.XPos == 2)
                {
                    #region up control for input
                    if (Program.cursorOpPos.YPos == 5)
                    {
                        Program.cursorOpPos.YPos = 13;
                    }
                    else
                    {
                        Program.cursorOpPos.YPos -= 2;
                    }
                    Program.updateDisplay();
                    #endregion
                }
                if (Program.cursorOpPos.XPos == 42)
                {
                    #region up control for color
                    if (Program.cursorOpPos.YPos == 5)
                    {
                        Program.cursorOpPos.YPos = 13;
                    }
                    else
                    {
                        Program.cursorOpPos.YPos -= 2;
                    }
                    Program.updateDisplay();
                    #endregion
                }
                if (Program.cursorOpPos.XPos == 10)
                {
                    #region up controls for diff
                    if (Program.cursorOpPos.YPos == 21)
                    {
                        Program.cursorOpPos.YPos = 29;
                    }
                    else
                    {
                        Program.cursorOpPos.YPos -= 2;
                    }
                    Program.updateDisplay();
                    #endregion
                }
            }
            else if (control.Key == GameLib.controls[1])
            {
                if (Program.cursorOpPos.XPos == 2)
                {
                    #region Down control for input
                    if (Program.cursorOpPos.YPos == 13)
                    {
                        Program.cursorOpPos.YPos = 5;
                    }
                    else
                    {
                        Program.cursorOpPos.YPos += 2;
                    }
                    Program.updateDisplay();
                    #endregion
                }
                if (Program.cursorOpPos.XPos == 42)
                {
                    #region down control for color
                    if (Program.cursorOpPos.YPos == 13)
                    {
                        Program.cursorOpPos.YPos = 5;
                    }
                    else
                    {
                        Program.cursorOpPos.YPos += 2;
                    }
                    Program.updateDisplay();
                    #endregion
                }
                if (Program.cursorOpPos.XPos == 10)
                {
                    #region down controls for diff
                    if (Program.cursorOpPos.YPos == 29)
                    {
                        Program.cursorOpPos.YPos = 21;
                    }
                    else
                    {
                        Program.cursorOpPos.YPos += 2;
                    }
                    Program.updateDisplay();
                    #endregion
                }
            }
            else if (control.Key == GameLib.controls[2])
            {
                if (Program.cursorOpPos.XPos == 2)
                {
                    Program.cursorOpPos.XPos = 10;
                    Program.cursorOpPos.YPos = 21;
                }
                else if (Program.cursorOpPos.XPos == 42)
                {
                    Program.cursorOpPos.XPos = 2;
                    Program.cursorOpPos.YPos = 5;
                }
                else if (Program.cursorOpPos.XPos == 10)
                {
                    Program.cursorOpPos.XPos = 42;
                    Program.cursorOpPos.YPos = 5;
                }
                Program.updateDisplay();
            }
            else if (control.Key == GameLib.controls[3])
            {
                if (Program.cursorOpPos.XPos == 2)
                {
                    Program.cursorOpPos.XPos = 42;
                    Program.cursorOpPos.YPos = 5;
                }
                else if (Program.cursorOpPos.XPos == 42)
                {
                    Program.cursorOpPos.XPos = 10;
                    Program.cursorOpPos.YPos = 21;
                }
                else if (Program.cursorOpPos.XPos == 10)
                {
                    Program.cursorOpPos.XPos = 2;
                    Program.cursorOpPos.YPos = 5;
                }
                Program.updateDisplay();
            }
            else if (control.Key == GameLib.controls[4])
            {
                if (Program.cursorOpPos.XPos == 2)
                {
                    #region changing controls
                    ConsoleKeyInfo newChar;
                    switch (Program.cursorOpPos.YPos)
                    {
                        case (13):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                GameLib.controls[4] = newChar.Key;
                            } while (newChar == null);
                            Program.updateDisplay();
                            break;
                        case (11):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                GameLib.controls[3] = newChar.Key;
                            } while (newChar == null);
                            Program.updateDisplay();
                            break;
                        case (9):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                GameLib.controls[2] = newChar.Key;
                            } while (newChar == null);
                            Program.updateDisplay();
                            break;
                        case (7):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                GameLib.controls[1] = newChar.Key;
                            } while (newChar == null);
                            Program.updateDisplay();
                            break;
                        case (5):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                GameLib.controls[0] = newChar.Key;
                            } while (newChar == null);
                            Program.updateDisplay();
                            break;
                    }
                    #endregion
                }
                else if (Program.cursorOpPos.XPos == 42)
                {
                    #region changing colors
                    ConsoleKeyInfo newChar;
                    switch (Program.cursorOpPos.YPos)
                    {
                        case (13):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                if (newChar.Key == GameLib.controls[2])
                                {
                                    if (GameLib.colorIndex[4] == 0)
                                    {
                                        GameLib.colorIndex[4] = 14;
                                        GameLib.colors[4] = GameLib.allColors[GameLib.colorIndex[4]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[4]--;
                                        GameLib.colors[4] = GameLib.allColors[GameLib.colorIndex[4]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                                else if (newChar.Key == GameLib.controls[3])
                                {
                                    if (GameLib.colorIndex[4] == 14)
                                    {
                                        GameLib.colorIndex[4] = 0;
                                        GameLib.colors[4] = GameLib.allColors[GameLib.colorIndex[4]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[4]++;
                                        GameLib.colors[4] = GameLib.allColors[GameLib.colorIndex[4]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                            } while (newChar.Key != GameLib.controls[4]);
                            drawColorCursor(false);
                            break;
                        case (11):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                if (newChar.Key == GameLib.controls[2])
                                {
                                    if (GameLib.colorIndex[3] == 0)
                                    {
                                        GameLib.colorIndex[3] = 14;
                                        GameLib.colors[3] = GameLib.allColors[GameLib.colorIndex[3]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[3]--;
                                        GameLib.colors[3] = GameLib.allColors[GameLib.colorIndex[3]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                                else if (newChar.Key == GameLib.controls[3])
                                {
                                    if (GameLib.colorIndex[3] == 14)
                                    {
                                        GameLib.colorIndex[3] = 0;
                                        GameLib.colors[3] = GameLib.allColors[GameLib.colorIndex[3]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[3]++;
                                        GameLib.colors[3] = GameLib.allColors[GameLib.colorIndex[3]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                            } while (newChar.Key != GameLib.controls[4]);
                            drawColorCursor(false);
                            break;
                        case (9):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                if (newChar.Key == GameLib.controls[2])
                                {
                                    if (GameLib.colorIndex[2] == 0)
                                    {
                                        GameLib.colorIndex[2] = 14;
                                        GameLib.colors[2] = GameLib.allColors[GameLib.colorIndex[2]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[2]--;
                                        GameLib.colors[2] = GameLib.allColors[GameLib.colorIndex[2]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                                else if (newChar.Key == GameLib.controls[3])
                                {
                                    if (GameLib.colorIndex[2] == 14)
                                    {
                                        GameLib.colorIndex[2] = 0;
                                        GameLib.colors[2] = GameLib.allColors[GameLib.colorIndex[2]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[2]++;
                                        GameLib.colors[2] = GameLib.allColors[GameLib.colorIndex[2]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                            } while (newChar.Key != GameLib.controls[4]);
                            drawColorCursor(false);
                            break;
                        case (7):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                if (newChar.Key == GameLib.controls[2])
                                {
                                    if (GameLib.colorIndex[1] == 0)
                                    {
                                        GameLib.colorIndex[1] = 14;
                                        GameLib.colors[1] = GameLib.allColors[GameLib.colorIndex[1]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[1]--;
                                        GameLib.colors[1] = GameLib.allColors[GameLib.colorIndex[1]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                                else if (newChar.Key == GameLib.controls[3])
                                {
                                    if (GameLib.colorIndex[1] == 14)
                                    {
                                        GameLib.colorIndex[1] = 0;
                                        GameLib.colors[1] = GameLib.allColors[GameLib.colorIndex[1]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[1]++;
                                        GameLib.colors[1] = GameLib.allColors[GameLib.colorIndex[1]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                            } while (newChar.Key != GameLib.controls[4]);
                            drawColorCursor(false);
                            break;
                        case (5):
                            do
                            {
                                newChar = Console.ReadKey(true);
                                if (newChar.Key == GameLib.controls[2])
                                {
                                    if (GameLib.colorIndex[0] == 0)
                                    {
                                        GameLib.colorIndex[0] = 14;
                                        GameLib.colors[0] = GameLib.allColors[GameLib.colorIndex[0]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[0]--;
                                        GameLib.colors[0] = GameLib.allColors[GameLib.colorIndex[0]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                                else if (newChar.Key == GameLib.controls[3])
                                {
                                    if (GameLib.colorIndex[0] == 14)
                                    {
                                        GameLib.colorIndex[0] = 0;
                                        GameLib.colors[0] = GameLib.allColors[GameLib.colorIndex[0]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                    else
                                    {
                                        GameLib.colorIndex[0]++;
                                        GameLib.colors[0] = GameLib.allColors[GameLib.colorIndex[0]];
                                        Program.updateDisplay();
                                        drawColorCursor(true);
                                    }
                                }
                            } while (newChar.Key != GameLib.controls[4]);
                            drawColorCursor(false);
                            break;
                    }
                    #endregion
                }
                else if (Program.cursorOpPos.XPos == 10)
                {
                    switch (Program.cursorOpPos.YPos)
                    {
                        case (21):
                            GameLib.diff = GameLib.Difficulty.Easy;
                            break;
                        case (23):
                            GameLib.diff = GameLib.Difficulty.Normal;
                            break;
                        case (25):
                            GameLib.diff = GameLib.Difficulty.Hard;
                            break;
                        case (27):
                            GameLib.mysteryMode = !GameLib.mysteryMode;
                            break;
                        case (29):
                            GameLib.resetOptions();
                            break;
                    }
                    Program.updateDisplay();
                }
            }
            else if (control.Key == ConsoleKey.Escape)
            {
                GameLib.curState = GameLib.GameState.Menu;
                Program.updateDisplay();
            }
        }

        private void drawColorCursor(bool drawStyle)
        {
            //true to draw, false to erase
            if (drawStyle)
            {
                Console.SetCursorPosition(60, Program.cursorOpPos.YPos);
                Console.Write('>');
                Console.SetCursorPosition(75, Program.cursorOpPos.YPos);
                Console.Write('<');
            }
            else
            {
                Console.SetCursorPosition(60, Program.cursorOpPos.YPos);
                Console.Write(' ');
                Console.SetCursorPosition(75, Program.cursorOpPos.YPos);
                Console.Write(' ');
            }
        }
    }
}
