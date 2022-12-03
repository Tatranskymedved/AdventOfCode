using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.Common
{
    public static class AlphabetHelpers
    {
        /// <summary>
        /// Returns int value for char by alphabet order. Supports both lower/capital.
        /// "a"=1, "b"=2, "c"=3 ... "z"=26, "A"=27, "B"=28,..., "Z"=52
        /// </summary>
        /// <param name="myChar">Character value in string variable</param>
        /// <returns>Value from 1 to 52 for valid inputs. 0 if input is invalid. </returns>
        public static int GetAlphabetForCharAsString(string myChar) =>
            myChar switch
            {
                "a" => 1,
                "b" => 2,
                "c" => 3,
                "d" => 4,
                "e" => 5,
                "f" => 6,
                "g" => 7,
                "h" => 8,
                "i" => 9,
                "j" => 10,
                "k" => 11,
                "l" => 12,
                "m" => 13,
                "n" => 14,
                "o" => 15,
                "p" => 16,
                "q" => 17,
                "r" => 18,
                "s" => 19,
                "t" => 20,
                "u" => 21,
                "v" => 22,
                "w" => 23,
                "x" => 24,
                "y" => 25,
                "z" => 26,
                "A" => 27,
                "B" => 28,
                "C" => 29,
                "D" => 30,
                "E" => 31,
                "F" => 32,
                "G" => 33,
                "H" => 34,
                "I" => 35,
                "J" => 36,
                "K" => 37,
                "L" => 38,
                "M" => 39,
                "N" => 40,
                "O" => 41,
                "P" => 42,
                "Q" => 43,
                "R" => 44,
                "S" => 45,
                "T" => 46,
                "U" => 47,
                "V" => 48,
                "W" => 49,
                "X" => 50,
                "Y" => 51,
                "Z" => 52,
                _ => 0
            };
    }
}
