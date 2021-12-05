using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day5 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day05\input.txt";
            //var path = @".\Days\Day05\input2.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-', '>', ',' };

            var lines = str.Select(line =>
            {
                var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                return new Line()
                {
                    X1 = Convert.ToInt32(row[0]),
                    Y1 = Convert.ToInt32(row[1]),
                    X2 = Convert.ToInt32(row[2]),
                    Y2 = Convert.ToInt32(row[3]),
                };
            });
            var max = lines.Max(a => a.MaxValue);

            var board = new HydrothermalVentsBoard(lines, max);

            //Enable for nice ouput :)
            //Console.WriteLine(board.ToString());

            return board.CountOfAtLeastTwoOverlaps;
        }
    }

    class HydrothermalVentsBoard
    {
        public int CountOfAtLeastTwoOverlaps => array.Count(a => a > 1);
        int[] array;
        int width;

        public HydrothermalVentsBoard(IEnumerable<Line> lines, int max)
        {
            width = max + 1;
            array = new int[width * width];

            foreach (var line in lines)
            {
                fillArray(line);
            }
        }

        private void fillArray(Line line)
        {
            if (line.IsVerticalOrHorizontal)
            {
                var smallerX = Math.Min(line.X1, line.X2);
                var biggerX = Math.Max(line.X1, line.X2);
                var smallerY = Math.Min(line.Y1, line.Y2);
                var biggerY = Math.Max(line.Y1, line.Y2);

                for (int y = smallerY; y <= biggerY; y++)
                {
                    for (int x = smallerX; x <= biggerX; x++)
                    {
                        int index = x + y * width;
                        array[index]++;
                    }
                }
            }
            else //return //part 1
            {
                int xSizer, ySizer;
                Predicate<int> xPred, yPred;

                if (line.X1 < line.X2 && line.Y1 < line.Y2)
                {
                    xSizer = 1;
                    ySizer = 1;
                    xPred = new Predicate<int>(a => a <= line.X2);
                    yPred = new Predicate<int>(a => a <= line.Y2);
                }
                else if (line.X1 < line.X2 && line.Y1 > line.Y2)
                {
                    xSizer = 1;
                    ySizer = -1;
                    xPred = new Predicate<int>(a => a <= line.X2);
                    yPred = new Predicate<int>(a => a >= line.Y2);
                }
                else if (line.X1 > line.X2 && line.Y1 < line.Y2)
                {
                    xSizer = -1;
                    ySizer = 1;
                    xPred = new Predicate<int>(a => a >= line.X2);
                    yPred = new Predicate<int>(a => a <= line.Y2);
                }
                else //if (line.X1 > line.X2 && line.Y1 > line.Y2)
                {
                    xSizer = -1;
                    ySizer = -1;
                    xPred = new Predicate<int>(a => a >= line.X2);
                    yPred = new Predicate<int>(a => a >= line.Y2);
                }

                for (int y = line.Y1, x = line.X1; xPred(x) && yPred(y); x += xSizer, y += ySizer)
                {
                    int index = x + y * width;
                    array[index]++;
                }
            }
        }

        public override string ToString()
        {
            var chars = width.ToString().Length - 1;

            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    sb.Append(array[x + y * width].ToString().PadLeft(chars));
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    class Line
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public bool IsVerticalOrHorizontal => (X1 == X2) || (Y1 == Y2);

        public int MaxValue => Math.Max(Math.Max(Math.Max(X1, X2), Y1), Y2);

        public override string ToString()
        {
            return $"{X1},{Y1} -> {X2},{Y2}";
        }
    }
}
