using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day12 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day12\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-' };

            var ct = new CaveTraverser(str, charsToRemove);

            return ct.GetCountOfAllTraversals();
        }
    }

    class CaveTraverser
    {
        public Dictionary<string, List<string>> AdjacencyDict { get; set; } = new Dictionary<string, List<string>>();

        private Stack<string> path = new Stack<string>();
        public int Count { get; set; } = 0;
        public bool IsAnySmallTwice() => path.Where(a => a.ToLowerInvariant() == a).GroupBy(a => a).Select(a => a.Count()).Any(a => a > 1);

        public CaveTraverser(string[] str, char[] charsToRemove)
        {
            foreach (var line in str)
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);

                AddNodesAsEdge(row[0], row[1]);
            };
        }

        private void AddNodesAsEdge(string first, string second)
        {
            if (AdjacencyDict.TryGetValue(first, out List<string> list))
            {
                list.Add(second);
            }
            else
            {
                AdjacencyDict.Add(first, new List<string>() { second });
            }

            if (AdjacencyDict.TryGetValue(second, out List<string> list2))
            {
                list2.Add(first);
            }
            else
            {
                AdjacencyDict.Add(second, new List<string>() { first });
            }
        }

        public int GetCountOfAllTraversals()
        {
            Traverse("start");
            return Count;
        }

        private void Traverse(string v, int depth = 0)
        {
            path.Push(v);
            if (depth > 0 && v == "start")
            {
                return;
            }
            depth++;

            if (v == "end")
            {
                Count++;
                //var tmp = path.ToList();
                //tmp.Reverse();
                //Console.WriteLine(string.Join(",", tmp));
                return;
            }

            //if (depth > 10) return;

            var neighbours = AdjacencyDict[v];
            foreach (var neighbour in neighbours)
            {
                if (IsAnySmallTwice())
                {
                    if (neighbour.IsOneWay())
                    {
                        if (neighbour.WasAlreadyThere(1, path)) { }
                        else
                        {
                            Traverse(neighbour, depth);
                            path.Pop();
                        }
                    }
                    else
                    {
                        Traverse(neighbour, depth);
                        path.Pop();
                    }
                }
                else
                {
                    if (neighbour.IsOneWay())
                    {
                        if (neighbour.WasAlreadyThere(2, path)) { }
                        else
                        {
                            Traverse(neighbour, depth);
                            path.Pop();
                        }
                    }
                    else
                    {
                        Traverse(neighbour, depth);
                        path.Pop();
                    }
                }
            }
        }
    }

    public static class Helpers
    {
        public static bool WasAlreadyThere(this string Neighbour, int cnt, IEnumerable<string> previousPath) => previousPath.Count(a => a == Neighbour) >= cnt;
        public static bool IsOneWay(this string Neighbour) => Neighbour.ToLowerInvariant() == Neighbour;
    }
}
