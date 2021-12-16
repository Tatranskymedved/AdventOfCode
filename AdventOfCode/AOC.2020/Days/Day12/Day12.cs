using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day12 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day12\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var sc = new ShipController(str);

            //Part 1
            //sc.Do();

            sc.DoWaypoint();

            return sc.ManhattanDistance;
        }
    }

    class ShipController
    {
        List<ShipInstruction> instructions = new List<ShipInstruction>();

        public int Angle { get; set; } = 0;
        public int shipX { get; set; } = 0;
        public int shipY { get; set; } = 0;
        public int waypointX { get; set; } = 10;
        public int waypointY { get; set; } = 1;
        public int ManhattanDistance => Math.Abs(shipX) + Math.Abs(shipY);
        public ShipController(string[] input)
        {
            instructions.AddRange(input.Select(a => new ShipInstruction(a)));
        }
        public override string ToString()
        {
            return $"shipX: {this.shipX}\tshipY:{shipY}\twaypointX:{waypointX}\twaypointY:{waypointY}\tAngle:{Angle}";
        }
        public void MoveFwd(int Fwd)
        {
            var angle = Angle % 360;
            switch (angle)
            {
                case 0:
                    MoveShipXY(Fwd, 0);
                    break;
                case 90:
                    MoveShipXY(0, Fwd * -1);
                    break;
                case 180:
                    MoveShipXY(Fwd * -1, 0);
                    break;
                case 270:
                    MoveShipXY(0, Fwd);
                    break;
            }
        }
        public void MoveWaypointFwd(int Fwd)
        {
            MoveShipXY(Fwd * waypointX, Fwd * waypointY);
        }
        public void MoveShipXY(int X, int Y)
        {
            this.shipX += X;
            this.shipY += Y;
        }
        public void MoveWaypointXY(int X, int Y)
        {
            this.waypointX += X;
            this.waypointY += Y;
        }
        public void Rotate(int angle)
        {
            Angle += angle;
        }
        public void RotateWaypoint(int angle)
        {
            var wpX = waypointX;
            var wpY = waypointY;

            switch (angle)
            {
                case 0:
                    break;
                case 90:
                case -270:
                    waypointX = wpY;
                    waypointY = wpX * -1;
                    break;
                case -180:
                case 180:
                    waypointX = wpX * -1;
                    waypointY = wpY * -1;
                    break;
                case -90:
                case 270:
                    waypointX = wpY * -1;
                    waypointY = wpX;
                    break;
            }
        }
        public void Do()
        {
            instructions.ForEach(a => a.Do(this));
        }
        public void DoWaypoint()
        {
            instructions.ForEach(a => {
                a.DoWaypoint(this);
                //Console.WriteLine(a.inputValue + " " +this.ToString());
            });
        }
    }

    class ShipInstruction
    {
        public string inputValue { get; set; }
        public int MoveFwd { get; set; }
        public int MoveX { get; set; }
        public int MoveY { get; set; }
        public int RotateAngle { get; set; }

        public ShipInstruction(string input)
        {
            inputValue = input;
            var action = input[0];
            var value = Convert.ToInt32(input.Substring(1));
            switch (action)
            {
                case 'N':
                    MoveY = value;
                    break;
                case 'S':
                    MoveY = value * -1;
                    break;
                case 'E':
                    MoveX = value;
                    break;
                case 'W':
                    MoveX = value * -1;
                    break;
                case 'L':
                    RotateAngle = value * -1;
                    break;
                case 'R':
                    RotateAngle = value;
                    break;
                case 'F':
                    MoveFwd = value;
                    break;
                default:
                    break;
            }
        }

        public void Do(ShipController ship)
        {
            if (MoveX != 0 || MoveY != 0)
                ship.MoveShipXY(MoveX, MoveY);
            if (MoveFwd != 0)
                ship.MoveFwd(MoveFwd);
            if (RotateAngle != 0)
                ship.Rotate(RotateAngle);
        }
        public void DoWaypoint(ShipController ship)
        {
            if (MoveX != 0 || MoveY != 0)
                ship.MoveWaypointXY(MoveX, MoveY);
            if (MoveFwd != 0)
                ship.MoveWaypointFwd(MoveFwd);
            if (RotateAngle != 0)
                ship.RotateWaypoint(RotateAngle);
        }
    }
}
