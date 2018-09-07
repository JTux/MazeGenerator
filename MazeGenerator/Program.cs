using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramUI program = new ProgramUI();

            program.SeedMazeList();
            program.SeedMazeList();

            program.ViewMaze();
            Console.ReadLine();

            program.CreateMaze();

            program.ViewMaze();
            Console.ReadLine();
        }
    }
}
