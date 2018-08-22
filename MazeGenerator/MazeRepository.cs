using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class MazeRepository
    {
        List<Maze> _mazeList = new List<Maze>();

        public List<Maze> GetMazes()
        {
            return _mazeList;
        }
    }
}
