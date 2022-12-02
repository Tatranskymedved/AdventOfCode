using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2022.Days
{
    class Day02 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day02\input.txt";
            var str = System.IO.File.ReadAllLines(path);

            var duels = new List<Duel>();
            for (int i = 0; i < str.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(str[i]))
                    break;

                var duel = new Duel() { Oponent = str[i][0].ToString(), MineOrResult = str[i][2].ToString() };

                //Part 2
                duel.MineOrResult = duel.ReassignMineBasedOnResultPartTwo();

                duels.Add(duel);
            }

            return duels.Sum(a => a.Points);
        }

        class Duel
        {
            public string Oponent { get; set; }
            public string MineOrResult { get; set; }
            public int PointsPerMove => GetPointsPerMove();
            private int pointsEvaluation = -1;

            public int PointsEvaluation
            {
                get
                {
                    if (pointsEvaluation == -1)
                    {
                        pointsEvaluation = EvaluatePartOne();
                    }

                    return pointsEvaluation;
                }
                set { pointsEvaluation = value; }
            }

            public int Points => PointsPerMove + PointsEvaluation;


            int GetPointsPerMove() => MineOrResult switch
            {
                "X" => 1,
                "Y" => 2,
                "Z" => 3,
                _ => 0
            };

            const int pointsForLose = 0;
            const int pointsForDraw = 3;
            const int pointsForWin = 6;

            int EvaluatePartOne() => Oponent switch
            {
                "A" => MineOrResult switch //Rock
                {
                    "X" => pointsForDraw, //Rock
                    "Y" => pointsForWin, //Paper
                    "Z" => pointsForLose, //Scissors
                    _ => throw new Exception($"Mine value not understood. Mine: '{MineOrResult}'")
                },
                "B" => MineOrResult switch //Paper
                {
                    "X" => pointsForLose, //Rock
                    "Y" => pointsForDraw, //Paper
                    "Z" => pointsForWin, //Scissors
                    _ => throw new Exception($"Mine value not understood. Mine: '{MineOrResult}'")
                },
                "C" => MineOrResult switch //Scissors
                {
                    "X" => pointsForWin, //Rock
                    "Y" => pointsForLose, //Paper
                    "Z" => pointsForDraw, //Scissors
                    _ => throw new Exception($"Mine value not understood. Mine: '{MineOrResult}'")
                },
                _ => throw new Exception($"Oponent value not understood. Oponent: '{Oponent}', Mine: '{MineOrResult}' ")
            };

            public string ReassignMineBasedOnResultPartTwo() => Oponent switch
            {
                "A" => MineOrResult switch //Rock
                {
                    "X" => "Z", //Lose
                    "Y" => "X", //Draw
                    "Z" => "Y", //Win
                    _ => throw new Exception($"Mine value not understood. Mine: '{MineOrResult}'")
                },
                "B" => MineOrResult switch //Paper
                {
                    "X" => "X", //Lose
                    "Y" => "Y", //Draw
                    "Z" => "Z", //Win
                    _ => throw new Exception($"Mine value not understood. Mine: '{MineOrResult}'")
                },
                "C" => MineOrResult switch //Scissors
                {
                    "X" => "Y", //Lose
                    "Y" => "Z", //Draw
                    "Z" => "X", //Win
                    _ => throw new Exception($"Mine value not understood. Mine: '{MineOrResult}'")
                },
                _ => throw new Exception($"Oponent value not understood. Oponent: '{Oponent}', Mine: '{MineOrResult}' ")
            };
        }
    }
}
