using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCIIMazeCS;
using System.Threading;
//==================================//
//Changed Code Block Begin
//==================================//
//namespace AiRogueLike
//{
//    class EnemyInputClass : ASCIIMazeCS.InputClass
//    {
//        private InputClass inputClass;
//        private Coord<int, int> topLeft;
//        private Coord<int, int> bottomRight;
//        private Thread thread;
//        private bool alive = true;
//        private int movementSpeed = 500;
//        private EnemyData enemyData;
//        public EnemyInputClass(Coord<int, int> initPos, int MaxX, int MaxY, InputClass ic, Coord<int, int> topLeft,
//            Coord<int, int> bottomRight)
//            : base(initPos, MaxX, MaxY)
//        {
//            Random r = new Random(42);
            
//            this.enemyData = GameLib.enemies[r.Next(0, GameLib.enemies.Count)];
//            this.inputClass = ic;
//            this.topLeft = topLeft;
//            this.bottomRight = bottomRight;
//            CharacterSymbol = enemyData.MapView.ToString();
//            thread = new Thread(new ThreadStart(Run));
            
            
//            thread.Start();
//        }

//        public EnemyInputClass(EnemyInputClass e) : base(e.Position, e.MaxX, e.MaxY)
//        {
//            this.enemyData = e.EnemyData;
//            this.inputClass = e.InputClass;
//            this.topLeft = e.TopLeft;
//            this.bottomRight = e.BottomRight;
//            CharacterSymbol = "E";
//            thread = new Thread(new ThreadStart(Run));

//            if (Position.XPos < 60 && Position.YPos < 35 &&
//                Position.XPos > 0 && Position.YPos > 0)
//            {
//                Console.SetCursorPosition(Position.XPos, Position.YPos);
//                Console.ForegroundColor = GameLib.colors[2];
//                Console.Write(CharacterSymbol + " ");
//            }


//            thread.Start();
//        }

//        public void Start()
//        {
//           // if(!thread.IsAlive)
//           //     thread.Start();
//            if (Position.XPos < 60 && Position.YPos < 35 &&
//                Position.XPos > 0 && Position.YPos > 0)
//            {
//                Console.SetCursorPosition(Position.XPos, Position.YPos);
//                Console.ForegroundColor = GameLib.colors[2];
//                Console.Write(CharacterSymbol + " ");
//            }
//        }

//        public void Stop()
//        {
//            if(thread.IsAlive)
//            thread.Abort();
//        }

//        public override bool collisionDetect(char dir)
//        {
//            return base.collisionDetect(dir);
//        }

//        public void Run()
//        {
//            while (alive)
//            {
//                getInput();
//                if (Position.XPos == inputClass.Position.XPos &&
//                    Position.YPos == inputClass.Position.YPos)
//                {
//                    Console.Clear();
//                    BattleMechanics bm = new BattleMechanics();
//                    GameLib.curState = GameLib.GameState.Battle;
//                    bm.startBattle(this.enemyData);
//                    alive = false;
//                    Console.SetCursorPosition(Position.XPos, Position.YPos);
//                    Console.Write("X");
//                    this.Position = new Coord<int, int>(0, 0);
//                    break;
//                }
                
//                Thread.Sleep(movementSpeed);
//            }
//        }

//        public new void getInput()
//        {
//            //Player is in the boundries of the vincinity...
//            if ((this.inputClass.Position.XPos >= topLeft.XPos && this.inputClass.Position.XPos <=
//                bottomRight.XPos) && (this.inputClass.Position.YPos >= this.topLeft.YPos &&
//                this.inputClass.Position.YPos <= bottomRight.YPos))
//            {
                
//                // Going to the left...
//                if (inputClass.Position.XPos < this.Position.XPos)
//                {
//                    if (!collisionDetect('a'))
//                    {
//                        Position.XPos--;
//                        if (Position.XPos < 0)
//                            Position.XPos = 0;
//                        Console.SetCursorPosition(Position.XPos, Position.YPos);
//                        Console.ForegroundColor = GameLib.colors[2];
//                        Console.Write(CharacterSymbol + " ");
//                    }

                   
//                }
//                // Going down...
//                if (inputClass.Position.YPos > this.Position.YPos)
//                {
//                    if (!collisionDetect('s'))
//                    {
//                        Position.YPos++;
//                        if (Position.YPos > MaxY)
//                            Position.YPos = MaxY;
//                        Console.SetCursorPosition(Position.XPos, Position.YPos);
//                        Console.ForegroundColor = GameLib.colors[2];
//                        Console.Write(CharacterSymbol);
//                        //Position.YPos--;
//                        Console.SetCursorPosition(Position.XPos, Position.YPos - 1);
//                        Console.Write(" ");
//                        //Position.YPos++;
//                    }
                   
//                }

//                // Going right...
//                if (inputClass.Position.XPos > this.Position.XPos)
//                {
//                    if (!collisionDetect('d'))
//                    {
//                        Position.XPos++;
//                        if (Position.XPos > MaxX)
//                            Position.XPos = MaxX;
//                        Console.SetCursorPosition(Position.XPos - 1, Position.YPos);
//                        Console.ForegroundColor = GameLib.colors[2];
//                        Console.Write(" ");
//                        Console.SetCursorPosition(Position.XPos, Position.YPos);
//                        Console.Write(CharacterSymbol);
//                    }
                    
//                }
//                //Going up.
//                if (inputClass.Position.YPos < this.Position.YPos)
//                {
//                    if (!collisionDetect('w'))
//                    {
//                        Position.YPos--;
//                        if (Position.YPos < 0)
//                            Position.YPos = 0;
//                        Console.SetCursorPosition(Position.XPos, Position.YPos);
//                        Console.ForegroundColor = GameLib.colors[2];
//                        Console.Write(CharacterSymbol);
//                        Position.YPos++;
//                        Console.SetCursorPosition(Position.XPos, Position.YPos);
//                        Console.Write(" ");
//                        Position.YPos--;
//                    }
                    
//                }
//                else
//                {

//                }
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//        }

//        public void Destory()
//        {
//            thread.Abort();
//            this.Position = new Coord<int, int>(0, 0);
//            this.CharacterSymbol = " ";
//        }

//        public bool Alive
//        {
//            get { return alive; }
//        }

//        public InputClass InputClass
//        {
//            get { return inputClass; }
//        }

//        public Coord<int, int> TopLeft
//        {
//            get { return topLeft; }
//        }

//        public Coord<int, int> BottomRight
//        {
//            get { return bottomRight; }
//        }

//        public EnemyData EnemyData
//        {
//            get { return enemyData; }
//        }

        
//    }
//}

//==================================//
//Changed Code Block End
//==================================//
