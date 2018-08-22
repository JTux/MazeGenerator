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

        //-- This method is just used to have a hard coded maze ready to test
        public void SeedMazeList()
        {
            _mazeCount++;
            var newMaze = new Maze
            {
                MazeID = _mazeCount,
                Width = 6,
                Height = 6,
                Size = $"6 x 6",
                StartCoord = new Coordinate { XCoord = 1, YCoord = 0, Type = CoordType.Start },
                EndCoord = new Coordinate { XCoord = 2, YCoord = 3, Type = CoordType.End },
                Walls = new Wall
                {
                    WallCoords = new List<Coordinate>
                    {
                        new Coordinate{ XCoord = 0, YCoord = 0, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 0, YCoord = 1, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 0, YCoord = 2, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 1, YCoord = 2, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 1, YCoord = 4, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 2, YCoord = 0, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 2, YCoord = 2, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 2, YCoord = 4, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 3, YCoord = 0, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 3, YCoord = 2, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 3, YCoord = 3, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 3, YCoord = 4, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 4, YCoord = 0, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 5, YCoord = 0, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 5, YCoord = 2, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 5, YCoord = 3, Type = CoordType.Wall },
                        new Coordinate{ XCoord = 5, YCoord = 5, Type = CoordType.Wall },
                    }
                }
            };
            _mazeList.Add(newMaze);
        }

        public void CreateMaze()
        {
            Console.Clear();

            //-- The Width and Height are given default values that will be changed
            int mazeWidth = 5;
            int mazeHeight = 5;

            var sizeCheck = true;

            //-- Checks to make sure the width and height are numbers and are not smaller than 5
            while (sizeCheck)
            {
                Console.Write("Enter maze width: ");
                mazeWidth = ParseIntput();
                if (mazeWidth > 4) break;
                else Console.WriteLine("Please enter a width greater than 4.");
            }
            while (sizeCheck)
            {
                Console.Write("Enter maze height: ");
                mazeHeight = ParseIntput();
                if (mazeHeight > 4) break;
                else Console.WriteLine("Please enter a width greater than 4.");
            }

            //-- Uses the CreateCoord helper method to create Coordinates
            var newStart = CreateCoord("Enter Start Coordinate: ", mazeWidth, mazeHeight, CoordType.Start);

            Coordinate newEnd = new Coordinate();
            //-- Checks to make sure the user does not create an End point at the same place they created the Start
            while (true)
            {
                newEnd = CreateCoord("Enter End Coordinate: ", mazeWidth, mazeHeight, CoordType.End);
                if (newEnd.XCoord != newStart.XCoord || newEnd.YCoord != newStart.YCoord) break;
                Console.WriteLine($"Start and End points cannot be equal. Start located at: ({newStart}).");
                Console.ReadLine();
            }
            //-- Updates the mazecount so it can assign each maze a unique ID
            _mazeCount++;

            //-- Calls the method that sets a loop for users to create as many wall objects as they want
            var newWall = CreateWalls(mazeWidth, mazeHeight, newStart, newEnd);

            //-- Creates the new Maze based on all the values the user has input thus far
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

            //-- Adds the new maze to the list
            _mazeList.Add(newMaze);
        }

        //-- Method that can be called by the program to offer a list of mazes and then output the selected one
        public void ViewMaze()
        {
            var validInput = false;
            while (!validInput)
            {
                Console.Clear();
                ListMazes("Which maze would you like to view?");
                var seeMaze = ParseIntput();

                //-- Checks the list of Mazes for the id number the user entered and then prints it
                foreach (Maze maze in _mazeList)
                {
                    Console.Clear();
                    if (maze.MazeID == seeMaze)
                    {
                        //-- Calls PrintMaze method and passes through the Maze it found that matches the user's request
                        PrintMaze(maze);
                        validInput = true;
                        break;
                    }
                    else Console.WriteLine("Maze does not exist");
                }
                if (!validInput) Console.ReadLine();
            }
        }

        //-- This method is used to output a visual representation of the maze
        public void PrintMaze(Maze maze)
        {
            //-- Creates a list of the wall coordinates, along with adding the start and end points
            List<Coordinate> buildList = maze.Walls.WallCoords;
            buildList.Add(maze.StartCoord);
            buildList.Add(maze.EndCoord);

            //-- Sorts the new buildList so that it can output correctly
            List<Coordinate> sortedList = buildList.OrderBy(s => s.XCoord).ThenBy(s => s.YCoord).ToList();
            foreach (Coordinate coord in sortedList)
            {
                Console.WriteLine(coord);
            }

            Console.Clear();
            //-- Creates one row at a time, checking each column as it goes
            for (int r = 0; r < maze.Height; r++)
            {
                for (int c = 0; c < maze.Width; c++)
                {
                    Coordinate testCoord = new Coordinate
                    {
                        XCoord = r,
                        YCoord = c
                    };

                    //-- Checks the current spot and assigns it the appropriate value
                    var result = sortedList.Find(x => x.XCoord == c && x.YCoord == r);
                    if (result != null)
                    {
                        if (result.Type == CoordType.Start) Console.Write("SS");
                        else if (result.Type == CoordType.End) Console.Write("EE");
                        else Console.Write("[]");
                    }
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        //-- Eventually going to be used to edit the walls along with start and end points
        public void EditMaze()
        {
            Console.Clear();
            ListMazes("Which maze would you like to edit?");

        }

        //-- Method to create the list of walls that will be passed into the Maze
        private Wall CreateWalls(int width, int height, Coordinate start, Coordinate end)
        {
            var newWall = new Wall();
            var newWallList = new List<Coordinate>();

            Console.Clear();

            var prompt = $"Enter Wall Coordinates: \n" +
                         $"Start:({start})  End:({end})";

            //-- Loops until the user decides they are done creating walls
            var creatingWalls = true;
            while (creatingWalls)
            {
                //-- Creates new Coordinate and adds it to the list
                var newCoord = CreateCoord(prompt, width, height, CoordType.Wall);
                newWallList.Add(newCoord);

                //-- Check to add another wall coordinate
                var validResponse = true;
                while (validResponse)
                {
                    Console.Write("Would you like to add another wall? (Y/N): ");
                    string response = Console.ReadLine().ToLower();

                    if (response == "n")
                    {
                        //-- Escapes out of adding walls to list
                        creatingWalls = false;
                        validResponse = false;
                    }
                    else if (response == "y")
                        //-- Returns you to creating walls
                        break;
                    else
                        Console.WriteLine("Invalid Input");
                }
            }

            //-- Sets new list to the list associated with the Wall
            newWall.WallCoords = newWallList;

            return newWall;
        }

        //-- Helper method that creates a new Coordinate object
        private Coordinate CreateCoord(string prompt, int width, int height, CoordType type)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine($"Valid values: X: 0 - {width-1}  Y: 0 - {height-1}");
            int newX = 0;
            int newY = 0;

            //-- Checks to make sure the inputs are valid and within the designated grid area
            bool inCheck = false;
            while (!inCheck)
            {
                Console.Write("X: ");
                newX = ParseIntput();
                if (!(newX < 0 || newX >= width)) break;
                else Console.WriteLine("Invalid number. Outside of parameters.");
            }
            while (!inCheck)
            {
                Console.Write("Y: ");
                newY = ParseIntput();
                if (!(newY < 0 || newY >= height)) break;
                else Console.WriteLine("Invalid number. Outside of parameters.");
            }

            var newCoord = new Coordinate
            {
                XCoord = newX,
                YCoord = newY,
                Type = type
            };

            return newCoord;
        }

        //-- Helper method that prints the id number for each maze
        private void ListMazes(string prompt)
        {
            Console.WriteLine(prompt);
            foreach (Maze maze in _mazeList)
            {
                Console.WriteLine($"{maze.MazeID}: {maze.Size}");
            }
        }

        //-- Helper method that makes sure input is a valid integer
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

        //-- Currently not implemented

        ////-- Helper method that checks to make sure coordinates are not outside the designated grid area
        //private string CheckSize(Coordinate input, int width, int height)
        //{
        //    string output = "";

        //    if (input.XCoord > width || input.YCoord < 0) output = "Invalid width";
        //    if (input.YCoord > height || input.YCoord < 0) output = "";

        //    return output;
        //}
    }
}