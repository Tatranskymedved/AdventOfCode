using AOC.Common.Days;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC._2020.Days
{
    class Day8 : ADay
    {
        public override int Main_Int32()
        {
            var path = @".\Days\Day08\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            // Part 1
            //var baseCp = new ConsolePlayer(str);
            //baseCp.StartExecution();
            //return baseCp.Accumulator;

            // Part 2
            var cpList = new List<ConsolePlayer>();
            var threadList = new List<Thread>();
            for (int i = 0; i < str.Length; i++)
            {
                var player = new ConsolePlayer(str);
                cpList.Add(player);

                var thread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    player.ReplaceSingleJmpNopAndExecute(i);
                });
                thread.Start();
            }

            while(!threadList.All(a => a.IsAlive))
            {
                Thread.Sleep(10);
            }

            return cpList.FirstOrDefault(a => !a.IsInfinite)?.Accumulator ?? 0;
        }
    }

    class ConsolePlayer
    {
        public int Accumulator { get; set; } = 0;
        public int CurrentPosition { get; set; } = 0;
        public List<Instruction> Instructions { get; } = new List<Instruction>();
        public HashSet<int> RunnedInstructions { get; } = new HashSet<int>();
        public bool IsInfinite { get; set; } = false;

        public void StartExecution()
        {
            ExecuteNext(0);
        }
        public void ExecuteNext()
        {
            ExecuteNext(1);
        }
        public void ExecuteNext(int step)
        {
            var next = CurrentPosition + step;

            bool contains = RunnedInstructions.Contains(next);
            if (contains)
            {
                IsInfinite = true;
                return;
            }
            if (next >= Instructions.Count)
            {
                IsInfinite = false;
                return;
            }

            CurrentPosition = next;
            RunnedInstructions.Add(next);
            Instructions[next].Do(this);
        }

        public ConsolePlayer(string[] instructions)
        {
            foreach (var instr in instructions)
            {
                var instruction = GetInstruction(instr);
                Instructions.Add(instruction);
            }
        }

        private Instruction GetInstruction(string instr)
        {
            var instrSplit = instr.Split(' ');
            switch (instrSplit[0])
            {
                case "acc":
                    return new ACC(instrSplit[1]);
                case "jmp":
                    return new JMP(instrSplit[1]);
                case "nop":
                    return new NOP(instrSplit[1]);
                default:
                    break;
            }
            return null;
        }
        private Instruction SwitchToOpposite(Instruction instr)
        {
            switch (instr)
            {
                case JMP j:
                    return new NOP(instr.Value);
                case NOP n:
                    return new JMP(instr.Value);
                default:
                    throw new ArgumentException(nameof(instr));
                case null:
                    throw new ArgumentNullException(nameof(instr));
            }
        }

        public void ReplaceSingleJmpNopAndExecute(int skipStep)
        {
            var instruction = this.Instructions.Where(a => a.GetType() == typeof(NOP) || a.GetType() == typeof(JMP)).Skip(skipStep).FirstOrDefault();
            if (instruction == null) return;

            var index = this.Instructions.FindIndex(a => a.Equals(instruction));
            this.Instructions[index] = SwitchToOpposite(instruction);

            this.StartExecution();
        }
    }

    abstract class Instruction
    {
        public int Value { get; set; }
        public Instruction(string value)
        {
            Value = Convert.ToInt32(value);
        }
        public Instruction(int value)
        {
            Value = value;
        }
        public abstract void Do(ConsolePlayer player);
    }

    class ACC : Instruction
    {
        public ACC(string value) : base(value) { }
        public ACC(int value) : base(value) { }
        public override void Do(ConsolePlayer player)
        {
            player.Accumulator += Value;
            player.ExecuteNext();
        }
    }
    class NOP : Instruction
    {
        public NOP(string value) : base(value) { }
        public NOP(int value) : base(value) { }
        public override void Do(ConsolePlayer player)
        {
            player.ExecuteNext();
        }
    }

    class JMP : Instruction
    {
        public JMP(string value) : base(value) { }
        public JMP(int value) : base(value) { }
        public override void Do(ConsolePlayer player)
        {
            player.ExecuteNext(Value);
        }
    }
}
