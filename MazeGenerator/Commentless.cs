using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Commentless
    {
        private List<Maze> _mazeList = new List<Maze>();
        private List<Coordinate> _fullList = new List<Coordinate>();

        private int _mazeCount = 0;

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
            newMaze.FullCoordList = CreateFullCoordList(newMaze.Walls, newMaze.StartCoord, newMaze.EndCoord, newMaze.Width, newMaze.Height);
            newMaze.FullCoordList = AssignValue(newMaze);
            _mazeList.Add(newMaze);
        }

        public void CreateMaze()
        {
            Console.Clear();

            int mazeWidth = 5;
            int mazeHeight = 5;

            while (true)
            {
                Console.Write("Enter maze width: ");
                mazeWidth = ParseIntput();
                if (mazeWidth > 4) break;
                else Console.WriteLine("Please enter a width greater than 4.");
            }
            while (true)
            {
                Console.Write("Enter maze height: ");
                mazeHeight = ParseIntput();
                if (mazeHeight > 4) break;
                else Console.WriteLine("Please enter a width greater than 4.");
            }

            var newStart = CreateCoord("Enter Start Coordinate: ", mazeWidth, mazeHeight, CoordType.Start);

            Coordinate newEnd = new Coordinate();
            while (true)
            {
                newEnd = CreateCoord("Enter End Coordinate: ", mazeWidth, mazeHeight, CoordType.End);
                if (newEnd.XCoord != newStart.XCoord || newEnd.YCoord != newStart.YCoord) break;
                Console.WriteLine($"Start and End points cannot be equal. Start located at: ({newStart}).");
                Console.ReadLine();
            }

            _mazeCount++;

            var newWall = CreateWalls(mazeWidth, mazeHeight, newStart, newEnd);

            var fullList = CreateFullCoordList(newWall, newStart, newEnd, mazeWidth, mazeHeight);

            var newMaze = new Maze
            {
                MazeID = _mazeCount,
                Width = mazeWidth,
                Height = mazeHeight,
                Size = $"{mazeWidth.ToString()} x {mazeHeight.ToString()}",
                StartCoord = newStart,
                EndCoord = newEnd,
                Walls = newWall,
                FullCoordList = fullList
            };

            AssignValue(newMaze);

            _mazeList.Add(newMaze);
        }

        public void ViewMaze()
        {
            var validInput = false;
            while (!validInput)
            {
                Console.Clear();
                ListMazes("Which maze would you like to view?");

                var seeMaze = ParseIntput();

                foreach (Maze maze in _mazeList)
                {
                    Console.Clear();
                    if (maze.MazeID == seeMaze)
                    {
                        PrintMaze(maze);
                        validInput = true;
                        break;
                    }
                    else Console.WriteLine("Maze does not exist");
                }
                if (!validInput) Console.ReadLine();
            }
        }

        public void EditMaze()
        {
            Console.Clear();
            ListMazes("Which maze would you like to edit?");
        }

        private void PrintMaze(Maze maze)
        {
            Console.Clear();

            for (int r = 0; r < maze.Height; r++)
            {
                int c = 0;
                c++;

                List<Coordinate> rowList = maze.FullCoordList.Where(e => e.YCoord == r).ToList();
                foreach (Coordinate coordinate in rowList)
                {
                    if (coordinate.Type == CoordType.Start) Console.Write("SS");
                    else if (coordinate.Type == CoordType.End) Console.Write("EE");
                    else if (coordinate.Type == CoordType.Wall) Console.Write("[]");
                    else if (coordinate.Value > 9) Console.Write(coordinate.Value);
                    else Console.Write(coordinate.Value + " ");
                }

                Console.WriteLine();
            }
        }

        private Coordinate CreateCoord(string prompt, int width, int height, CoordType type)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            Console.WriteLine($"Valid values: X: 0 - {width - 1}  Y: 0 - {height - 1}");
            int newX = 0;
            int newY = 0;

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

            return new Coordinate { XCoord = newX, YCoord = newY, Type = type };
        }

        private Wall CreateWalls(int width, int height, Coordinate start, Coordinate end)
        {
            Console.Clear();

            var newWall = new Wall();
            var newWallList = new List<Coordinate>();

            var prompt = $"Enter Wall Coordinates: \n" +
                         $"Start: ({start.XCoord}, {start.YCoord})  End: ({end.XCoord}, {end.YCoord}) \n" +
                         $"Current walls: ";

            Console.WriteLine();

            var creatingWalls = true;
            while (creatingWalls)
            {
                Coordinate newCoord = new Coordinate();

                while (true)
                {
                    newCoord = CreateCoord(prompt, width, height, CoordType.Wall);

                    var result = newWallList.Find(x => x.XCoord == newCoord.XCoord && x.YCoord == newCoord.YCoord);

                    if ((newCoord.XCoord != start.XCoord || newCoord.YCoord != start.YCoord) &&
                        (newCoord.XCoord != end.XCoord || newCoord.YCoord != end.YCoord) &&
                        (result == null))
                        break;
                    Console.WriteLine($"Cannot place wall because this coordinate is already taken.");
                    Console.ReadLine();
                }

                //TODO: Sort this list and remove the extra data (type and value)
                prompt = $"{prompt}({newCoord.XCoord}, {newCoord.YCoord}) ";
                newWallList.Add(newCoord);

                while (true)
                {
                    Console.Write("Would you like to add another wall? (Y/N): ");
                    string response = Console.ReadLine().ToLower();

                    if (response == "n")
                    {
                        creatingWalls = false;
                        break;
                    }
                    else if (response == "y")
                        break;
                    else
                        Console.WriteLine("Invalid Input");
                }
            }

            newWall.WallCoords = newWallList;

            return newWall;
        }

        private List<Coordinate> CreateFullCoordList(Wall wall, Coordinate start, Coordinate end, int width, int height)
        {
            var newCoordList = new List<Coordinate>();

            newCoordList.Add(start);
            newCoordList.Add(end);
            foreach (Coordinate coordinate in wall.WallCoords)
            {
                newCoordList.Add(coordinate);
            }

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    Coordinate testCoord = new Coordinate
                    {
                        XCoord = c,
                        YCoord = r,
                    };

                    var result = newCoordList.Find(x => x.XCoord == c && x.YCoord == r);
                    if (result == null)
                    {
                        testCoord.Type = CoordType.Empty;
                        newCoordList.Add(testCoord);
                    }
                }
            }

            List<Coordinate> sortedList = newCoordList.OrderBy(s => s.XCoord).ThenBy(s => s.YCoord).ToList();

            return sortedList;
        }

        private List<Coordinate> FindNeighbors(Maze maze, Coordinate coordinate, int value, List<Coordinate> usedCoords)
        {
            List<Coordinate> neighbors = new List<Coordinate>();

            Coordinate topCoord;
            Coordinate rightCoord;
            Coordinate bottomCoord;
            Coordinate leftCoord;

            if (coordinate.XCoord > 0 && !(usedCoords.Contains(coordinate)))
                neighbors.Add(leftCoord = new Coordinate { XCoord = (coordinate.XCoord - 1), YCoord = coordinate.YCoord, Value = value });
            if (coordinate.XCoord < (maze.Width - 1) && !(usedCoords.Contains(coordinate)))
                neighbors.Add(rightCoord = new Coordinate { XCoord = (coordinate.XCoord + 1), YCoord = coordinate.YCoord, Value = value });
            if (coordinate.YCoord > 0 && !(usedCoords.Contains(coordinate)))
                neighbors.Add(topCoord = new Coordinate { XCoord = coordinate.XCoord, YCoord = (coordinate.YCoord - 1), Value = value });
            if (coordinate.YCoord < (maze.Height - 1) && !(usedCoords.Contains(coordinate)))
                neighbors.Add(bottomCoord = new Coordinate { XCoord = coordinate.XCoord, YCoord = (coordinate.YCoord + 1), Value = value });

            return neighbors;
        }

        private List<Coordinate> AssignValue(Maze maze)
        {
            var completeList = maze.FullCoordList;
            var usedCoords = new List<Coordinate>();
            usedCoords.Add(maze.StartCoord);
            var newNeighbors = new List<Coordinate>();
            var nextNeighbors = new List<Coordinate>();

            var value = 0;
            var neighbors = FindNeighbors(maze, maze.EndCoord, value, usedCoords);

            var emptySpacesCount = completeList.Count(c => c.Type == CoordType.Empty);

            for (int i = 0; i < emptySpacesCount; i++)
            {
                value++;
                foreach (Coordinate neighbor in neighbors)
                {
                    var currentCoord = completeList.Find(x => x.XCoord == neighbor.XCoord && x.YCoord == neighbor.YCoord);
                    var checkCoord = (usedCoords.Find(c => c.XCoord == currentCoord.XCoord && c.YCoord == currentCoord.YCoord));
                    if (checkCoord == null)
                    {
                        if (currentCoord != null && currentCoord.Type == CoordType.Empty)
                        {
                            currentCoord.Value = value;
                            nextNeighbors = FindNeighbors(maze, neighbor, value, usedCoords);

                            foreach (Coordinate nextNeighbor in nextNeighbors)
                            {
                                newNeighbors.Add(nextNeighbor);
                            }

                            usedCoords.Add(neighbor);
                        }
                    }
                }

                neighbors.Clear();

                foreach (Coordinate newNeighbor in newNeighbors)
                    neighbors.Add(newNeighbor);

                newNeighbors.Clear();
            }

            return completeList;
        }

        private void ListMazes(string prompt)
        {
            Console.WriteLine(prompt);
            foreach (Maze maze in _mazeList)
            {
                Console.WriteLine($"{maze.MazeID}: {maze.Size}");
            }
        }

        private int ParseIntput()
        {
            int value;
            while (true)
            {
                var newXAsString = Console.ReadLine();
                var parse = Int32.TryParse(newXAsString, out value);
                if (parse) break;
                else Console.Write("Please enter a valid integer: ");
            }
            return value;
        }
    }
}