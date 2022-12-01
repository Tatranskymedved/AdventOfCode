using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC._2022.Days
{
    class Day17 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day17\input.txt";

            var line = System.IO.File.ReadAllLines(path).First();

            var r = new Regex(@".*=(?<xMin>(\D*\d+))\.\.(?<xMax>(\D*\d+)).*=(?<yMin>(\D*\d+))\.\.(?<yMax>(\D*\d+)).*");
            var res = r.Match(line);
            var xMin = Convert.ToInt32(res.Groups["xMin"].Value);
            var xMax = Convert.ToInt32(res.Groups["xMax"].Value);
            var yMin = Convert.ToInt32(res.Groups["yMin"].Value);
            var yMax = Convert.ToInt32(res.Groups["yMax"].Value);

            int generateMin = -10000;
            int generateMax = 10000;

            int defaultPos = 0;
            int xPos = defaultPos;
            int yPos = defaultPos;

            int xVel = 1;
            int yVel = 1;

            var velList = new List<(int, int)>();

            for (int i = generateMin; i <= generateMax; i++)
            {
                for (int y = generateMin; y <= generateMax; y++)
                {
                    velList.Add((i, y));
                }
            }

            int cnt = 0;
            foreach (var velocity in velList)
            {
                (xVel, yVel) = velocity;
                Run(xPos, yPos, xVel, yVel, xMin, xMax, yMin, yMax, ref cnt);

                xPos = yPos = defaultPos;
            }

            //return yMaxResult; //17766
            return cnt; //1733
        }


        void Run(int xPos, int yPos, int xVel, int yVel, int xMin, int xMax, int yMin, int yMax, ref int cnt)
        {
            int initXVel = xVel;
            int initYVel = yVel;
            int steps = 0;

            while (true)
            {
                if (++steps > 1000) break;

                xPos += xVel;
                yPos += yVel;
                if (xVel > 0) xVel--;
                if (xVel < 0) xVel++;
                yVel--;

                //if (yPos > tmpYMaxResult)
                //    tmpYMaxResult = yPos;

                if (xPos >= xMin && xPos <= xMax && yPos >= yMin && yPos <= yMax)
                {
                    //Console.WriteLine($"{initXVel},{initYVel}");
                    cnt++;
                    //if (tmpYMaxResult > yMaxResult)
                    //    yMaxResult = tmpYMaxResult;

                    break;
                }

                if ((xPos > xMax || xPos < xMin) && xVel == 0
                    || (xPos < xMin && xVel < 0)
                    || (xPos > xMax && xVel > 0))
                {
                    break;
                }
            }
            //tmpYMaxResult = 0;
        }
    }
}
