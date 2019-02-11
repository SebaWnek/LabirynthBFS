using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirynthBFS
{
    class Program
    {
        static int[,] labirynth;
        static bool[,] visited;
        static int[] exitCell;
        static Queue<int[]> toCheck = new Queue<int[]>();
        static int[,] searchPattern;
        static bool exit = false;
        static List<int[]> path = new List<int[]>();

        static void Main(string[] args)
        {
            //declare test labirynth
            labirynth = new int[,] { { 1, -1, 0, 0, 0 }, { 0, -1, 0, -1, 0 }, { 0, -1, 0, -1, 0 }, { 0, -1, 0, 0, 0 }, { 0, 0, 0, -1, 0 } };
            exitCell = new int[] { 4, 4 };

            ////test labirynth
            //labirynth = new int[60, 60];
            //exitCell = new int[] { 49, 49 };

            //declare visited  matrix
            visited = new bool[labirynth.GetLength(0), labirynth.GetLength(1)];

            //declare search pattern
            searchPattern = new int[,] { { -1, 0, 1, 0 }, { 0, 1, 0, -1 } };

            int[] start = new int[] { 0, 0, 1 };

            //move unpassable to visited matrix
            MoveToVisited();

            //initialize search
            Search(start);

            //show path
            Print(labirynth);

            //backtrack
            FindPath(exitCell);

            //print path
            PrintPath(path);

        }

        //main searching function
        static void Search(int[] next)
        {
            int row = next[0];
            int col = next[1];
            int count = next[2];


            

            

            //write current steps
            labirynth[row, col] = count;

            //enqueue next ones
            for (int i = 0; i < searchPattern.GetLength(1); i++)
            {
                //check if inside matrix & empty
                if (row + searchPattern[0,i] >= 0 && row + searchPattern[0,i] < labirynth.GetLength(0) 
                    && col + searchPattern[1,i] >= 0 && col + searchPattern[1,i] < labirynth.GetLength(1)
                    && visited[row + searchPattern[0,i], col + searchPattern[1,i]] == false)
                {
                    //check if exit
                    if (row + searchPattern[0, i] == exitCell[0] && col + searchPattern[1, i] == exitCell[1])
                    {
                        Console.WriteLine("exit found at {0},{1} after {2} steps", exitCell[0], exitCell[1], count + 1);
                        exit = true;
                        labirynth[row + searchPattern[0, i], col + searchPattern[1, i]] = count + 1;
                        return;
                    }
                    
                    else
                    {
                        //write current steps
                        labirynth[row, col] = count;
                        //add neighbors to queue if empty and not checked
                        toCheck.Enqueue(new int[] { row + searchPattern[0, i], col + searchPattern[1, i], count + 1 });
                        //mark as visited
                        visited[row + searchPattern[0, i], col + searchPattern[1, i]] = true;
                        //Console.WriteLine();
                        //Print(labirynth);
                    }
                }
            }
            //go to next element in queue if queue not empty or exit not found
            if (toCheck.Count != 0 && exit == false)
            {
                Search(toCheck.Dequeue());
            }
            //if no exit found
            else if (toCheck.Count == 0 && exit == false)
            {
                Console.WriteLine("no exit was found");
            }


        }

        //printing matrix
        static void Print(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write("{0,3}", arr[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void MoveToVisited()
        {
            //go through all cells marking unpassable as true
            for (int i = 0; i < visited.GetLength(0); i++)
            {
                for (int j = 0; j < visited.GetLength(1); j++)
                {
                    if (labirynth[i,j] != 0)
                    {
                        visited[i, j] = true;
                    }
                }
            }
        }

        //backtracking shortest path
        static void FindPath(int[] start)
        {
            int row = start[0], col = start[1];
            //check if beggining
            if (labirynth[row, col] == 1)
            {
                path.Add(start);
                return;
            }

            //test neighbour cells
            for (int i = 0; i < searchPattern.GetLength(1); i++)
            {
                //check if inside matrix & empty
                if (row + searchPattern[0, i] >= 0 && row + searchPattern[0, i] < labirynth.GetLength(0)
                    && col + searchPattern[1, i] >= 0 && col + searchPattern[1, i] < labirynth.GetLength(1)
                    && labirynth[row + searchPattern[0, i], col + searchPattern[1, i]] == labirynth[row, col] - 1)
                {
                    path.Add(start);
                    FindPath(new int[] { row + searchPattern[0, i], col + searchPattern[1, i] });
                }
            }
        }

        static void PrintPath(List<int[]> li)
        {
            foreach (int[] l in li)
            {
                Console.WriteLine(l[0] + " " + l[1]);
            }
        }
    }
}
