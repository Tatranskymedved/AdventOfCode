using System;
using System.Collections.Generic;
using System.Text;

namespace AOC._2020.Days
{
    public abstract class ADay
    {
        public object Main()
        {
            var resultString = this.Main_String();
            if (!string.IsNullOrEmpty(resultString)) return resultString;

            var resultInt = this.Main_Int32();
            return resultInt;
        }

        public virtual string Main_String() { return null; }
        public virtual int Main_Int32() { return Int32.MinValue; }
    }
}
