using AOC.Common.Days;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day08 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day08\input.txt";
            //var path = @".\Days\Day08\input2.txt";

            var str = System.IO.File.ReadAllLines(path);

            var wood = new Wood(str);
            wood.UpdateTreesVisibility();

            //Part 1
            //wood.PrintTree();
            //return wood.TreesVisibleCount;

            //Part 2
            wood.CalculateScenicScore();
            //wood.PrintTree();
            //Console.WriteLine(string.Join("", wood.Trees.SelectMany(a => a.Select(b => b.ScenicScore))));
            return wood.MaxScenicScore;
        }

        class Wood
        {
            public List<Tree[]> Trees { get; set; }
            public int TreesVisibleCount => Trees.SelectMany(a => a.Select(b => b)).Count(a => a.IsVisible)
                + (2 * Trees.Count) //Border
                + (2 * Trees[0].Length) //Border
                - 4; //Corners

            public int MaxScenicScore => Trees.SelectMany(a => a.Select(b => b)).Max(a => a.ScenicScore);

            public Wood(string[] trees)
            {
                Trees = trees.Select(a => a.Select(b => new Tree()
                {
                    Height = Convert.ToInt32(b.ToString())
                })
                .ToArray()).ToList();
            }

            public void UpdateTreesVisibility()
            {
                for (int x = 1; x < Trees[0].Length - 1; x++)
                {
                    int yDownBig = Trees[0][x].Height, yUpBig = Trees[Trees[x].Length - 1][x].Height;

                    for (int yDown = 1; yDown < Trees.Count - 1; yDown++)
                    {
                        if (Trees[yDown][x].Height > yDownBig)
                        {
                            Trees[yDown][x].IsVisible = true;
                            yDownBig = Trees[yDown][x].Height;
                        }
                    }
                    for (int yUp = Trees.Count - 2; yUp > 0; yUp--)
                    {
                        if (Trees[yUp][x].Height > yUpBig)
                        {
                            Trees[yUp][x].IsVisible = true;
                            yUpBig = Trees[yUp][x].Height;
                        }
                    }
                }

                for (int y = 1; y < Trees.Count - 1; y++)
                {
                    int xRightBig = Trees[y][0].Height, xLeftBig = Trees[y][Trees[y].Length - 1].Height;

                    for (int xRight = 1; xRight < Trees[y].Length - 1; xRight++)
                    {
                        if (Trees[y][xRight].Height > xRightBig)
                        {
                            Trees[y][xRight].IsVisible = true;
                            xRightBig = Trees[y][xRight].Height;
                        }
                    }
                    for (int xLeft = Trees[y].Length - 2; xLeft > 0; xLeft--)
                    {
                        if (Trees[y][xLeft].Height > xLeftBig)
                        {
                            Trees[y][xLeft].IsVisible = true;
                            xLeftBig = Trees[y][xLeft].Height;
                        }
                    }
                }
            }

            public void CalculateScenicScore()
            {
                for (int baseX = 0; baseX < Trees[0].Length; baseX++)
                {
                    for (int baseY = 0; baseY < Trees.Count; baseY++)
                    {
                        var tree = Trees[baseY][baseX];
                        if (tree.IsVisible == false) continue;


                        int y = baseY;
                        do { tree.ScoreDown++; y++; } while (y < Trees.Count - 1 && tree.Height > Trees[y][baseX].Height);
                        y = baseY;
                        do { tree.ScoreUp++; y--; } while (y > 0 && tree.Height > Trees[y][baseX].Height);

                        int x = baseX;
                        do { tree.ScoreRight++; x++; } while (x < Trees[baseY].Length - 1 && tree.Height > Trees[baseY][x].Height);
                        x = baseX;
                        do { tree.ScoreLeft++; x--; } while (x > 0 && tree.Height > Trees[baseY][x].Height);
                    }
                }
            }

            public void PrintTree()
            {
                for (int i = 0; i < Trees.Count; i++)
                {
                    for (int j = 0; j < Trees[i].Length; j++)
                    {
                        var orig = Console.ForegroundColor;

                        var tree = Trees[i][j];
                        if (tree.IsVisible)
                            Console.ForegroundColor = ConsoleColor.Green;

                        //Console.Write(Trees[i][j].Height);
                        Console.Write(Trees[i][j].ScenicScore);

                        Console.ForegroundColor = orig;
                    }
                    Console.WriteLine();
                }
            }
        }

        class Tree
        {
            public int Height { get; set; }
            public bool IsVisible { get; set; } = false;

            const int start = 0;

            public int ScoreRight { get; set; } = start;
            public int ScoreLeft { get; set; } = start;
            public int ScoreUp { get; set; } = start;
            public int ScoreDown { get; set; } = start;
            public int ScenicScore => ScoreRight * ScoreLeft * ScoreUp * ScoreDown;
        }
    }

}
