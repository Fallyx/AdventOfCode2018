using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2018.Day16
{
    class Tasks
    {
        const string inputPath = @"Day16/Input.txt";

        public static void Task1()
        {
            List<OpCode> opcodes = new List<OpCode>();
            int threeOrMore = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line, line2, line3;

                while ((line = reader.ReadLine()) != null)
                {
                    line2 = reader.ReadLine();
                    line3 = reader.ReadLine();
                    reader.ReadLine(); // Skip empty line

                    if(string.IsNullOrEmpty(line) && string.IsNullOrEmpty(line2))
                    {
                        break;
                    }

                    string[] beforeStr = line.Split(',');
                    string[] instruction = line2.Split(' ');
                    string[] afterStr = line3.Split(',');

                    int[] before = new int[4];
                    int[] after = new int[4];

                    before[0] = int.Parse(beforeStr[0].Substring(beforeStr[0].Length - 1));
                    before[1] = int.Parse(beforeStr[1].Trim());
                    before[2] = int.Parse(beforeStr[2].Trim());
                    before[3] = int.Parse(beforeStr[3].Substring(1, 1));

                    after[0] = int.Parse(afterStr[0].Substring(afterStr[0].Length - 1));
                    after[1] = int.Parse(afterStr[1].Trim());
                    after[2] = int.Parse(afterStr[2].Trim());
                    after[3] = int.Parse(afterStr[3].Substring(1, 1));

                    int[] instr = new int[] { int.Parse(instruction[0]), int.Parse(instruction[1]), int.Parse(instruction[2]), int.Parse(instruction[3]) };

                    opcodes.Add(new OpCode(instr, before, after));
                }
            }

            foreach(var o in opcodes)
            {
                int counter = 0;

                if (o.Addr()) counter++;
                if (o.Addi()) counter++;
                if (o.Mulr()) counter++;
                if (o.Muli()) counter++;
                if (o.Banr()) counter++;
                if (o.Bani()) counter++;
                if (o.Borr()) counter++;
                if (o.Bori()) counter++;
                if (o.Setr()) counter++;
                if (o.Seti()) counter++;
                if (o.Gtir()) counter++;
                if (o.Gtri()) counter++;
                if (o.Gtrr()) counter++;
                if (o.Eqir()) counter++;
                if (o.Eqri()) counter++;
                if (o.Eqrr()) counter++;

                if (counter > 2) threeOrMore++;
            }

            Console.WriteLine(threeOrMore);
        }
    }

    internal class OpCode
    {
        private int[] instruction;
        private int[] before;
        private int[] after;

        public OpCode(int[] instruction, int[] before, int[] after)
        {
            Instruction = instruction;
            Before = before;
            After = after;
        }

        public int[] Instruction { get => instruction; set => instruction = value; }
        public int[] Before { get => before; set => before = value; }
        public int[] After { get => after; set => after = value; }

        internal bool Addr()
        {
            int r1 = Instruction[1];
            int r2 = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r1] + tmp[r2];

            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Addi()
        {
            int r = Instruction[1];
            int i = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r] + i;

            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Mulr()
        {
            int r1 = Instruction[1];
            int r2 = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r1] * tmp[r2];
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Muli()
        {
            int r = Instruction[1];
            int i = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r] * i;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Banr()
        {
            int r1 = Instruction[1];
            int r2 = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r1] & tmp[r2];
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Bani()
        {
            int r = Instruction[1];
            int i = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r] & i;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Borr()
        {
            int r1 = Instruction[1];
            int r2 = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r1] | tmp[r2];
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Bori()
        {
            int r = Instruction[1];
            int i = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r] | i;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Setr()
        {
            int r = Instruction[1];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = tmp[r];
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Seti()
        {
            int i = Instruction[1];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = i;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Gtir()
        {
            int i = Instruction[1];
            int r = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = (i > tmp[r]) ? 1 : 0;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Gtri()
        {
            int r = Instruction[1];
            int i = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = (tmp[r] > i) ? 1 : 0;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Gtrr()
        {
            int r1 = Instruction[1];
            int r2 = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = (tmp[r1] > tmp[r2]) ? 1 : 0;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Eqir()
        {
            int i = Instruction[1];
            int r = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = (tmp[r] == i) ? 1 : 0;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Eqri()
        {
            int r = Instruction[1];
            int i = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = (tmp[r] == i) ? 1 : 0;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        internal bool Eqrr()
        {
            int r1 = Instruction[1];
            int r2 = Instruction[2];
            int outr = Instruction[3];

            int[] tmp = CreateTmp();

            tmp[outr] = (tmp[r1] == tmp[r2]) ? 1 : 0;
            if (tmp.SequenceEqual(After)) return true;
            else return false;
        }

        private int[] CreateTmp()
        {
            return new int[]
            {
                Before[0],
                Before[1],
                Before[2],
                Before[3]
            };
        }
    }
}
