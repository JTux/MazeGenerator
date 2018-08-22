using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public enum CoordType { Start=1, Wall, Empty, End }

    class Coordinate
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public CoordType Type { get; set; }

        public override string ToString()
        {
            return $"{XCoord}, {YCoord}";
        }
    }
}
