using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ASCIIMazeCS
{
    public class MusicThread
    {
        Thread mthread;

        public MusicThread()
        {
            updateMusic();
        }

        public void updateMusic()
        {
            switch (GameLib.curState)
            {
                case GameLib.GameState.Battle:
                    if (mthread != null)
                    {
                        stopMusic();
                    }
                    mthread = new Thread(new ThreadStart(playBattleTheme));
                    startMusic();
                    break;
                case GameLib.GameState.Game_Adv:
                case GameLib.GameState.Game_Sth:
                case GameLib.GameState.Game_Sur:
                    if (mthread != null)
                    {
                        stopMusic();
                    }
                    mthread = new Thread(new ThreadStart(playMainTheme));
                    startMusic();
                    break;
                case GameLib.GameState.GameOver:
                    if (mthread != null)
                    {
                        stopMusic();
                    }
                    mthread = new Thread(new ThreadStart(playGameOverTheme));
                    startMusic();
                    break;
                case GameLib.GameState.Menu:
                case GameLib.GameState.ClassChooser:
                case GameLib.GameState.Options:
                    if (mthread != null)
                    {
                        stopMusic();
                    }
                    mthread = new Thread(new ThreadStart(playMenuTheme));
                    startMusic();
                    break;
            }
        }

        private void playBattleTheme()
        {
            while (true)
            {
                Console.Beep((int)GameLib.Tone.Fsharp, (int)GameLib.Duration.EIGHTH);
                Console.Beep((int)GameLib.Tone.Fsharp, (int)GameLib.Duration.EIGHTH);
                Console.Beep((int)GameLib.Tone.A, (int)GameLib.Duration.EIGHTH);
                Console.Beep((int)GameLib.Tone.B, (int)GameLib.Duration.EIGHTH);
                Console.Beep((int)GameLib.Tone.A, (int)GameLib.Duration.EIGHTH);
                Console.Beep((int)GameLib.Tone.D, (int)GameLib.Duration.EIGHTH);
                Console.Beep((int)GameLib.Tone.Fsharp, (int)GameLib.Duration.EIGHTH);
                Console.Beep((int)GameLib.Tone.D, (int)GameLib.Duration.EIGHTH);
            }
        }

        private void playMenuTheme()
        {
            while (true)
            {
                Console.Beep((int)GameLib.Tone.Dsharp, (int)GameLib.Duration.QUARTER);
                Console.Beep((int)GameLib.Tone.E, (int)GameLib.Duration.QUARTER);
                Console.Beep((int)GameLib.Tone.A, (int)GameLib.Duration.QUARTER);
                Console.Beep((int)GameLib.Tone.G, (int)GameLib.Duration.QUARTER);
            }
        }

        private void playGameOverTheme()
        {
            while (true)
            {

            }
        }

        private void playMainTheme()
        {
            while (true)
            {
                Console.Beep((int)GameLib.Tone.C, (int)GameLib.Duration.HALF);
                Console.Beep((int)GameLib.Tone.Asharp, (int)GameLib.Duration.QUARTER);
                Console.Beep((int)GameLib.Tone.F, (int)GameLib.Duration.HALF);
                Console.Beep((int)GameLib.Tone.G, (int)GameLib.Duration.QUARTER);

                Console.Beep((int)GameLib.Tone.G, (int)GameLib.Duration.HALF);
                Console.Beep((int)GameLib.Tone.E, (int)GameLib.Duration.QUARTER);
                Console.Beep((int)GameLib.Tone.C, (int)GameLib.Duration.HALF);
                Console.Beep((int)GameLib.Tone.D, (int)GameLib.Duration.HALF);
            }
        }

        public void startMusic()
        {
            mthread.Start();
            while (!mthread.IsAlive) ;
        }

        public void stopMusic()
        {
            mthread.Abort();
            mthread.Join();
        }
    }
}
