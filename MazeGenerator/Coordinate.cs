using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Coordinate
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public override string ToString()
        {
            return $"{XCoord}, {YCoord}";
        }
    }
}
