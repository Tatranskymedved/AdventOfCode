using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day5 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day05\input.txt";

            var str = System.IO.File.ReadAllLines(path);
            var tickets = str.Select(a => new Ticket(a)).ToList();

            //Part2
            var min = tickets.Min(a => a.RowNumber) + 1;
            var max = tickets.Max(a => a.RowNumber) - 1;
            for(int rowIndex = min; rowIndex <= max; rowIndex++)
            {
                if(tickets.Count(a => a.RowNumber == rowIndex) < 8)
                {
                    var myRowTickets = tickets.FindAll(a => a.RowNumber == rowIndex);
                    for (int colIndex = 0; colIndex < 8; colIndex++)
                    {
                        var seatTicket = myRowTickets.Find(a => a.ColumnNumber == colIndex);
                        if (seatTicket == null)
                            return rowIndex * 8 + colIndex;
                    }
                }
            }

            return tickets.Max(ticket => ticket.SeatId);
        }
    }

    internal class Ticket
    {
        static List<Func<char, int>> ListRows = new List<Func<char, int>>()
        {
            (a) => { if(a == 'F') return 0; else return 64; },
            (a) => { if(a == 'F') return 0; else return 32; },
            (a) => { if(a == 'F') return 0; else return 16; },
            (a) => { if(a == 'F') return 0; else return 8; },
            (a) => { if(a == 'F') return 0; else return 4; },
            (a) => { if(a == 'F') return 0; else return 2; },
            (a) => { if(a == 'F') return 0; else return 1; },
        };
        static List<Func<char, int>> ListCols = new List<Func<char, int>>()
        {
            (a) => { if(a == 'L') return 0; else return 4; },
            (a) => { if(a == 'L') return 0; else return 2; },
            (a) => { if(a == 'L') return 0; else return 1; },
        };

        public string Row { get; set; }
        public string Column { get; set; }

        public int RowNumber => ListRows[0](Row[0]) +
                                ListRows[1](Row[1]) +
                                ListRows[2](Row[2]) +
                                ListRows[3](Row[3]) +
                                ListRows[4](Row[4]) +
                                ListRows[5](Row[5]) +
                                ListRows[6](Row[6]);
        /// <summary>
        /// Alternative solution with linq
        /// </summary>
        public int RowNumberLinq => Row.Select((item,index) => new { item, index})
                                       .Sum((a) => ListRows[a.index](a.item));

        public int ColumnNumber => ListCols[0](Column[0]) +
                                   ListCols[1](Column[1]) +
                                   ListCols[2](Column[2]);
        public int SeatId => RowNumber * 8 + ColumnNumber;

        public Ticket(string ticket)
        {
            Row = ticket.Substring(0, 7);
            Column = ticket.Substring(7, 3);
        }
    }
}
