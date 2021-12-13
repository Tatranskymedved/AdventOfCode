using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day13 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day13\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-', ',', '=' };

            var paper = new FoldPaper(15, str, charsToRemove);
            //Console.WriteLine(paper);

            paper.FoldAll();
            Console.WriteLine(paper); //code: CJHAZHKU

            return paper.HowManyDotsAreVisible();
        }
    }

    class FoldPaper
    {
        bool[,] array;
        int height = 100;
        int width = 100;
        public List<FoldPaperInstruction> listFpi = new List<FoldPaperInstruction>();

        public FoldPaper(int size, string[] str, char[] charsToRemove)
        {
            var results = str.Where(a => !string.IsNullOrEmpty(a) && !a.StartsWith("fold")).Select(a => a.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries)).Where(a => a.Length > 0).ToList();
            width = results.Max(a => Convert.ToInt32(a[0])) + 1;
            height = results.Max(a => Convert.ToInt32(a[1])) + 1;
            array = new bool[width, height];

            foreach (var line in str)
            {
                if (string.IsNullOrEmpty(line)) continue;

                if (line.Contains("fold along"))
                {
                    var foldAlong = line.Replace("fold along ", "");
                    var fold = foldAlong.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                    var isX = fold[0] == "x";
                    listFpi.Add(new FoldPaperInstruction(Convert.ToInt32(fold[1]), isX));
                }
                else
                {
                    var row = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);
                    int x = Convert.ToInt32(row[0]);
                    int y = Convert.ToInt32(row[1]);
                    array[x, y] = true;
                }
            }
        }

        public void FoldAll()
        {
            for (int i = 0; i < listFpi.Count; i++)
            {
                FoldOne(i);
            }
        }

        public void FoldOne(int index)
        {
            //first test 796, 728
            var fold = listFpi[index];

            int pos = fold.Position;

            if (fold.FoldAtX)
            {
                Console.WriteLine("Fold at x=" + pos);
                for (int x = 0; x <= pos; x++)
                {
                    int oX = pos + x;
                    int nX = pos - x;

                    if (oX < 0 || oX >= width)
                        continue;

                    for (int y = 0; y < height; y++)
                    {
                        if (nX < 0 || nX >= width)
                        {
                            array[oX, y] = false;
                            continue;
                        }

                        if (array[oX, y])
                        {
                            array[nX, y] = true;
                            array[oX, y] = false;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Fold at y=" + pos);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y <= pos; y++)
                    {
                        int oY = pos + y;
                        int nY = pos - y;

                        if (oY < 0 || oY >= height)
                            continue;

                        if (nY < 0 || nY >= height)
                        {
                            array[x, oY] = false;
                            continue;
                        }

                        if (array[x, oY])
                        {
                            array[x, nY] = true;
                            array[x, oY] = false;
                        }
                    }
                }
            }
        }


        public int HowManyDotsAreVisible()
        {
            int sum = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (array[x, y])
                        sum++;
                }
            }
            return sum;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < 70; y++)
            {
                for (int x = 0; x < 70; x++)
                {
                    sb.Append(array[x, y] ? '#' : '.');
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    class FoldPaperInstruction
    {
        public int Position { get; set; }
        private bool foldAtX = false;
        public bool FoldAtX => foldAtX;
        public bool FoldAtY => !foldAtX;

        public FoldPaperInstruction(int position, bool foldAtX)
        {
            Position = position;
            this.foldAtX = foldAtX;
        }
    }
}
