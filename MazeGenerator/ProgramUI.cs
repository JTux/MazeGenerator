using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class ProgramUI
    {
        MazeRepository mazeRepo = new MazeRepository();
        List<Maze> mazeList = new List<Maze>();

        bool FirstTime = true;

        public void Run()
        {
            if (FirstTime) SetUp();
            Menu();
            Console.ReadLine();
        }

        private void Menu()
        {
            var running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine($"Options:\n" +
                    $"1) View Existing Mazes\n" +
                    $"2) Create New Maze\n" +
                    $"3) Edit Existing Maze\n" +
                    $"4) Delete Existing Maze\n" +
                    $"5) Exit");
                var response = mazeRepo.ParseIntput();

                switch (response)
                {
                    case 1:
                        mazeRepo.ViewMaze();
                        break;
                    case 2:
                        mazeRepo.CreateMaze();
                        break;
                    case 3:
                        mazeRepo.EditMaze();
                        break;
                    case 4:
                        mazeRepo.DeleteMaze();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                }
            }
        }

        private void SetUp()
        {
            mazeList = mazeRepo.GetMazeList();
            mazeRepo.SeedMazeList();
            FirstTime = false;
        }
    }
}