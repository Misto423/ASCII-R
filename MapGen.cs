using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    public class EnemyPlacement
    {
        public Coord<int, int> pos;
        public EnemyData enemy;

        public EnemyPlacement()
        {
            enemy = null;
            pos = new Coord<int, int>(-1, -1);
        }
    }

    public class Room
    {
        public Coord<int, int> topLeft;
        public Coord<int, int> botRight;
        private int quadrant;
        private EnemyPlacement rmEnemy;
        public Coord<int, int> lDoor, tDoor, rDoor, bDoor;
        public List<Coord<int, int>> doors;

        public Room()
        {
            topLeft = new Coord<int, int>(-1, -1);
            botRight = new Coord<int, int>(-1, -1);
            quadrant = -1;
            lDoor = new Coord<int, int>(-1, -1);
            tDoor = new Coord<int, int>(-1, -1);
            rDoor = new Coord<int, int>(-1, -1);
            bDoor = new Coord<int, int>(-1, -1);
            doors = new List<Coord<int, int>>();
            rmEnemy = new EnemyPlacement();
        }

        public Room(Coord<int, int> start, Coord<int, int> size)
        {
            this.botRight = size;
            this.topLeft = start;
            quadrant = -1;
            lDoor = new Coord<int, int>(-1, -1);
            tDoor = new Coord<int, int>(-1, -1);
            rDoor = new Coord<int, int>(-1, -1);
            bDoor = new Coord<int, int>(-1, -1);
            doors = new List<Coord<int, int>>();
            rmEnemy = new EnemyPlacement();
        }

        public int Quadrant
        {
            get { return quadrant; }
            set { quadrant = value; }
        }

        public EnemyPlacement Enemy
        {
            get { return rmEnemy; }
            set { rmEnemy = value; }
        }
    }

    class MapGen
    {
        private Random r;
        private List<Room> rary;
        private byte gridSize = 16;
        //public static List<AiRogueLike.EnemyInputClass> enemyAi = new List<AiRogueLike.EnemyInputClass>();

        public MapGen()
        {
            r = new Random();
        }

        public void generateRooms(int width, int height)
        {
            rary = new List<Room>();

            for (int xGrid = 0; xGrid <= width - gridSize; xGrid += gridSize)
            {
                for (int yGrid = 0; yGrid <= height - gridSize; yGrid += gridSize)
                {
                    int isRoom = r.Next(1, 8);
                    if (isRoom > 1)
                    {
                        int xLength = r.Next(4, gridSize - 1);
                        int yLength = r.Next(4, gridSize - 1);
                        int startx = r.Next(0, gridSize - xLength - 1);
                        int starty = r.Next(0, gridSize - yLength - 1);
                        Room temp = new Room(new Coord<int, int>(startx + xGrid, starty + yGrid),
                            new Coord<int, int>(startx + xLength + xGrid, starty + yLength + yGrid));
                        determineQuadrant(ref temp);
                        rary.Add(temp);
                    }
                    else
                    {
                        rary.Add(new Room());
                    }
                }
            }

            for (int n = 0; n < rary.Count; n++)
            {
                if (rary[n].topLeft.XPos != -1)
                {
                    for (int x = rary[n].topLeft.XPos; x <= rary[n].botRight.XPos; x++)
                    {
                        for (int y = rary[n].topLeft.YPos; y <= rary[n].botRight.YPos; y++)
                        {
                            if (x == rary[n].topLeft.XPos || y == rary[n].topLeft.YPos ||
                                x == rary[n].botRight.XPos || y == rary[n].botRight.YPos)
                            {
                                GameLib.map[y, x] = '#';
                            }
                            else
                            {
                                GameLib.map[y, x] = ' ';
                            }
                        }
                    }
                }
            }

            //place character in first room
            for (int i = 0; i < rary.Count; i++)
            {
                if (rary[i].Quadrant != -1)
                {
                    int x = r.Next(rary[i].topLeft.XPos + 1, rary[i].botRight.XPos - 1);
                    int y = r.Next(rary[i].topLeft.YPos + 1, rary[i].botRight.YPos - 1);
                    GameLib.playerPosition.XPos = y;
                    GameLib.playerPosition.YPos = x;
                    GameLib.playerScreenPosition.XPos = y;
                    GameLib.playerScreenPosition.YPos = x;
                    break;
                }
            }
        }

        public void generateHalls(int width, int height)
        {

            Room below = null, right = null;
            for (int i = 0; i < rary.Count - 1; i++)
            {

                int col = i / 16;

                if (rary[i].botRight.XPos != -1)
                {

                    for (int inc = 1; (((i + inc) / 16) == col) && (below == null); inc++)
                    {
                        if (rary[i + inc].topLeft.XPos != -1)
                        {
                            below = rary[i + inc];
                        }
                    }

                    if (below != null)
                    {
                        int nowDoorX = -1, nowDoorY = -1, thenDoorX = -1, thenDoorY = -1;

                        if (rary[i].bDoor.XPos == -1)
                        {
                            do
                            {
                                nowDoorX = rary[i].bDoor.XPos = r.Next(rary[i].topLeft.XPos + 1, rary[i].botRight.XPos - 1);
                                nowDoorY = rary[i].bDoor.YPos = rary[i].botRight.YPos;
                            } while ((nowDoorX == below.topLeft.XPos) || (nowDoorX == below.botRight.XPos));
                            rary[i].doors.Add(new Coord<int, int>(nowDoorX, nowDoorY));
                        }
                        else
                        {
                            nowDoorX = rary[i].bDoor.XPos;
                            nowDoorY = rary[i].bDoor.YPos;
                        }

                        if ((nowDoorX > below.topLeft.XPos) && (nowDoorX < below.botRight.XPos))
                        {
                            thenDoorX = below.tDoor.XPos = nowDoorX;
                            thenDoorY = below.tDoor.YPos = below.topLeft.YPos;
                        }
                        else if (nowDoorX < below.topLeft.XPos)
                        {
                            thenDoorX = below.lDoor.XPos = below.topLeft.XPos;
                            thenDoorY = below.lDoor.YPos = r.Next(below.topLeft.YPos + 1, below.botRight.YPos - 1);
                        }
                        else if (nowDoorX > below.botRight.XPos)
                        {
                            thenDoorX = below.rDoor.XPos = below.botRight.XPos;
                            thenDoorY = below.rDoor.YPos = r.Next(below.topLeft.YPos + 1, below.botRight.YPos - 1);
                        }

                        below.doors.Add(new Coord<int, int>(thenDoorX, thenDoorY));

                        for (int j = nowDoorY; j <= thenDoorY; j++)
                        {
                            GameLib.map[j, nowDoorX - 1] = '#';
                            GameLib.map[j, nowDoorX] = ' ';
                            GameLib.map[j, nowDoorX + 1] = '#';
                        }

                        if ((nowDoorX != thenDoorX) && (thenDoorX != -1))
                        {
                            GameLib.map[thenDoorY + 1, nowDoorX - 1] = '#';
                            GameLib.map[thenDoorY + 1, nowDoorX] = '#';
                            GameLib.map[thenDoorY + 1, nowDoorX + 1] = '#';

                            int leftest = Math.Min(thenDoorX, nowDoorX + 1);
                            int rightest = Math.Max(thenDoorX, nowDoorX - 1);

                            for (int j = rightest; j >= leftest; j--)
                            {
                                GameLib.map[thenDoorY - 1, j] = '#';
                                GameLib.map[thenDoorY, j] = ' ';
                                GameLib.map[thenDoorY + 1, j] = '#';
                            }
                        }

                    }
                }
                below = null;
            }
            for (int i = 0; i < rary.Count - 1; i++)
            {
                if (rary[i].botRight.XPos != -1)
                {
                    for (int inc = 16; ((i + inc) < 256) && (right == null); inc += 16)
                    {
                        if (rary[i + inc].topLeft.XPos != -1)
                        {
                            right = rary[i + inc];
                        }
                    }

                    if (right != null)
                    {
                        int nowDoorX = -1, nowDoorY = -1, thenDoorX = -1, thenDoorY = -1;

                        if (rary[i].rDoor.XPos == -1)
                        {
                            do
                            {
                                nowDoorX = rary[i].rDoor.XPos = rary[i].botRight.XPos;
                                nowDoorY = rary[i].rDoor.YPos = r.Next(rary[i].topLeft.YPos + 1, rary[i].botRight.YPos - 1);
                            } while ((nowDoorY == right.topLeft.YPos) || (nowDoorY == right.botRight.YPos));
                            rary[i].doors.Add(new Coord<int, int>(nowDoorX, nowDoorY));
                        }
                        else
                        {
                            nowDoorX = rary[i].rDoor.XPos;
                            nowDoorY = rary[i].rDoor.YPos;
                        }

                        if ((nowDoorY > right.topLeft.YPos) && (nowDoorY < right.botRight.YPos))
                        {
                            if (right.lDoor.XPos == -1)
                            {
                                thenDoorY = right.lDoor.YPos = nowDoorY;
                                thenDoorX = right.lDoor.XPos = right.topLeft.XPos;
                            }
                            else
                            {
                                thenDoorY = right.lDoor.YPos;
                                thenDoorX = right.lDoor.XPos;
                            }
                            right.doors.Add(new Coord<int, int>(thenDoorX, thenDoorY));
                        }
                        else if (nowDoorY < right.topLeft.YPos)
                        {
                            if (right.tDoor.YPos == -1)
                            {
                                thenDoorY = right.tDoor.YPos = right.topLeft.YPos;
                                thenDoorX = right.tDoor.XPos = r.Next(right.topLeft.XPos + 1, right.botRight.XPos - 1);
                            }
                            else
                            {
                                thenDoorY = right.tDoor.YPos;
                                thenDoorX = right.tDoor.XPos;
                            }
                            right.doors.Add(new Coord<int, int>(thenDoorX, thenDoorY));
                        }
                        else if (nowDoorY > right.botRight.YPos)
                        {
                            if (right.bDoor.YPos == -1)
                            {
                                thenDoorY = right.bDoor.YPos = right.botRight.YPos;
                                thenDoorX = right.bDoor.XPos = r.Next(right.topLeft.XPos + 1, right.botRight.XPos - 1);
                            }
                            else
                            {
                                thenDoorY = right.bDoor.YPos;
                                thenDoorX = right.bDoor.XPos;
                            }
                            right.doors.Add(new Coord<int, int>(thenDoorX, thenDoorY));
                        }

                        for (int j = nowDoorX; j <= thenDoorX; j++)
                        {
                            if (GameLib.map[nowDoorY - 1, j] != ' ')
                            {
                                GameLib.map[nowDoorY - 1, j] = '#';
                            }
                            GameLib.map[nowDoorY, j] = ' ';
                            if (GameLib.map[nowDoorY + 1, j] != ' ')
                            {
                                GameLib.map[nowDoorY + 1, j] = '#';
                            }
                        }

                        if ((nowDoorY != thenDoorY) && (thenDoorY != -1))
                        {
                            if (GameLib.map[nowDoorY - 1, thenDoorX + 1] != ' ')
                            {
                                GameLib.map[nowDoorY - 1, thenDoorX + 1] = '#';
                            }
                            if (GameLib.map[nowDoorY, thenDoorX + 1] != ' ')
                            {
                                GameLib.map[nowDoorY, thenDoorX + 1] = '#';
                            }
                            if (GameLib.map[nowDoorY + 1, thenDoorX + 1] != ' ')
                            {
                                GameLib.map[nowDoorY + 1, thenDoorX + 1] = '#';
                            }

                            int lowest = Math.Min(thenDoorY, nowDoorY + 1);
                            int highest = Math.Max(thenDoorY, nowDoorY - 1);

                            for (int j = highest; j >= lowest; j--)
                            {
                                if (GameLib.map[j, thenDoorX - 1] != ' ')
                                {
                                    GameLib.map[j, thenDoorX - 1] = '#';
                                }
                                GameLib.map[j, thenDoorX] = ' ';
                                if (GameLib.map[j, thenDoorX + 1] != ' ')
                                {
                                    GameLib.map[j, thenDoorX + 1] = '#';
                                }
                            }
                        }
                    }
                }
                right = null;
            }

            for (int i = 0; i <= rary.Count - 1; i++)
            {

                for (int j = 0; j <= rary[i].doors.Count - 1; j++)
                {
                    GameLib.map[rary[i].doors[j].YPos, rary[i].doors[j].XPos] = (char)(48+rary[i].Quadrant);
                }

            }
        }

        public int curQuadrant()
        {
            if (GameLib.playerPosition.XPos < 4 * gridSize && GameLib.playerPosition.YPos < 4 * gridSize)
            {
                return 0;
            }
            else if (GameLib.playerPosition.YPos >= (4 * gridSize) && GameLib.playerPosition.YPos < (8 * gridSize) &&
                GameLib.playerPosition.XPos < (4 * gridSize))
            {
                return 1;
            }
            else if (GameLib.playerPosition.YPos >= (8 * gridSize) && GameLib.playerPosition.YPos < (12 * gridSize) &&
               GameLib.playerPosition.XPos < (4 * gridSize))
            {
                return 2;
            }
            else if (GameLib.playerPosition.YPos >= (4 * gridSize) && GameLib.playerPosition.YPos < (8 * gridSize) &&
                GameLib.playerPosition.XPos >= (4 * gridSize) && GameLib.playerPosition.XPos < (8 * gridSize))
            {
                return 3;
            }
            else if (GameLib.playerPosition.YPos >= (8 * gridSize) && GameLib.playerPosition.YPos < (12 * gridSize) &&
                GameLib.playerPosition.XPos >= (4 * gridSize) && GameLib.playerPosition.XPos < (8 * gridSize))
            {
                return 4;
            }
            else if (GameLib.playerPosition.YPos < (4 * gridSize) &&
                GameLib.playerPosition.XPos >= (8 * gridSize) && GameLib.playerPosition.XPos < (12 * gridSize))
            {
                return 6;
            }
            else if (GameLib.playerPosition.YPos >= (4 * gridSize) && GameLib.playerPosition.YPos < (8 * gridSize) &&
                GameLib.playerPosition.XPos >= (8 * gridSize) && GameLib.playerPosition.XPos < (12 * gridSize))
            {
                return 5;
            }
            else if (GameLib.playerPosition.YPos >= (4 * gridSize) && GameLib.playerPosition.YPos < (8 * gridSize) &&
                GameLib.playerPosition.XPos >= 12 * gridSize && GameLib.playerPosition.XPos < 16 * gridSize)
            {
                return 7;
            }
            else if (GameLib.playerPosition.YPos >= (12 * gridSize) && GameLib.playerPosition.YPos < (16 * gridSize) &&
                GameLib.playerPosition.XPos < (4 * gridSize))
            {
                return 8;
            }
            else if (GameLib.playerPosition.YPos >= (12 * gridSize) && GameLib.playerPosition.YPos < (16 * gridSize) &&
                GameLib.playerPosition.XPos >= (4 * gridSize) && GameLib.playerPosition.XPos < (8 * gridSize))
            {
                return 9;
            }
            else if (GameLib.playerPosition.YPos < (4 * gridSize) &&
                GameLib.playerPosition.XPos >= (12 * gridSize) && GameLib.playerPosition.XPos < (16 * gridSize))
            {
                return 10;
            }
            else if (GameLib.playerPosition.YPos < (4 * gridSize) &&
                GameLib.playerPosition.XPos >= (4 * gridSize) && GameLib.playerPosition.XPos < (8 * gridSize))
            {
                return 11;
            }
            else if (GameLib.playerPosition.YPos >= (12 * gridSize) && GameLib.playerPosition.YPos < (16 * gridSize) &&
                GameLib.playerPosition.XPos >= (8 * gridSize) && GameLib.playerPosition.XPos < (12 * gridSize))
            {
                return 12;
            }
            else if (GameLib.playerPosition.YPos >= (12 * gridSize) && GameLib.playerPosition.YPos < (16 * gridSize) &&
                GameLib.playerPosition.XPos >= (12 * gridSize) && GameLib.playerPosition.XPos < (16 * gridSize))
            {
                return 13;
            }
            else if (GameLib.playerPosition.YPos >= (8 * gridSize) && GameLib.playerPosition.YPos < (12 * gridSize) &&
                GameLib.playerPosition.XPos >= (8 * gridSize) && GameLib.playerPosition.XPos < (12 * gridSize))
            {
                return 14;
            }
            else if (GameLib.playerPosition.YPos >= (8 * gridSize) && GameLib.playerPosition.YPos < (12 * gridSize) &&
               GameLib.playerPosition.XPos >= (12 * gridSize) && GameLib.playerPosition.XPos < (16 * gridSize))
            {
                return 15;
            }
            else
            {
                return 0;
            }
        }

        private void determineQuadrant(ref Room r)
        {
            if (r.topLeft.XPos < 4 * gridSize && r.topLeft.YPos < 4 * gridSize)
            {
                r.Quadrant = 0;
            }
            else if (r.topLeft.YPos >= (4 * gridSize) && r.topLeft.YPos < (8 * gridSize) &&
                r.topLeft.XPos < (4 * gridSize))
            {
                r.Quadrant = 1;
            }
            else if (r.topLeft.YPos >= (8 * gridSize) && r.topLeft.YPos < (12 * gridSize) &&
               r.topLeft.XPos < (4 * gridSize))
            {
                r.Quadrant = 2;
            }
            else if (r.topLeft.YPos >= (4 * gridSize) && r.topLeft.YPos < (8 * gridSize) &&
                r.topLeft.XPos >= (4 * gridSize) && r.topLeft.XPos < (8 * gridSize))
            {
                r.Quadrant = 3;
            }
            else if (r.topLeft.YPos >= (8 * gridSize) && r.topLeft.YPos < (12 * gridSize) &&
                r.topLeft.XPos >= (4 * gridSize) && r.topLeft.XPos < (8 * gridSize))
            {
                r.Quadrant = 4;
            }
            else if (r.topLeft.YPos < (4 * gridSize) &&
                r.topLeft.XPos >= (8 * gridSize) && r.topLeft.XPos < (12 * gridSize))
            {
                r.Quadrant = 6;
            }
            else if (r.topLeft.YPos >= (4 * gridSize) && r.topLeft.YPos < (8 * gridSize) &&
                r.topLeft.XPos >= (8 * gridSize) && r.topLeft.XPos < (12 * gridSize))
            {
                r.Quadrant = 5;
            }
            else if (r.topLeft.YPos >= (4 * gridSize) && r.topLeft.YPos < (8 * gridSize) &&
                r.topLeft.XPos >= 12 * gridSize && r.topLeft.XPos < 16 * gridSize)
            {
                r.Quadrant = 7;
            }
            else if (r.topLeft.YPos >= (12 * gridSize) && r.topLeft.YPos < (16 * gridSize) &&
                r.topLeft.XPos < (4 * gridSize))
            {
                r.Quadrant = 8;
            }
            else if (r.topLeft.YPos >= (12 * gridSize) && r.topLeft.YPos < (16 * gridSize) &&
                r.topLeft.XPos >= (4 * gridSize) && r.topLeft.XPos < (8 * gridSize))
            {
                r.Quadrant = 9;
            }
            else if (r.topLeft.YPos < (4 * gridSize) &&
                r.topLeft.XPos >= (12 * gridSize) && r.topLeft.XPos < (16 * gridSize))
            {
                r.Quadrant = 10;
            }
            else if (r.topLeft.YPos < (4 * gridSize) &&
                r.topLeft.XPos >= (4 * gridSize) && r.topLeft.XPos < (8 * gridSize))
            {
                r.Quadrant = 11;
            }
            else if (r.topLeft.YPos >= (12 * gridSize) && r.topLeft.YPos < (16 * gridSize) &&
                r.topLeft.XPos >= (8 * gridSize) && r.topLeft.XPos < (12 * gridSize))
            {
                r.Quadrant = 12;
            }
            else if (r.topLeft.YPos >= (12 * gridSize) && r.topLeft.YPos < (16 * gridSize) &&
                r.topLeft.XPos >= (12 * gridSize) && r.topLeft.XPos < (16 * gridSize))
            {
                r.Quadrant = 13;
            }
            else if (r.topLeft.YPos >= (8 * gridSize) && r.topLeft.YPos < (12 * gridSize) &&
                r.topLeft.XPos >= (8 * gridSize) && r.topLeft.XPos < (12 * gridSize))
            {
                r.Quadrant = 14;
            }
            else if (r.topLeft.YPos >= (8 * gridSize) && r.topLeft.YPos < (12 * gridSize) &&
                r.topLeft.XPos >= (12 * gridSize) && r.topLeft.XPos < (16 * gridSize))
            {
                r.Quadrant = 15;
            }
            else
            {
                r.Quadrant = 0;
            }
        }

        private List<Room> getRoomsInQuadrant(int q)
        {
            List<Room> ret = new List<Room>();

            for (int i = 0; i < rary.Count; i++)
            {
                if (rary[i].Quadrant == q)
                {
                    ret.Add(rary[i]);
                }
            }

            return ret;
        }

        public List<Room> getRoomsInView()
        {
            List<Room> viewedRooms = new List<Room>();

            for (int i = 0; i < rary.Count; i++)
            {
                if (rary[i].topLeft.XPos >= GameLib.screenPos.XPos && 
                    rary[i].topLeft.XPos < GameLib.screenPos.XPos + 64 &&
                    rary[i].topLeft.YPos >= GameLib.screenPos.YPos &&
                    rary[i].topLeft.YPos < GameLib.screenPos.YPos + 32)
                {
                    viewedRooms.Add(rary[i]);
                }
            }

            return viewedRooms;
        }

        public void genKey()
        {
            int x, y;
            //place first key in first room
            for (int i = 0; i < rary.Count; i++)
            {
                if (rary[i].Quadrant != -1)
                {
                    x = r.Next(rary[i].topLeft.XPos + 1, rary[i].botRight.XPos - 1);
                    y = r.Next(rary[i].topLeft.YPos + 1, rary[i].botRight.YPos - 1);

                    GameLib.map[y, x] = (char)(48);
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                    break;
                }
            }

            for (int k = 1; k < gridSize; k++)
            {
                List<Room> quadRooms = getRoomsInQuadrant(k);
                //get random room in that quadrent
                Room placeRoom = quadRooms[r.Next(0, quadRooms.Count - 1)];
                //get random placement in the room
                x = r.Next(placeRoom.topLeft.XPos + 1, placeRoom.botRight.XPos - 1);
                y = r.Next(placeRoom.topLeft.YPos + 1, placeRoom.botRight.YPos - 1);
                //place item
                if (k >= 1 && k <= 9)
                {
                    GameLib.map[y, x] = (char)(k + 48);
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                }
                else if (k == 10)
                {
                    GameLib.map[y, x] = 'A';
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                }
                else if (k == 11)
                {
                    GameLib.map[y, x] = 'B';
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                }
                else if (k == 12)
                {
                    GameLib.map[y, x] = 'C';
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                }
                else if (k == 13)
                {
                    GameLib.map[y, x] = 'D';
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                }
                else if (k == 14)
                {
                    GameLib.map[y, x] = 'E';
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                }
                else
                {
                    GameLib.map[y, x] = 'F';
                    GameLib.keyPositions.Add(new Coord<int, int>(x, y));
                }
            }
        }

        public void genAdvItems()
        {
            int x = 0, y = 0;
            Room placeRoom;
            List<Room> quadRooms;
            for (int i = 0; i < GameLib.weapons.Count; i++)
            {
                quadRooms = getRoomsInQuadrant(GameLib.weapons[i].Quadrant);
                do
                {
                    //get random room in that quadrent
                    placeRoom = quadRooms[r.Next(0, quadRooms.Count - 1)];
                    //get random placement in the room
                    getRand(ref x, ref y, placeRoom);
                } while (GameLib.map[y, x] >= '\x30' && GameLib.map[x, y] <= '\x39');
                GameLib.map[y, x] = GameLib.weapons[i].MapChar;
                GameLib.weapons[i].Location = new Coord<int, int>(y, x);
            }

            for (int i = 0; i < GameLib.mWeapons.Count; i++)
            {
                quadRooms = getRoomsInQuadrant(GameLib.mWeapons[i].Quadrant);
                do
                {
                    //get random room in that quadrent
                    placeRoom = quadRooms[r.Next(0, quadRooms.Count - 1)];
                    //get random placement in the room
                    getRand(ref x, ref y, placeRoom);
                } while (GameLib.map[y, x] >= '\x30' && GameLib.map[x, y] <= '\x39');
                GameLib.map[y, x] = GameLib.mWeapons[i].MapChar;
                GameLib.mWeapons[i].Location = new Coord<int, int>(y, x);
            }

            for (int i = 0; i < GameLib.armor.Count; i++)
            {
                quadRooms = getRoomsInQuadrant(GameLib.armor[i].Quadrant);
                do
                {
                    //get random room in that quadrent
                    placeRoom = quadRooms[r.Next(0, quadRooms.Count - 1)];
                    //get random placement in the room
                    getRand(ref x, ref y, placeRoom);
                } while (GameLib.map[y, x] >= '\x30' && GameLib.map[x, y] <= '\x39');
                GameLib.map[y, x] = GameLib.armor[i].MapChar;
                GameLib.armor[i].Location = new Coord<int, int>(y, x);
            }
            
        }

        private void getRand(ref int x, ref int y, Room placeRoom)
        {
            x = r.Next(placeRoom.topLeft.XPos + 1, placeRoom.botRight.XPos - 1);
            y = r.Next(placeRoom.topLeft.YPos + 1, placeRoom.botRight.YPos - 1);
        }

        //public void generateEnemyPositions()
        //{
        //    int x = 0, y = 0;
        //    int ran = r.Next(1, 100);
        //    EnemyPlacement ep;
        //    ai.Rooms = getRoomsInView();
        //    foreach (Room room in ai.Rooms)
        //    {
        //        getRand(ref x, ref y, room);
        //        ep = new EnemyPlacement();
        //        ep.enemy = ai.pickEnemy(room.Quadrant, ran);
        //        ep.pos.XPos = y;
        //        ep.pos.YPos = x;
        //        room.Enemy = ep;
        //    }
        //}

        //public void drawEnemies()
        //{
        //    foreach (Room room in ai.Rooms)
        //    {
        //        if (room.Enemy != null)
        //        {
        //            Console.ForegroundColor = GameLib.colors[2];
        //            Console.SetCursorPosition(room.Enemy.pos.XPos % 64, room.Enemy.pos.YPos % 32);
        //            Console.Write(room.Enemy.enemy.MapView);
        //            Console.ForegroundColor = GameLib.colors[0];
        //        }
        //    }
        //}

        //public void updateAI()
        //{
        //    ai.updateEnemyPos();
        //    drawEnemies();
        //}

        //public void GenerateEnemyPlacement()
        //{

        //    for (int i = 0; i < rary.Count; i++)
        //    {
        //        Room temp = rary[i];
        //        AiRogueLike.EnemyInputClass e = new AiRogueLike.EnemyInputClass(
        //                         new Coord<int, int>(temp.topLeft.XPos + (temp.botRight.XPos -
        //                     temp.topLeft.XPos) / 2, temp.botRight.YPos - (temp.botRight.YPos -
        //                     temp.topLeft.YPos) / 2), Program.HORZ, Program.VERT, Program.Player, temp.topLeft, temp.botRight);
        //        enemyAi.Add(e);
        //    }



        //}

        //public void LoadEnemyPlacement()
        //{
        //    for (int i = 0; i < enemyAi.Count; i++)
        //    {
        //        AiRogueLike.EnemyInputClass e = enemyAi[i];
        //        if (!e.Alive)
        //        {
        //            e.Destory();
        //            enemyAi.RemoveAt(i);

        //        }
        //        else
        //        {
        //            AiRogueLike.EnemyInputClass eic = new AiRogueLike.EnemyInputClass(e);
        //            enemyAi.RemoveAt(i);
        //            enemyAi.Add(eic);
        //        }
        //    }

        //}

        //public static void TurnOnAI()
        //{
        //    foreach (AiRogueLike.EnemyInputClass e in MapGen.enemyAi)
        //    {
        //        e.Start();
        //    }
        //}

        //public static void TurnOffAI()
        //{
        //    foreach (AiRogueLike.EnemyInputClass e in MapGen.enemyAi)
        //    {
        //        e.Stop();
        //    }
        //}

    }
}
