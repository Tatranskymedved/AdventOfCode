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
            var path = @".\Days\Day06\myInput.txt";
            var str = System.IO.File.ReadAllText(path);

            var factory = new GroupFactory();
            var groups = factory.ParseGroups(str);

            return groups.Sum(a => a.CountOfAnswerWithAllPeopleAnswered);;
        }
    }

    internal class GroupFactory
    {
        static string charsToSplitGroups = Environment.NewLine + Environment.NewLine;
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
        static string charsToSplitPeople = Environment.NewLine;
        public string value;
        public int countOfPeople;
        public Dictionary<char, int> answers = new Dictionary<char, int>();
        public int Count => answers.Count();
        public int CountOfAnswerWithAllPeopleAnswered => answers.Count(a => a.Value == countOfPeople);

        public Group(string groupLine)
        {
            var peopleLines = groupLine.Split(charsToSplitPeople);
            value = groupLine;
            countOfPeople = peopleLines.Length;

            var alreadyUsedCharsForPerson = new HashSet<char>();
            foreach (var personLine in peopleLines)
            {
                alreadyUsedCharsForPerson.Clear();
                foreach (char answer in personLine)
                {
                    //Duplicate check
                    if (alreadyUsedCharsForPerson.Contains(answer)) continue;
                    alreadyUsedCharsForPerson.Add(answer);

                    if (answers.ContainsKey(answer))
                    {
                        int count = answers[answer];
                        answers[answer] = ++count;
                    }
                    else
                    {
                        answers.Add(answer, 1);
                    }
                }
            }
        }
    }
}
