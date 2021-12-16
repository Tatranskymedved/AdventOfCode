using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day10 : ADay
    {
        public override double Main_Double()
        {
            var path = @".\Days\Day10\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', '-' };
            int sum = 0;

            List<string> justIncompleteLines = new List<string>();
            var values = str.Select(line =>
            {
                var rows = line.Split(charsToRemove, StringSplitOptions.RemoveEmptyEntries);


                for (int i = 0; i < rows.Length; i++)
                {
                    var q = new Stack<char>();
                    string row = rows[i];
                    bool wasIncorrect = false;


                    for (int j = 0; j < row.Length; j++)
                    {
                        var c = row[j];

                        if (c.IsOpen())
                        {
                            q.Push(c);
                        }
                        else
                        {
                            var d = q.Pop();
                            if (d == '(')
                            {
                                if (c != ')')
                                {
                                    sum += c.GetScore();
                                    wasIncorrect = true;
                                    break;
                                }
                            }
                            if (d == '[')
                            {
                                if (c != ']')
                                {
                                    sum += c.GetScore();
                                    wasIncorrect = true;
                                    break;
                                }
                            }
                            if (d == '{')
                            {
                                if (c != '}')
                                {
                                    sum += c.GetScore();
                                    wasIncorrect = true;
                                    break;
                                }
                            }
                            if (d == '<')
                            {
                                if (c != '>')
                                {
                                    sum += c.GetScore();
                                    wasIncorrect = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (wasIncorrect == false)
                    {
                        justIncompleteLines.Add(row);
                    }
                }



                return rows;
            }).ToList();

            var scores = new List<double>();
            for (int i = 0; i < justIncompleteLines.Count; i++)
            {
                var score = 0d;
                var q = new Stack<char>();
                var line = justIncompleteLines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];

                    if (c.IsOpen())
                        q.Push(c);
                    else
                        q.Pop();
                }

                while (q.TryPop(out char d))
                {
                    if (d == '(')
                        score = (score * 5) + 1;
                    if (d == '[')
                        score = (score * 5) + 2;
                    if (d == '{')
                        score = (score * 5) + 3;
                    if (d == '<')
                        score = (score * 5) + 4;
                }
                scores.Add(score);
            }

            scores.Sort();
            var middleScore = scores[scores.Count / 2 ];

            return middleScore;
        }
    }

    public static class ExtChar
    {
        public static bool IsOpen(this char c)
        {
            return c == '(' || c == '[' || c == '{' || c == '<';
        }
        public static int GetScore(this char c)
        {
            return c == ')' ? 3 :
                c == ']' ? 57 :
                c == '}' ? 1197 : 25137;
        }
    }
}
