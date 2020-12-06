using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day6 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day06\input.txt";
            var str = System.IO.File.ReadAllText(path);

            var factory = new GroupFactory();
            var groups = factory.ParseGroups(str);

            return groups.Sum(g => g.Count);
        }
    }

    internal class GroupFactory
    {
        static string charsToSplitGroups = "\n\n";
        public List<Group> ParseGroups(string inputLines)
        {
            var groups = inputLines.Split(charsToSplitGroups);
            var groupsList = new List<Group>();
            foreach (var groupLine in groups)
            {
                groupsList.Add(new Group(groupLine));
            }

            return groupsList;
        }
    }

    internal class Group
    {
        static string charsToSplitPeople = "\n";
        Dictionary<char, int> answers = new Dictionary<char, int>();
        public int Count => answers.Count(a => a.Count());

        public Group(string groupLine)
        {
            var peopleLines = groupLine.Split(charsToSplitPeople);

            foreach (var personLine in peopleLines)
            {
                foreach (char answer in personLine)
                {
                    if(answers.TryGetKey(answer, out int count))
                    {
                        answers[answer] = count++;
                    }
                    answers.Add(answer, 1);
                }
            }
        }
    }
}
