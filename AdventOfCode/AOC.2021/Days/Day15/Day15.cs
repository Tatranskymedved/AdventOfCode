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
            var charsToRemove = new char[] { ' ', ':', '-' };

            var values = str.Select(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                return row;
            });

            var cl = new ChitonLeaver(str, charsToRemove);

            return cl.GetMinSumOfTraversals();
        }
    }



    class ChitonLeaver
    {
        public Dictionary<ChitonNode, List<ChitonNode>> AdjacencyDict { get; set; } = new Dictionary<ChitonNode, List<ChitonNode>>();
        public ChitonNode LastNode { get; set; }

        private Stack<ChitonNode> path = new Stack<ChitonNode>();
        public double Sum { get; set; } = 0;
        public double MinSum { get; set; } = double.MaxValue;

        public ChitonLeaver(string[] str, char[] charsToRemove)
        {
            for (int y = 0; y < str.Length; y++)
            {
                var row = str[y];
                for (int x = 0; x < row.Length; x++)
                {
                    var value = Convert.ToInt32(row[x].ToString());
                    var curr = new ChitonNode(x, y, value);

                    if (x < str.First().Length - 1)
                        AddNodesAsEdge(curr, new ChitonNode(x + 1, y, Convert.ToInt32(str[y][x + 1].ToString())));
                    if (y < str.Length - 1)
                        AddNodesAsEdge(curr, new ChitonNode(x, y + 1, Convert.ToInt32(str[y + 1][x].ToString())));
                }
            }

            int lastX = AdjacencyDict.Max(a => a.Key.X);
            int lastY = AdjacencyDict.Max(a => a.Key.Y);
            LastNode = new ChitonNode(lastX, lastY, Convert.ToInt32(str[lastY][lastX].ToString()));
            AdjacencyDict.Add(LastNode, new List<ChitonNode>());
        }

        private void AddNodesAsEdge(ChitonNode first, ChitonNode second)
        {
            if (AdjacencyDict.TryGetValue(first, out var list))
            {
                list.Add(second);
            }
            else
            {
                AdjacencyDict.Add(first, new List<ChitonNode>() { second });
            }

            //if (AdjacencyDict.TryGetValue(second, out var list2))
            //{
            //    list2.Add(first);
            //}
            //else
            //{
            //    AdjacencyDict.Add(second, new List<ChitonNode>() { first });
            //}
        }

        public double GetMinSumOfTraversals()
        {
            Traverse(AdjacencyDict.First(a => a.Key.X == 0 && a.Key.Y == 0).Key);
            return MinSum;
        }

        private void Traverse(ChitonNode v, int depth = 0)
        {
            path.Push(v);
            depth++;

            //if ((new System.Diagnostics.StackTrace()).FrameCount > 1000) return;
            if (v.Equals(LastNode))
            {   
                //var tmp = path.ToList();
                Sum = path.Sum(a => Convert.ToDouble(a.Value)); ;
                Sum -= AdjacencyDict.First(a => a.Key.X == 0 && a.Key.Y == 0).Key.Value;

                if (MinSum > Sum)
                {
                    MinSum = Sum;
                    Console.WriteLine(MinSum);
                }

                //tmp.Reverse();
                //Console.WriteLine(string.Join(",", tmp));

                return;
            }

            //if (depth > 10) return;

            var neighbours = AdjacencyDict[v];
            foreach (var neighbour in neighbours)
            {
                Traverse(neighbour, depth);
                path.Pop();
            }
        }
    }


    class ChitonNode : IEquatable<ChitonNode>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }

        public ChitonNode(int x, int y, int value)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }

        public override int GetHashCode()
        {
            return X * 8243 + Y * 6607 + Value * 1871;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ChitonNode);
        }

        public override string ToString()
        {
            return $"{X},{Y}: {Value}";
        }

        public bool Equals(ChitonNode other)
        {
            if (other == null) return false;

            return other.GetHashCode() == this.GetHashCode();
        }
    }
}
