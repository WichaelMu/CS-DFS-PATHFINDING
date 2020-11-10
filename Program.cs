using System;
using System.Collections.Generic;

namespace CS_DFS_PATHFINDING
{
    class Program
    {
        /// <summary> The map to transverse through. </summary>
        static char[,] map ={
            {' ', '*', ' ', ' ', ' ', '*', ' ', '*', ' ', '*', ' ', ' ', ' ', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', '*', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*'},
            {' ', '*', ' ', '*', ' ', '*', ' ', ' ', ' ', '*', ' ', '*', ' ', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', '*', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*'},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', '*', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', '*', ' ', ' ', ' ', ' '},
            {' ', '*', ' ', ' ', ' ', ' ', ' ', '*', ' ', '*', ' ', '*', ' ', '*'},
            {' ', '*', ' ', '*', ' ', '*', '*', '*', '*', '*', ' ', '*', ' ', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', ' ', ' ', ' ', ' ', '*', '*', ' '},
            {' ', '*', ' ', '*', ' ', '*', '*', ' ', '*', '*', ' ', '*', ' ', ' '},
            {' ', '*', ' ', '*', ' ', '*', '*', ' ', '*', '*', ' ', '*', ' ', '*'},
            {' ', '*', ' ', '*', ' ', '*', '*', ' ', '*', '*', ' ', ' ', ' ', ' '},
            {' ', '*', ' ', ' ', ' ', '*', '*', '*', '*', '*', ' ', '*', '*', ' '},
            {' ', '*', ' ', '*', ' ', '*', ' ', ' ', ' ', ' ', ' ', '*', ' ', ' '},
            {' ', ' ', ' ', '*', ' ', ' ', ' ', ' ', '*', ' ', ' ', '*', '*', 'e'},

        };
        /// <summary> The direction from the previous cell. </summary>
        static char[] path = new char[map.GetLength(0) * map.GetLength(1)];
        static int p = 0;
        /// <summary> The number of visited cells. </summary>
        static int visited = 0;
        /// <summary> The number of valid paths to the end. </summary>
        static int paths = 0;
        /// <summary> The directions to the end. </summary>
        static List<string> uniquePaths = new List<string>();

        static void Main(string[] args)
        {
            if (!ValidMap())
            {
                Console.WriteLine("There is no end.");
                return;
            }
            FindPath(0, 0, 'S');    //  Search begins at the top left (map[0, 0]).
            Console.WriteLine("\nVisited: " + visited + " cells.\nVisit percentage: " + (float)visited / ValidCells() + "%" + "\nFound " + paths + " path/s.");
            ShortestPath();
        }

        /// <summary>
        /// Checks int row and int col in direction char d for a valid position to reach the end.<br>Recursive Function.</br>
        /// </summary>
        /// <param name="row">The int row co-ordinate to check.</param>
        /// <param name="col">The int column co-ordinate to check.</param>
        /// <param name="d">The char direction to move to.</param>

        static void FindPath(int row, int col, char d)
        {
            if ((col < 0) || (row < 0) || (col >= map.GetLength(1)) || (row >= map.GetLength(0)))   //  If outside of the map.
                return;

            path[p] = d;
            p++;
            if (map[row, col] == 'e')   //  If the end is found.
            {
                AddPath(path, 1, p - 1);
                paths++;
            }

            if (map[row, col] == ' ')   //  If a valid path is found.
            {
                visited++;
                map[row, col] = 's';

                FindPath(row, col - 1, 'L');
                FindPath(row - 1, col, 'U');
                FindPath(row, col + 1, 'R');
                FindPath(row + 1, col, 'D');

                map[row, col] = ' ';
            }
            p--;
        }

        /// <summary>Adds this valid path.</summary>
        /// <param name="path">The char[] that denotes the path taken from the start to end from int sP to int eP.</param>
        /// <param name="sP">Adds the path taken from int sP start point.</param>
        /// <param name="eP">Adds the path taken from int eP end point.</param>

        static void AddPath(char[] path, int sP, int eP)
        {
            string a = "";
            for (int pos = sP; pos <= eP; pos++)
            {
                //Console.Write("Found Path to Exit: " + path[pos] + "\n");
                a += path[pos];
            }
            uniquePaths.Add(a);
        }

        /// <summary>Prints the shortest path to the end.</summary>

        static void ShortestPath()
        {
            int m = int.MaxValue;
            int k = 0;
            for (int i = 0; i < uniquePaths.Count; i++)
                if (uniquePaths[i].Length < m)
                {
                    m = uniquePaths[i].Length;
                    k = i;
                }

            if (uniquePaths.Count != 0)
                Console.WriteLine("Shortest Path: " + uniquePaths[k]);
        }

        /// <returns>The int number of valid cells that can be visited.</returns>

        static int ValidCells()
        {
            int validcells = 0;
            for (int i = 0; i < map.GetLength(0); i++)
                for (int k = 0; k < map.GetLength(1); k++)
                    if (map[i, k] == ' ')
                        validcells++;
            return validcells < 1
                ? 1
                : validcells;
        }

        static bool ValidMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int k = 0; k < map.GetLength(1); k++)
                    if (map[i, k] == 'e')
                        return true;
            return false;
        }
    }
}
