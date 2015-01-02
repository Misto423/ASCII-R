using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    public class Coord<X, Y>
    {
        public Coord() { }

        public Coord(Coord<X, Y> c)
        {
            this.XPos = c.XPos;
            this.YPos = c.YPos;
        }

        public Coord(X x, Y y)
        {
            this.XPos = x;
            this.YPos = y;
        }

        public X XPos { get; set; }
        public Y YPos { get; set; }
    }
}
