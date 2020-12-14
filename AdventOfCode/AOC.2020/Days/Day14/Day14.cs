using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day14 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day14\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var md = new MemoryDecoder(str);
            md.Do();
            return md.SumOfValuesInMemory();
        }
    }

    class MemoryDecoder
    {
        private readonly char[] arrayOfCharsToRemove = new char[] { ' ', '=' };
        public List<IMemoryInstruction> steps = new List<IMemoryInstruction>();
        public Dictionary<string, string> Memory { get; set; } = new Dictionary<string, string>();
        public MemoryMask CurrentMask { get; set; }

        public MemoryDecoder(string[] lines)
        {
            foreach (var line in lines)
            {
                var splitLine = line.Split(arrayOfCharsToRemove, StringSplitOptions.RemoveEmptyEntries);
                if (splitLine[0].Contains("mask"))
                    steps.Add(new MemoryMask(splitLine));
                else
                    steps.Add(new MemoryAssignment(splitLine));
            }
        }

        public long SumOfValuesInMemory()
        {
            return Memory.Sum(a => Convert.ToInt64(a.Value, 2));
        }

        public void Do()
        {
            steps.ForEach(a => a.Do(this));
        }

        public void Add(string memLoc, string value)
        {
            if (!Memory.TryAdd(memLoc, value))
                Memory[memLoc] = value;
        }
    }

    interface IMemoryInstruction
    {
        public void Do(MemoryDecoder md);
    }
    class MemoryMask : IMemoryInstruction
    {
        public string Mask { get; set; }
        public MemoryMask(string[] splitLine)
        {
            Mask = splitLine[1];
        }
        public void Do(MemoryDecoder md)
        {
            md.CurrentMask = this;
        }
    }
    class MemoryAssignment : IMemoryInstruction
    {
        public long Value { get; set; }
        public long MemoryLocation { get; set; }
        public MemoryAssignment(string[] splitLine)
        {
            var strMem = splitLine[0];
            var firstI = strMem.IndexOf('[');
            var memLoc = strMem.Substring(firstI + 1, strMem.Length - firstI - 2);
            MemoryLocation = Convert.ToInt64(memLoc);
            Value = Convert.ToInt64(splitLine[1]);
        }
        public void Do(MemoryDecoder md)
        {
            var mask = md.CurrentMask.Mask;
            var sb = new StringBuilder();
            var valueB = Convert.ToString(Value, 2);
            valueB = new string('0', mask.Length - valueB.Length) + valueB;
            var memLoc = Convert.ToString(MemoryLocation, 2);
            memLoc = new string('0', mask.Length - memLoc.Length) + memLoc;

            //Part 1
            //for (int i = mask.Length - 1; i >= 0; i--)
            //{
            //    var c = mask[i];
            //    if (c == 'X')
            //        sb.Append(valueB[i]);
            //    else
            //        sb.Append(mask[i]);
            //}
            //valueB = Reverse(sb.ToString());

            //Part 2
            for (int i = mask.Length - 1; i >= 0; i--)
            {
                var c = mask[i];
                if (c == '0')
                    sb.Append(memLoc[i]);
                else
                    sb.Append(mask[i]);
            }
            memLoc = Reverse(sb.ToString());

            var memoryList = new List<string>();
            ReccReplaceXWithValuesAndGetSum(memLoc, memoryList);
            memoryList.ForEach(a => md.Add(a, valueB));


            void ReccReplaceXWithValuesAndGetSum(string value, List<string> resultList)
            {
                int firstPosOfX = value.IndexOf('X');
                if (firstPosOfX == -1)
                {
                    resultList.Add(value);
                    return;
                }

                var newValue0 = value.Substring(0, firstPosOfX) + '0' + value.Substring(firstPosOfX + 1);
                var newValue1 = value.Substring(0, firstPosOfX) + '1' + value.Substring(firstPosOfX + 1);

                ReccReplaceXWithValuesAndGetSum(newValue0, resultList);
                ReccReplaceXWithValuesAndGetSum(newValue1, resultList);
            }
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
