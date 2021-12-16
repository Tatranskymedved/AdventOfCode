using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day11 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day11\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            var fpm = new FloorPositionMap(str);
            int countOfSteps = fpm.DoStepsUntilNoMove();
            //var values = str.Select();
            return fpm.CountOfOccupiedSeats;
        }
    }

    class FloorPositionMap
    {
        public int RowCount { get; set; }
        public int RowLength { get; set; }
        public char[] Map { get; set; }

        public int CountOfOccupiedSeats => Map.Count(c => c == '#');

        public void PrintToConsole()
        {
            for (int i = 0; i < Map.Length; i++)
            {
                Console.Write(Map[i]);
                if ((i + 1) % RowLength == 0)
                    Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        public FloorPositionMap(string[] values)
        {
            RowCount = values.Length;
            RowLength = values.First().Length;
            Map = new char[RowCount * RowLength];

            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values[i].Length; j++)
                {
                    char c = values[i][j];
                    Map[i * RowLength + j] = c;
                }
            }
        }
        public int DoStepsUntilNoMove()
        {
            int count = 0;
            bool moved;
            do
            {
                moved = NextStep();
                if (moved)
                {
                    count++;
                }
            } while (moved);

            return count;
        }

        public bool NextStep()
        {
            //var mapCopy = Map;
            var mapCopy = new char[Map.Length];
            Array.Copy(Map, mapCopy, Map.Length);

            bool didStep = false;
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < RowLength; j++)
                {
                    var tile = TryGetPos(Map, i, j, 0, 0);
                    if (tile == '.')
                        continue;

                    int cntOfOccupied = 0;
                    //Part1 directly use TryGetPos()
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, +1, +0) == '#' ? 1 : 0;
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, +0, +1) == '#' ? 1 : 0;
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, +1, +1) == '#' ? 1 : 0;
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, -1, +0) == '#' ? 1 : 0;
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, +0, -1) == '#' ? 1 : 0;
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, -1, -1) == '#' ? 1 : 0;
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, +1, -1) == '#' ? 1 : 0;
                    cntOfOccupied += TryGetPos_FirstOccasion(mapCopy, i, j, -1, +1) == '#' ? 1 : 0;

                    if (tile == '#' && cntOfOccupied >= 5)
                    {
                        Map[i * RowLength + j] = 'L';
                        didStep = true;
                    }
                    else if (tile == 'L' && cntOfOccupied == 0)
                    {
                        Map[i * RowLength + j] = '#';
                        didStep = true;
                    }
                }
            }
            //PrintToConsole();
            return didStep;
        }

        private char TryGetPos_FirstOccasion(char[] map, int originY, int originX, int yMod, int xMod)
        {
            if (originY < 0 || originY >= (RowCount)) return '.';
            if (originX < 0 || originX >= (RowLength)) return '.';

            int x = originX;
            int y = originY;

            char c = '.';
            for (int i = 0; c != ' ' && c != 'L' && c != '#'; i++)
            {
                var newYMod = yMod > 0
                    ? yMod + i
                    : yMod < 0
                        ? yMod - i
                        : 0;
                var newXMod = xMod > 0
                    ? xMod + i
                    : xMod < 0
                        ? xMod - i
                        : 0;

                c = TryGetPos(map, originY, originX, newYMod, newXMod);
            }

            return c;
        }

        private char TryGetPos(char[] map, int originY, int originX, int yMod, int xMod)
        {
            if (originY < 0 || originY >= (RowCount)) return ' ';
            if (originX < 0 || originX >= (RowLength)) return ' ';

            int x = originX;
            int y = originY;

            //Part 1
            x += xMod;
            y += yMod;

            if (y < 0 || y >= (RowCount)) return ' ';
            if (x < 0 || x >= (RowLength)) return ' ';

            return map[y * RowLength + x];
        }
    }
}