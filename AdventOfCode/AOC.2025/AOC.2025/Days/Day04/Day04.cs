using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Transactions;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AOC._2025.Days
{
    class Day04 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day04\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            int width = str[0].Length, height = str.Length;

            Node[,] arr = new Node[height, width];
            for (int iY = 0; iY < height; iY++)
            {
                string line = str[iY];
                for (int iX = 0; iX < width; iX++)
                {
                    arr[iX, iY] = new Node(c: line[iX]);
                }
            }

            int sumOfMoveableRolls = 0;
            for (int iY = 0; iY < height; iY++)
            {
                for (int iX = 0; iX < width; iX++)
                {
                    if (arr[iX, iY].C != '@') continue;

                    int numOfRollsAround = 0;
                    try { if (arr[iX - 1, iY - 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX - 1, iY + 0].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX - 1, iY + 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 0, iY - 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 0, iY + 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 1, iY - 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 1, iY + 0].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 1, iY + 1].C == '@') { ++numOfRollsAround; } } catch { }

                    if (numOfRollsAround < 4)
                    {
                        arr[iX, iY].CanBeAccessedByForklift = true;
                        sumOfMoveableRolls++;
                    }
                }
            }

            int counterOfRemovedRolls = 0;
            while (RunCleanup(ref arr, height, width, ref counterOfRemovedRolls)) ;


            //Debug:
            //Console.WriteLine();
            //for (int iY = 0; iY < height; iY++)
            //{
            //    for (int iX = 0; iX < width; iX++)
            //    {
            //        Console.Write(arr[iX, iY].CanBeAccessedByForklift ? 'x' : arr[iX, iY].C);
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();




            // Part 1
            //return sumOfMoveableRolls;

            return counterOfRemovedRolls;
        }

        bool RunCleanup(ref Node[,] arr, int height, int width, ref int counter)
        {
            bool wasAnythingCleaned = false;

            for (int iY = 0; iY < height; iY++)
            {
                for (int iX = 0; iX < width; iX++)
                {
                    if (arr[iX, iY].C != '@') continue;

                    int numOfRollsAround = 0;
                    try { if (arr[iX - 1, iY - 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX - 1, iY + 0].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX - 1, iY + 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 0, iY - 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 0, iY + 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 1, iY - 1].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 1, iY + 0].C == '@') { ++numOfRollsAround; } } catch { }
                    try { if (arr[iX + 1, iY + 1].C == '@') { ++numOfRollsAround; } } catch { }

                    if (numOfRollsAround < 4)
                    {
                        arr[iX, iY].C = '.';
                        wasAnythingCleaned = true;
                        counter++;
                    }
                }
            }
            return wasAnythingCleaned;
        }

        public struct Node(char c, bool canBeAccessedByForklift = false)
        {
            public char C { get; set; } = c;
            public bool CanBeAccessedByForklift { get; set; } = canBeAccessedByForklift;
        }
    }
}
