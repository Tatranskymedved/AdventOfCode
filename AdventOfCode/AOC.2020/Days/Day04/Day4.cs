using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC._2020.Days
{
    class Day4 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day04\input.txt";

            var str = System.IO.File.ReadAllText(path);

            var fac = new PassportFactory();
            var passports = fac.ParsePassports(str);

            return passports.Count(a => a.IsValid);
        }
    }

    internal class PassportFactory
    {
        readonly string charsToSplitPassports = "\n\n";
        readonly char[] charsToSplitAttributesInPassport = new char[] { '\n', ' ' };

        public List<Passport> ParsePassports(string text)
        {
            var lines = text.Split(charsToSplitPassports);
            var passList = new List<Passport>();
            foreach (var line in lines)
            {
                passList.Add(ParsePassport(line));
            }

            return passList;
        }

        public Passport ParsePassport(string text)
        {
            var attributePairs = text.Split(charsToSplitAttributesInPassport, StringSplitOptions.RemoveEmptyEntries);
            var pass = new Passport(attributePairs);

            return pass;
        }
    }

    internal class Passport
    {
        public string ecl { get; set; }
        public string pid { get; set; }
        public string eyr { get; set; }
        public string hcl { get; set; }
        public string byr { get; set; }
        public string iyr { get; set; }
        public string cid { get; set; }
        public string hgt { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(ecl) && Regex.IsMatch(ecl, @"^(amb|blu|brn|gry|grn|hzl|oth){1}$") &&
                               !string.IsNullOrEmpty(pid) && Regex.IsMatch(pid, @"^(\d){9}$") &&
                               !string.IsNullOrEmpty(eyr) && Between(eyr, 2020, 2030) &&
                               !string.IsNullOrEmpty(hcl) && Regex.IsMatch(hcl, @"^#[0-9a-f]{6}$") &&
                               !string.IsNullOrEmpty(byr) && Between(byr, 1920, 2002) &&
                               !string.IsNullOrEmpty(iyr) && Between(iyr, 2010, 2020) &&
                               //!string.IsNullOrEmpty(cid) &&
                               !string.IsNullOrEmpty(hgt) && BetweenHeight(hgt);

        public Passport(string[] pairs)
        {
            pairs.ToList().ForEach(a =>
            {
                var row = a.Split(':');
                var prop = this.GetType().GetProperty(row[0]);
                prop?.SetValue(this, row[1]);
            });
        }

        private bool BetweenHeight(string value)
        {
            try
            {
                var parsed = Regex.Split(value, @"^(\d+)(cm|in){1}$");
                string height = parsed[1];
                string unit = parsed[2];

                if (unit == "cm")
                    return Between(height, 150, 193);
                if (unit == "in")
                    return Between(height, 59, 76);

            }
            catch
            {
                return false;
            }
            return false;
        }

        private bool Between(string value, int min, int max)
        {
            if (string.IsNullOrEmpty(value)) return false;

            int number = Convert.ToInt32(value);
            return number >= min && number <= max;
        }
    }
}
