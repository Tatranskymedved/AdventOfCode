using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC._2022.Days
{
    class Day04 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day04\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ',' };

            var bm = new BoardManager(str, charsToRemove);

            return bm.MakeCalls();
        }
    }

    class BoardManager
    {
        List<int> calls = new List<int>();
        List<Board> boards = new List<Board>();

        public BoardManager(string[] lines, char[] charsToRemove)
        {
            calls.AddRange(lines.First().Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries).Select(a => Convert.ToInt32(a)));

            for (int i = 2; i < lines.Length; i += 6)
            {
                boards.Add(new Board(lines.Skip(i).Take(5).ToArray(), charsToRemove));
            }
        }

        public int MakeCalls()
        {
            bool isLast = false;

            for (int i = 0; i < calls.Count; i++)
            {
                var call = calls[i];
                var brds = boards.Where(a => a.IsFinished == false);
                isLast = brds.Count() == 1;

                foreach (var board in brds)
                {
                    var sum = board.MakeCall(call);
                    //Part 1
                    if (sum != null)
                        //Part 2
                        if (isLast)
                            return sum.Value * call;

                }
            }
            return 0;
        }
    }

    class Board
    {
        public bool IsFinished { get; set; } = false;
        List<Number> numbers = new List<Number>();

        public int SumOfUnmarked => numbers.Where(a => a.WasDrawn == false).Sum(a => a.Value);

        public Board(string[] lines, char[] charsToRemove)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                var nums = lines[i].Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < nums.Length; j++)
                {
                    numbers.Add(new Number(Convert.ToInt32(nums[j]), i, j));
                }
            }
        }

        public int? MakeCall(int call)
        {
            numbers.Where(a => a.Value == call).ToList().ForEach(a => a.WasDrawn = true);

            if (numbers.Count(a => a.WasDrawn && a.PositionX == 0) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionX == 1) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionX == 2) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionX == 3) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionX == 4) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionY == 0) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionY == 1) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionY == 2) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionY == 3) == 5) { IsFinished = true; return SumOfUnmarked; }
            if (numbers.Count(a => a.WasDrawn && a.PositionY == 4) == 5) { IsFinished = true; return SumOfUnmarked; }

            return null;
        }

        public override string ToString()
        {
            return
                string.Join(", ", numbers.Where(a => a.PositionX == 0).OrderBy(a => a.PositionY)) + Environment.NewLine +
                string.Join(", ", numbers.Where(a => a.PositionX == 1).OrderBy(a => a.PositionY)) + Environment.NewLine +
                string.Join(", ", numbers.Where(a => a.PositionX == 2).OrderBy(a => a.PositionY)) + Environment.NewLine +
                string.Join(", ", numbers.Where(a => a.PositionX == 3).OrderBy(a => a.PositionY)) + Environment.NewLine +
                string.Join(", ", numbers.Where(a => a.PositionX == 4).OrderBy(a => a.PositionY)) + Environment.NewLine;
        }
    }
    class Number
    {
        public int Value { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool WasDrawn { get; set; } = false;

        public Number(int value, int x, int y)
        {
            Value = value;
            PositionX = x;
            PositionY = y;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
