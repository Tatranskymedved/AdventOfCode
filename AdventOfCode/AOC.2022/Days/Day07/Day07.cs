using AOC.Common;
using AOC.Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC._2022.Days
{
    class Day07 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day07\input.txt";
            //var path = @".\Days\Day07\input2.txt";


            var str = System.IO.File.ReadAllLines(path);
            var charsToRemove = new char[] { ' ', ':', ',' };

            var root = new MDirectory()
            {
                Name = "",
                Parent = null,
            };

            var currDir = root;

            for (int i = 0; i < str.Length; i++)
            {
                string line = str[i];

                switch (line)
                {
                    case var l when l.StartsWith("$ cd"):
                        var cdPath = l.Substring(5);
                        if (cdPath == "/")
                        {
                            currDir = root;
                        }
                        else if (cdPath == "..")
                        {
                            currDir = currDir.Parent;
                        }
                        else
                        {
                            if (currDir.Childs.ContainsKey(cdPath) == false)
                                currDir.Childs[cdPath] = new MDirectory() { Name = cdPath, Parent = currDir };
                            currDir = currDir.Childs[cdPath];
                        }
                        break;
                    case var l when l.StartsWith("$ ls"):
                        int j = i + 1;
                        for (; j < str.Length && (str[j].StartsWith("$") == false); j++)
                        {
                            var lsLine = str[j];
                            if (lsLine.StartsWith("dir"))
                            {
                                var dirName = lsLine.Substring(4);
                                if (currDir.Childs.ContainsKey(dirName) == false)
                                    currDir.Childs[dirName] = new MDirectory() { Name = dirName, Parent = currDir };
                            }
                            else
                            {
                                var ls = lsLine.Split(' ');
                                currDir.Files[ls[1]] = new MFile() { Name = ls[1], Size = Convert.ToInt32(ls[0]), Parent = currDir };
                            }
                        }
                        i = j - 1;
                        break;
                    default:
                        throw new Exception("Unknown command");
                        break;
                };
            }

            var dirsAsList = new List<MDirectory>() { root } .Concat(root.Dirs).ToList();

            //Part 1
            //return dirsAsList.Sum(a => a.AOCSize);


            //Part 2
            const int totalMax    = 70000000;
            const int needAtLeast = 30000000;

            int currentOccupiedSize = root.Size;
            int currentFreeSpace = totalMax - currentOccupiedSize;
            int needToClearAtLeast = needAtLeast - currentFreeSpace;

            var dirsThatFitTheLimit = dirsAsList.Where(a => a.Size > needToClearAtLeast);
            return dirsThatFitTheLimit.Min(a => a.Size);
        }


        class MDirectory
        {
            public string Name { get; set; }
            public int Size => Childs.Sum(a => a.Value.Size) + Files.Sum(a => a.Value.Size);
            public string FullPath => Parent?.FullPath + $"/{Name}";
            public MDirectory Parent { get; set; }
            public Dictionary<string, MDirectory> Childs { get; set; } = new Dictionary<string, MDirectory>();
            public Dictionary<string, MFile> Files { get; set; } = new Dictionary<string, MFile>();


            public IEnumerable<MDirectory> Dirs => Childs.Select(a => a.Value).Concat(Childs.SelectMany(a => a.Value.Dirs));


            public int AOCSize => Size <= 100000 ? Size : 0;

            public override string ToString()
            {
                return this.Parent?.ToString() + $"/{Name}";
            }
        }

        class MFile
        {
            public string Name { get; set; }
            public int Size { get; set; } = 0;
            public string FullPath => Parent?.FullPath + $"/{Name}";
            public MDirectory Parent { get; set; }
        }
    }
}