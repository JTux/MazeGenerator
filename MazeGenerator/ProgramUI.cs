using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class ProgramUI
    {
        private List<Maze> _mazeList = new List<Maze>();

        private int _mazeCount = 0;

        public void CreateMaze()
        {
            Console.Clear();

            Console.Write("Enter maze width: ");
            var mazeWidth = Int32.Parse(Console.ReadLine());

            Console.Write("Enter maze height: ");
            var mazeHeight = Int32.Parse(Console.ReadLine());

            var newStart = CreateCoord("Enter Start Coordinate: ");
            var newEnd = CreateCoord("Enter End Coordinate: ");

            _mazeCount++;

            var newWall = CreateWalls();

            var newMaze = new Maze
            {
                MazeID = _mazeCount,
                Width = mazeWidth,
                Height = mazeHeight,
                Size = $"{mazeWidth.ToString()} x {mazeHeight.ToString()}",
                StartCoord = newStart,
                EndCoord = newEnd,
                Walls = newWall
            };

            _mazeList.Add(newMaze);
        }

        public void ViewMaze()
        {
            Console.Clear();
            ListMazes("Which maze would you like to view?");
            var seeMaze = Int32.Parse(Console.ReadLine());

            foreach (Maze maze in _mazeList)
            {
                Console.Clear();
                if (maze.MazeID == seeMaze)
                {
                    PrintMaze(maze);
                    break;
                }
                else Console.WriteLine("Maze does not exist");
            }
        }

        public void PrintMaze(Maze maze)
        {
            List<Coordinate> sortedList = maze.Walls.WallCoords.OrderBy(s => s.XCoord).ThenBy(s => s.YCoord).ToList();
            foreach (Coordinate coord in sortedList)
            {
                Console.WriteLine(coord);
            }

            Console.Clear();
            for (int r = 0; r < maze.Height; r++)
            {
                var rowNum = r;
                for (int c = 0; c < maze.Width; c++)
                {
                    Coordinate testCoord = new Coordinate
                    {
                        XCoord = r,
                        YCoord = c
                    };

                    var result = sortedList.Find(x => x.XCoord == r && x.YCoord == c);
                    if (result != null)
                    {
                        if (result.ToString() == testCoord.ToString()) Console.Write(" X ");
                        else Console.Write(" - ");
                    }
                    else Console.Write(" + ");
                }
                Console.WriteLine();
            }
        }

        public void EditMaze()
        {
            Console.Clear();
            ListMazes("Which maze would you like to edit?");

        }

        private Wall CreateWalls()
        {
            var newWall = new Wall();
            var newWallList = new List<Coordinate>();

            Console.Clear();
            Console.WriteLine("Let's build a wall! #MakeMazesGreatAgain #NoRagrets");

            var creatingWalls = true;
            while (creatingWalls)
            {
                //Creates new Coordinate and adds it to the list
                var newCoord = CreateCoord("Enter position for wall: ");
                newWallList.Add(newCoord);

                //Check to add another wall coordinate
                var validResponse = true;
                while (validResponse)
                {
                    Console.Write("Would you like to add another wall? (Y/N)");
                    string response = Console.ReadLine().ToLower();

                    if (response == "n")
                    {
                        //Escapes out of adding walls to list
                        creatingWalls = false;
                        validResponse = false;
                    }
                    else if (response == "y")
                        //Returns you to creating walls
                        break;
                    else
                        Console.WriteLine("Invalid Input");
                }
            }

            //Sets new list to the list associated with the Wall
            newWall.WallCoords = newWallList;

            return newWall;
        }

        private Coordinate CreateCoord(string prompt)
        {
            Console.WriteLine(prompt);
            Console.Write("X: ");
            var newX = Int32.Parse(Console.ReadLine());
            Console.Write("Y: ");
            var newY = Int32.Parse(Console.ReadLine());

            var newCoord = new Coordinate
            {
                XCoord = newX,
                YCoord = newY
            };

            return newCoord;
        }

        private void ListMazes(string prompt)
        {
            Console.WriteLine(prompt);
            foreach (Maze maze in _mazeList)
            {
                Console.WriteLine(maze.MazeID);
            }
        }
    }
}
