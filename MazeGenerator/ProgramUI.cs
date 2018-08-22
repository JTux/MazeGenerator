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

            //The Width and Height are given default values that will be changed
            int mazeWidth = 5;
            int mazeHeight = 5;

            var sizeCheck = true;

            //Checks to make sure the width and height are numbers and are not smaller than 5
            while (sizeCheck)
            {
                Console.Write("Enter maze width: ");
                mazeWidth = ParseIntput();
                if (!(mazeWidth < 5)) break;
                else Console.WriteLine("Please enter a width greater than 4.");
            }
            while (sizeCheck)
            {
                Console.Write("Enter maze height: ");
                mazeHeight = ParseIntput();
                if (!(mazeHeight < 5)) break;
                else Console.WriteLine("Please enter a width greater than 4.");
            }

            //Uses the CreateCoord helper method to create Coordinates
            var newStart = CreateCoord("Enter Start Coordinate: ");
            var newEnd = CreateCoord("Enter End Coordinate: ");

            //Updates the mazecount so it can assign each maze a unique ID
            _mazeCount++;

            //Calls the method that sets a loop for users to create as many wall objects as they want
            var newWall = CreateWalls();

            //Creates the new Maze based on all the values the user has input thus far
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

            //Adds the new maze to the list
            _mazeList.Add(newMaze);
        }

        public void ViewMaze()
        {
            Console.Clear();
            ListMazes("Which maze would you like to view?");
            var seeMaze = Int32.Parse(Console.ReadLine());

            //Checks the list of Mazes for the id number the user entered and then prints it
            foreach (Maze maze in _mazeList)
            {
                Console.Clear();
                if (maze.MazeID == seeMaze)
                {
                    //Calls PrintMaze method and passes through the Maze it found that matches the user's request
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
            //Creates one row at a time, checking each column as it goes
            for (int r = 0; r < maze.Height; r++)
            {
                for (int c = 0; c < maze.Width; c++)
                {
                    Coordinate testCoord = new Coordinate
                    {
                        XCoord = r,
                        YCoord = c
                    };

                    //Checks if the current spot is a wall or not and assigns it the corresponding value
                    var result = sortedList.Find(x => x.XCoord == r && x.YCoord == c);
                    if (result != null)
                    {
                        Console.Write(" [] ");
                    }
                    else Console.Write("    ");
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

            //Loops until the user decides they are done creating walls
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
                    Console.Write("Would you like to add another wall? (Y/N): ");
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

        //Helper method that creates a new Coordinate object
        private Coordinate CreateCoord(string prompt)
        {
            Console.WriteLine(prompt);
            Console.Write("X: ");
            var newX = ParseIntput();

            Console.Write("Y: ");
            var newY = ParseIntput();

            var newCoord = new Coordinate
            {
                XCoord = newX,
                YCoord = newY
            };

            return newCoord;
        }

        //Helper method that prints the id number for each maze
        private void ListMazes(string prompt)
        {
            Console.WriteLine(prompt);
            foreach (Maze maze in _mazeList)
            {
                Console.WriteLine(maze.MazeID);
            }
        }

        //Helper method that makes sure input is a valid number
        private int ParseIntput()
        {
            int value;
            while (true)
            {
                var newXAsString = Console.ReadLine();
                var parse = Int32.TryParse(newXAsString, out value);
                if (parse) break;
                else Console.Write("Please enter a valid number: ");
            }
            return value;
        }
    }
}