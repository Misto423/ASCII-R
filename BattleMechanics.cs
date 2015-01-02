using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    public class BattleMechanics
    {
        bool defending = false, frame = true;
        public Timer EnemyAtk, playerAtk;
        Random r = new Random();
        EnemyData enemy;
        Coord<int, int> batCursor = new Coord<int, int>(50, 25);
        private string prev2 = "", prev1 = "", current = "";

        public void startBattle(EnemyData e)
        {
            enemy = e;
            //1 minute - speed
            //EnemyAtk = new Timer(20000 - (enemy.Speed * 250));
            //playerAtk = new Timer(30000 - (GameLib.player.Speed * 500));
            EnemyAtk = new Timer(2000);
            playerAtk = new Timer(5000);
            EnemyAtk.Enabled = true;
            playerAtk.Enabled = true;

            GameLib.mth.updateMusic();

            EnemyAtk.Elapsed += new ElapsedEventHandler(OnEnemyEvent);
            playerAtk.Elapsed += new ElapsedEventHandler(OnPlayerEvent);

            Console.Clear();
            drawBattleScreen();
        }

        public void endBattle(bool leveled, bool flee)
        {
            ConsoleKeyInfo control;

            EnemyAtk.Stop();
            playerAtk.Stop();

            if (flee == false)
            {
                //erase enemy
                for (int y = 20 - enemy.battleView.Length; y < 20; y++)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write("                             ");
                }

                Timer anim = new Timer(250);
                anim.Elapsed += new ElapsedEventHandler(victoryPose);
                anim.Start();

                displayEndBattle(leveled, flee);

                do
                {
                    control = Console.ReadKey(true);
                } while (control.Key != GameLib.controls[4]);

                anim.Stop();
            }
            else
            {
                displayEndBattle(false, flee);

                do
                {
                    control = Console.ReadKey(true);
                } while (control.Key != GameLib.controls[4]);
            }
            GameLib.curState = GameLib.GameState.Game_Adv;
            GameLib.mth.updateMusic();
            Program.updateDisplay();
        }

        private void victoryPose(object source, ElapsedEventArgs e)
        {
            frame = !frame;
            if (frame)
            {
                for (int y = 15; y < 20; y++)
                {
                    Console.SetCursorPosition(55, y);
                    Console.Write(GameLib.playerBattle[y - 15]);
                }
            }
            else
            {
                for (int y = 15; y < 20; y++)
                {
                    Console.SetCursorPosition(55, y);
                    Console.Write(GameLib.playerVictory[y - 15]);
                }
            }
        }

        private void OnEnemyEvent(object source, ElapsedEventArgs e)
        {
            if (GameLib.curState != GameLib.GameState.Battle)
            {
                EnemyAtk.Stop();
                return;
            }
            EnemyAtk.Stop();
            //enemy makes move here
            int atk = r.Next(1, 2);
            bool playerDead = false;
            if (atk == 1)
            {
                playerDead = attack(1, 2);
            }
            else
            {
                playerDead = attack(2, 2);
            }
            if (playerDead)
            {
                EnemyAtk.Stop();
                playerAtk.Stop();
                GameLib.curState = GameLib.GameState.GameOver;
                Program.updateDisplay();
            }
            EnemyAtk.Start();

        }

        private void OnPlayerEvent(object source, ElapsedEventArgs e)
        {
            //display player menu and make move
            //EnemyAtk.Stop();
            playerAtk.Stop();

            if (GameLib.curState != GameLib.GameState.Battle)
            {
                playerAtk.Stop();
                return;
            }

            defending = false;
            displayPlayMenu();

            int selection = 0;
            bool enemyDead = false;

            do
            {
                selection = getPlayerOption();
                if (GameLib.curState != GameLib.GameState.Battle)
                {
                    playerAtk.Stop();
                    return;
                }
            } while (selection == 0);
            clearMenu();
            //perform action
            switch (selection)
            {
                case 1:
                    enemyDead = attack(1, 1);
                    break;
                case 2:
                    enemyDead = attack(2, 1);
                    break;
                case 3: //defend
                    defending = true;
                    if (GameLib.player.CurrentHP < GameLib.player.HP)
                    {
                        if (GameLib.player.CurrentHP == GameLib.player.HP - 2)
                        {
                            GameLib.player.CurrentHP += 2;
                        }
                        else if (GameLib.player.CurrentHP == GameLib.player.HP - 1)
                        {
                            GameLib.player.CurrentHP += 1;
                        }
                        else
                        {
                            GameLib.player.CurrentHP += 3;
                        }
                        updateHP();
                    }
                    displayAction(3, 1, 0);
                    break;
                case 4: //flee
                    int flee = r.Next(1, 100);
                    if (flee > 45)
                    {
                        endBattle(false, true);
                    }
                    else
                    {
                        displayAction(4, 1, 0);
                    }
                    break;
            }
            if (enemyDead)
            {
                bool leveled = false;
                GameLib.player.Experience += enemy.Experience;
                if (GameLib.player.Experience >= GameLib.expChart[GameLib.player.Level - 1])
                {
                    GameLib.player.levelUp();
                    leveled = true;
                }
                endBattle(leveled, false);
            }
            else
            {
                //EnemyAtk.Start();
                playerAtk.Start();
            }
        }

        public void drawBattleScreen()
        {
            for (int x = 0; x < 80; x++)
            {
                Console.SetCursorPosition(x, 20);
                Console.Write('-');
            }
            for (int y = 21; y < 33; y++)
            {
                Console.SetCursorPosition(40, y);
                Console.Write('|');
            }
            for (int y = 15; y < 20; y++)
            {
                Console.SetCursorPosition(55, y);
                Console.Write(GameLib.playerBattle[y - 15]);
            }
            for (int y = 20 - enemy.battleView.Length; y < 20; y++)
            {
                Console.SetCursorPosition(5, y);
                Console.Write(enemy.battleView[y - (20 - enemy.battleView.Length)]);
            }
            updateHP();
        }

        private void updateHP()
        {
            Console.SetCursorPosition(55, 22);
            Console.Write("                            ");
            Console.SetCursorPosition(55, 22);
            Console.Write("HP:  " + GameLib.player.CurrentHP + "/" + GameLib.player.HP);
        }

        private void displayPlayMenu()
        {
            Console.SetCursorPosition(batCursor.XPos, batCursor.YPos);
            Console.Write("->");
            Console.SetCursorPosition(55, 25);
            Console.Write("Attack");
            Console.SetCursorPosition(55, 26);
            Console.Write("Magic");
            Console.SetCursorPosition(55, 27);
            Console.Write("Defend");
            Console.SetCursorPosition(55, 28);
            Console.Write("Flee");
        }

        private void clearMenu()
        {
            Console.SetCursorPosition(batCursor.XPos, batCursor.YPos);
            Console.Write("  ");
            Console.SetCursorPosition(55, 25);
            Console.Write("      ");
            Console.SetCursorPosition(55, 26);
            Console.Write("     ");
            Console.SetCursorPosition(55, 27);
            Console.Write("      ");
            Console.SetCursorPosition(55, 28);
            Console.Write("    ");
        }

        private int getPlayerOption()
        {
            ConsoleKeyInfo select;
            select = Console.ReadKey(true);
            int selection = 0;
            if (select.Key == GameLib.controls[0])
            {
                if (batCursor.YPos == 25)
                {
                    Console.SetCursorPosition(batCursor.XPos, batCursor.YPos);
                    Console.Write("  ");
                    batCursor.YPos = 28;
                }
                else
                {
                    Console.SetCursorPosition(batCursor.XPos, batCursor.YPos);
                    Console.Write("  ");
                    batCursor.YPos -= 1;
                }
                displayPlayMenu();
            }
            else if (select.Key == GameLib.controls[1])
            {
                if (batCursor.YPos == 28)
                {
                    Console.SetCursorPosition(batCursor.XPos, batCursor.YPos);
                    Console.Write("  ");
                    batCursor.YPos = 25;
                }
                else
                {
                    Console.SetCursorPosition(batCursor.XPos, batCursor.YPos);
                    Console.Write("  ");
                    batCursor.YPos += 1;
                }
                displayPlayMenu();
            }
            else if (select.Key == GameLib.controls[4])
            {
                GameLib.playSelectionTune();
                switch (batCursor.YPos)
                {
                    case 25:
                        selection = 1;
                        break;
                    case 26:
                        selection = 2;
                        break;
                    case 27:
                        selection = 3;
                        break;
                    case 28:
                        selection = 4;
                        break;
                }
            }
            return selection;
        }

        //1 for attack, 2 for magic
        //1 for player, 2 for enemy
        //return true if enemy or player dies.
        private bool attack(int type, int player)
        {
            bool death = false;

            if (player == 1)
            {
                if (type == 1)
                {
                    int dmg = r.Next(GameLib.player.Strength - 3, GameLib.player.Strength + 2);
                    if (dmg - enemy.Defense > 0)
                    {
                        enemy.CurrentHP -= (dmg - enemy.Defense);
                        displayAction(1, 1, (dmg - enemy.Defense));
                    }
                    else
                    {
                        displayAction(1, 1, 0);
                    }
                }
                else
                {
                    int dmg = r.Next(GameLib.player.Intelligence - 3, GameLib.player.Intelligence + 2);
                    if (dmg - enemy.Fortitude > 0)
                    {
                        enemy.CurrentHP -= (dmg - enemy.Fortitude);
                        displayAction(2, 1, (dmg - enemy.Fortitude));
                    }
                    else
                    {
                        displayAction(2, 1, 0);
                    }
                }
                if (enemy.CurrentHP <= 0)
                {
                    death = true;
                }
            }
            else
            {
                if (type == 1)
                {
                    int dmg = r.Next(enemy.Strength - 3, enemy.Strength + 2);
                    if (defending)
                    {
                        if (dmg - (GameLib.player.Defense + 5) > 0)
                        {
                            GameLib.player.CurrentHP -= (dmg - (GameLib.player.Defense + 5));
                            displayAction(1, 2, (dmg - (GameLib.player.Defense + 5)));
                        }
                        else
                        {
                            displayAction(1, 2, 0);
                        }
                    }
                    else
                    {
                        if (dmg - GameLib.player.Defense > 0)
                        {
                            GameLib.player.CurrentHP -= (dmg - GameLib.player.Defense);
                            displayAction(1, 2, (dmg - GameLib.player.Defense));
                        }
                        else
                        {
                            displayAction(1, 2, 0);
                        }
                    }
                }
                else
                {
                    int dmg = r.Next(enemy.Intelligence - 3, enemy.Intelligence + 2);
                    if (defending)
                    {
                        if (dmg - (GameLib.player.Fortitude + 5) > 0)
                        {
                            GameLib.player.CurrentHP -= (dmg - (GameLib.player.Fortitude + 5));
                            displayAction(2, 2, (dmg - (GameLib.player.Fortitude + 5)));
                        }
                        else
                        {
                            displayAction(2, 2, 0);
                        }
                    }
                    else
                    {
                        if (dmg - GameLib.player.Fortitude > 0)
                        {
                            GameLib.player.CurrentHP -= (dmg - GameLib.player.Fortitude);
                            displayAction(2, 2, (dmg - GameLib.player.Fortitude));
                        }
                        else
                        {
                            displayAction(2, 2, 0);
                        }
                    }
                }
                if (GameLib.player.CurrentHP <= 0)
                {
                    death = true;
                }
                updateHP();
            }
            return death;
        }

        //displays last action of enemy or player
        private void displayAction(int select, int attacker, int dmg)
        {
            Console.SetCursorPosition(3, 24);
            Console.Write("                                     ");
            Console.SetCursorPosition(3, 25);
            Console.Write("                                     ");
            Console.SetCursorPosition(3, 26);
            Console.Write("                                     ");

            switch (select)
            {
                case 1:
                    if (attacker == 1)
                    {
                        prev2 = prev1;
                        prev1 = current;
                        current = "Player Attacks - does " + dmg + " damage!";
                    }
                    else
                    {
                        prev2 = prev1;
                        prev1 = current;
                        current = enemy.name + " Attacks - does " + dmg + " damage!";
                    }
                    break;
                case 2:
                    if (attacker == 1)
                    {
                        prev2 = prev1;
                        prev1 = current;
                        current = "Player Uses Magic - does " + dmg + " damage!";
                    }
                    else
                    {
                        prev2 = prev1;
                        prev1 = current;
                        current = enemy.name + " Uses Magic - does " + dmg + " damage!";
                    }
                    break;
                case 3:
                    prev2 = prev1;
                    prev1 = current;
                    current = "Player Readied His Defenses!";
                    break;
                case 4:
                    prev2 = prev1;
                    prev1 = current;
                    current = "Player Failed to Flee...";
                    break;
            }

            Console.SetCursorPosition(3, 24);
            Console.Write(prev2);
            Console.SetCursorPosition(3, 25);
            Console.Write(prev1);
            Console.SetCursorPosition(3, 26);
            Console.Write(current);
        }

        private void displayEndBattle(bool leveled, bool flee)
        {
            if (flee == false)
            {
                Console.SetCursorPosition(3, 24);
                Console.Write("                                    ");
                Console.SetCursorPosition(3, 24);
                Console.Write("You Won!!");
                Console.SetCursorPosition(3, 25);
                Console.Write("You earned " + enemy.Experience + " experience!");
                if (leveled)
                {
                    Console.SetCursorPosition(3, 26);
                    Console.Write("LEVEL UP!  Player is now level " + GameLib.player.Level);
                }
            }
            else
            {
                Console.SetCursorPosition(3, 24);
                Console.Write("                                    ");
                Console.SetCursorPosition(3, 24);
                Console.Write("You ran away, you coward...");
                Console.SetCursorPosition(3, 25);
                Console.Write("Press Action Key.");
            }
        }
    }
}
