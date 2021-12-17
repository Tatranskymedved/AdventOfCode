using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day15 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day15\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            var cl = new ChitonLeaver();
            cl.Run(str);

            return 0;
        }
    }

    //Unproudly stolen from: https://github.com/JKolkman/AdventOfCode/blob/master/ACC2021/day15/Day15.cs
    class ChitonLeaver
    {
        private string[] str;
        private Node[,] grid;
        public void Run(string[] inputString)
        {
            str = inputString;
            grid = new Node[str.Length, str[0].Length];
            for (var i = 0; i < str.Length; i++)
            {
                for (var j = 0; j < str[i].Length; j++)
                {
                    var weight = int.Parse(str[i].ToCharArray()[j].ToString());
                    grid[i, j] = new Node(j, i, weight);
                }
            }

            var expandedGrid = new Node[grid.GetLength(0) * 5, grid.GetLength(1) * 5];
            for (var i = 0; i < str.Length; i++)
            {
                for (var j = 0; j < str[i].Length; j++)
                {
                    var gridSizeX = str[i].Length;
                    var gridSizeY = str.Length;
                    for (var k = 0; k < 5; k++)
                    {
                        var newX = j + k * gridSizeX;
                        for (var l = 0; l < 5; l++)
                        {
                            var input = int.Parse(str[i][j].ToString());
                            input += k;
                            input += l;
                            if (input > 9)
                            {
                                input -= 9;
                            }

                            var newY = i + l * gridSizeY;
                            expandedGrid[newY, newX] = new Node(newX, newY, input);
                        }
                    }
                }
            }
            Console.WriteLine();
            Console.Write(("(1) "));
            Tasks();
            grid = expandedGrid;
            Console.Write(("(2) "));
            Tasks();
        }

        private void Tasks()
        {
            var time = DateTime.UtcNow;
            grid[0, 0].Distance = 0;
            var queue = new Queue<(int, int)>();
            var visited = new HashSet<(int, int)>();

            var nodeDict = new Dictionary<(int, int), Node>
            {
                [(0, 0)] = grid[0, 0]
            };
            queue.Enqueue((0, 0));
            while (queue.TryDequeue(out var coords))
            {
                var n = nodeDict[coords];
                visited.Add(coords);
                nodeDict.Remove(coords);

                FindNeighbours(n).ToList().ForEach(x =>
                {
                    var nCoord = (x.Y, x.X);
                    if (visited.Contains(nCoord) || nodeDict.ContainsKey(nCoord))
                    {
                        if (x.Distance > x.Weight + n.Distance)
                        {
                            x.Distance = x.Weight + n.Distance;
                        }

                        if (n.Distance <= n.Weight + x.Distance) return;
                        n.Distance = n.Weight + x.Distance;
                    }
                    else
                    {
                        x.Distance = x.Weight + n.Distance;
                        nodeDict.Add((x.Y, x.X), x);
                        queue.Enqueue((x.Y, x.X));
                    }
                });
            }

            var finalNode = grid[grid.GetUpperBound(0), grid.GetUpperBound(1)];
            Console.WriteLine(finalNode.Distance + " : " + (DateTime.UtcNow - time));
        }

        private IEnumerable<Node> FindNeighbours(Node node)
        {
            var x = node.X;
            var y = node.Y;
            var neighbours = new List<Node>();

            if (x < grid.GetLength(1) - 1)
            {
                neighbours.Add(grid[y, x + 1]);
            }

            if (y < grid.GetLength(0) - 1)
            {
                neighbours.Add(grid[y + 1, x]);
            }

            return neighbours;
        }
    }

    internal class Node
    {
        public int Distance { get; set; }
        public readonly int Y;
        public readonly int X;

        public readonly int Weight;

        public Node(int x, int y, int weight)
        {
            X = x;
            Y = y;
            Weight = weight;
            Distance = int.MaxValue / 2;
        }
    }
}
