using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace Algorithms.GCJ.APAC2017.Round1
{
    public class B
    {
        public static void Parse()
        {
            var inAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round1\BInputSmall.txt";
            var outAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round1\BOutputSmall.txt";
            var arr = File.ReadAllLines(inAddress);
            var ptr = 0;
            var cases = int.Parse(arr[ptr++]);

            using (var outFile = new StreamWriter(path: outAddress, append: false, encoding: System.Text.ASCIIEncoding.ASCII))
            {
                for (var caseNum = 1; caseNum <= cases; caseNum++)
                {
                    var ar = arr[ptr++].Split(' ').Select(int.Parse).ToList();
                    var R = ar[0];
                    var C = ar[1];
                    var grid = Enumerable.Range(0, R).Select(r => arr[ptr++].Split(' ').Select(int.Parse).ToArray()).ToArray();
                    outFile.WriteLine("Case #{0}: {1}", caseNum, Solve(R, C, grid));
                }
            }
        }

        // iterative: for each cell, adjust water level to be: lower of next.water and now.water. (is that all?) 
        // however water level cannot be lower than ground. so readjust water level = max now.ground, now.water ??

        // 2 solutions: Bellman-Ford variation or DFS pick lowest if next is lower, assgin next.water = now.water. if hit hole, 
        // dfs variables. highest: all the walls that u stop at, this is the min of all the higher walls. hasHitHole: break out to lowest level: the min of all the sides
        // logic: assign all to min(highest, lowest) 
        public static int Solve(int R, int C, int[][] grid)
        {
            var water = Enumerable.Range(0, R).Select(r => Enumerable.Range(0, C).Select(c => isBorder(r, c, R, C) ? grid[r][c] : int.MaxValue).ToArray()).ToArray();
            var allNonborderCells = Enumerable.Range(0, R).ToArray().SelectMany(r => Enumerable.Range(0, C).Where(c => !isBorder(r, c, R, C)).Select(c => new { x = r, y = c }));
            var directions = new int[][] { new [] { 0, 1 }, new [] { 1, 0 }, new [] { -1, 0 }, new [] { 0, -1 }};

            bool hasDoneUpdate;
            do
            {
                hasDoneUpdate = false;
                foreach (var cell in allNonborderCells)
                {
                    var minimum = int.MaxValue;
                    foreach (var direction in directions)
                    {
                        var nextX = cell.x + direction[0];
                        var nextY = cell.y + direction[1];
                        if (nextX < 0 || nextY < 0 || nextX >= R || nextY >= C) continue;
                        var nextBest = Math.Max(water[nextX][nextY], grid[nextX][nextY]); //IMPORTANT: minimum of all the next best (coz water can be lower than ground!) 2 2 2, 2 5 2, 2 2 2
                        // note: yes water can be lower than ground. imagine a high peak surrounded by water. then technically the water 'cuts' through lower body of peak 
                        minimum = Math.Min(minimum, nextBest);
                    }
                    if (minimum < water[cell.x][cell.y])
                    {
                        water[cell.x][cell.y] = minimum;// Math.Max(minimum, grid[cell.x][cell.y]); will not terminate if u do not let water be lower than ground 
                        hasDoneUpdate = true;
                    }
                }

            } while (hasDoneUpdate);

            return allNonborderCells.Sum(cell => Math.Max(0, water[cell.x][cell.y] - grid[cell.x][cell.y]));
            //return -1;
        }

        public static bool isBorder(int r, int c, int R, int C)
        {
            return r == 0 || c == 0 || r == R - 1 || c == C - 1;
        }


        
    }
}
