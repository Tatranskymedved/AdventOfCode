﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day9 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day09\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-' };

            //var values = str.Select();
            return 0;
        }
    }
}
