using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Maze
    {
        public int MazeID { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public string Size { get; set; }

        public Coordinate StartCoord { get; set; }
        public Coordinate EndCoord { get; set; }
        public Wall Walls { get; set; }

        public List<Coordinate> FullCoordList { get; set; }
    }
}
