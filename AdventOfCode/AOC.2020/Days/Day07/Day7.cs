using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day7 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day07\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            var bc = new BagCollector();
            bc.Parse(str);

            return bc.GetBagsThatContains("shiny gold").Count();
        }
    }

    /// <summary>
    /// For part 1
    /// </summary>
    class BagCollector
    {
        public List<Bag> Bags = new List<Bag>();
        
        public void Parse(string[] lines)
        {
            foreach (var line in lines)
            {
                Bags.Add(new Bag(line));
            }
        }

        public HashSet<string> GetBagsThatContains(string value)
        {
            var result = new HashSet<string>();

            ReccursiveGetBagsThatContains(value, ref result);

            return result;
        }

        public bool ReccursiveGetBagsThatContains(string value, ref HashSet<string> hashSetOfBags)
        {
            if (hashSetOfBags == null) hashSetOfBags = new HashSet<string>();

            bool foundNew = false;
            foreach (var bag in Bags)
            {
                var foundNewBitStepUp = bag.GetParentsThatContains(value, hashSetOfBags);
                if (foundNewBitStepUp)
                    foundNew = foundNewBitStepUp;
            }

            while(foundNew)
            {
                foundNew = false;

                var copiedBags = hashSetOfBags.ToArray();
                foreach (var bag in copiedBags)
                {
                    var foundNewBitStepUp = this.ReccursiveGetBagsThatContains(bag,ref hashSetOfBags);
                    if (foundNewBitStepUp)
                        foundNew = foundNewBitStepUp;
                }
            }

            return false;
        }
    }

    internal class Bag
    {
        static char[] splitChars = new char[] { ',', '.' };
        public Dictionary<string, int> Store = new Dictionary<string, int>();
        public string Name;

        public Bag(string line)
        {
            var lineToTwoParts = line.Split(" bags contain ");
            Name = lineToTwoParts[0];

            if (lineToTwoParts[1].Equals("no other bags.")) return;

            var containment = lineToTwoParts[1].Replace(" bags", "").Replace(" bag", "").Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
            foreach (var singleInnerBagType in containment)
            {
                var singleBagSplit = singleInnerBagType.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Store.Add(singleBagSplit[1] + " " + singleBagSplit[2], Convert.ToInt32(singleBagSplit[0]));
            }
        }

        public bool GetParentsThatContains(string value, HashSet<string> hashSetOfParents)
        {
            if (this.Store.ContainsKey(value) && !hashSetOfParents.Contains(Name))
            {
                hashSetOfParents.Add(Name);
                return true;
            }
            return false;
        }
    }
}
