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
            var path = @".\Days\Day15\input2.txt";

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
        public ChitonNode FirstNode { get; set; }
        public ChitonNode LastNode { get; set; }

        private List<ChitonNode> nodes = new List<ChitonNode>();

        private Stack<ChitonNode> path = new Stack<ChitonNode>();
        public double Sum { get; set; } = 0;
        public double MinSum { get; set; } = double.MaxValue;

        HashSet<ChitonNode> nodesThatAreBeingProcess = new HashSet<ChitonNode>();

        public ChitonLeaver(string[] str, char[] charsToRemove)
        {
            for (int y = 0; y < str.Length; y++)
            {
                var row = str[y];
                for (int x = 0; x < row.Length; x++)
                {
                    var value = Convert.ToInt32(row[x].ToString());
                    var curr = new ChitonNode(x, y, value);
                    nodes.Add(curr);
                }
            }

            int lastX = nodes.Max(a => a.X);
            int lastY = nodes.Max(a => a.Y);
            FirstNode = nodes.First(a => a.X == 0 && a.Y == 0);
            LastNode = nodes.First(a => a.X == lastX && a.Y == lastY);

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                var node = nodes[i];

                CountCostForNode(node);
            }
        }

        private void CountCostForNode(ChitonNode node)
        {
            if (node.lowerCostToNeighbor != null) return;
            if (nodesThatAreBeingProcess.Contains(node)) return;

            if (node == LastNode)
            {
                node.lowerCostToNeighbor = LastNode.costOfThisNode;
                node.LowerCostNeighboringNodeToEnd = LastNode;
                node.estimate = LastNode.costOfThisNode;
                return;
            }

            nodesThatAreBeingProcess.Add(node);

            var possiblePaths = new List<ChitonNode>() {
                GetNodeAtCoords(node.X + 1, node.Y),
                GetNodeAtCoords(node.X - 1, node.Y),
                GetNodeAtCoords(node.X, node.Y + 1),
                GetNodeAtCoords(node.X, node.Y - 1),
            }.Where(a => a != null).ToList();

            var notCountedPaths = possiblePaths.Where(a => a.lowerCostToNeighbor == null).ToList();
            for (int i = 0; i < notCountedPaths.Count; i++)
            {
                CountCostForNode(notCountedPaths[i]);
            }

            node.LowerCostNeighboringNodeToEnd = possiblePaths.First(a => a.lowerCostToNeighbor == possiblePaths.Min(a => a.lowerCostToNeighbor));
            node.lowerCostToNeighbor = node.LowerCostNeighboringNodeToEnd.lowerCostToNeighbor + node.costOfThisNode;

            nodesThatAreBeingProcess.Remove(node);
        }

        public ChitonNode GetNodeAtCoords(int x, int y)
        {
            return nodes.FirstOrDefault(a => a.X == x && a.Y == y);
        }

        public double GetMinSumOfTraversals()
        {
            //Traverse(AdjacencyDict.First(a => a.Key.X == 0 && a.Key.Y == 0).Key);
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
                Sum = path.Sum(a => Convert.ToDouble(a.costOfThisNode)); ;
                Sum -= AdjacencyDict.First(a => a.Key.X == 0 && a.Key.Y == 0).Key.costOfThisNode;

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

    class Point2D : IEquatable<Point2D>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals([AllowNull] Point2D other)
        {
            if (this is null) return false;
            if (other is null) return false;

            return this.X == other.X && this.Y == other.Y;
        }
    }

    class ChitonNode : IEquatable<ChitonNode>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int costOfThisNode { get; set; }
        private int hash;

        public int? startToThisNode;
        public int? estimate;
        public int? lowerCostToNeighbor;

        public ChitonNode(int x, int y, int value)
        {
            this.X = x;
            this.Y = y;
            this.costOfThisNode = value;
            hash = CalcHashCode();
        }

        private int CalcHashCode() => X * 8243 + Y * 6607 + costOfThisNode * 1871;

        public override int GetHashCode()
        {
            return hash;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ChitonNode);
        }

        public override string ToString()
        {
            return $"{X},{Y}: {lowerCostToNeighbor}";
        }

        public bool Equals(ChitonNode other)
        {
            if (other == null) return false;

            return other.GetHashCode() == this.GetHashCode();
        }
    }
}
