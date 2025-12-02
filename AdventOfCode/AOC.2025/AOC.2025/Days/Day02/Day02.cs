using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Transactions;
using System.Text;

namespace AOC._2025.Days
{
    class Day02 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day02\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            StringBuilder sb = new StringBuilder();
            foreach (var line in str)
            {
                sb.Append(line);
            }

            var stringIDs = sb.ToString().Trim().Split(',');

            var listOfInvalidIDs = new List<double>();

            foreach (var stringID in stringIDs)
            {
                var split = stringID.Split('-');
                string a = split[0];
                string b = split[1];

                double ad = Convert.ToDouble(a);
                double bd = Convert.ToDouble(b);

                for (double i = ad; i <= bd; i++)
                {
                    //Part 1
                    //if (IsValid(i.ToString()) == false) listOfInvalidIDs.Add(i);

                    //Part 2
                    if (IsValid2(i.ToString()) == false) listOfInvalidIDs.Add(i);
                }
            }

            return listOfInvalidIDs.Sum();
        }

        bool IsValid2(string val)
        {
            var trimmedVal = val.TrimStart('0');

            for (int i = 1; i <= val.Length / 2; i++)
            {
                string key = trimmedVal.Substring(0, i);

                var replacedText = trimmedVal.Replace(key, "");
                if (replacedText.Length == 0)
                {
                    return false;
                }
            }
            return true;
        }

        bool IsValid(string val)
        {
            var trimmedVal = val.TrimStart('0');

            if (trimmedVal.Length % 2 != 0) return true;

            var secondHalfIndex = (trimmedVal.Length / 2);

            string partA = trimmedVal.Substring(0, secondHalfIndex);
            string partB = trimmedVal.Substring(secondHalfIndex);
            return partA != partB;
        }
    }
}
