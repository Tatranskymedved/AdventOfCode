using System;

namespace AOC.Common.Days
{
    public abstract class ADay
    {
        public object Main()
        {
            var resultString = this.Main_String();
            if (!string.IsNullOrEmpty(resultString)) return resultString;

            var resultInt = this.Main_Int32();
            if (resultInt != Int32.MinValue) return resultInt;

            var resultDouble = this.Main_Double();
            return resultDouble;
        }

        public virtual string Main_String() { return null; }
        public virtual int Main_Int32() { return Int32.MinValue; }
        public virtual double Main_Double() { return Double.NaN; }
    }
}
