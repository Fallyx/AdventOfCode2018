using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AdventOfCode2018.Helper;

namespace AdventOfCode2018.Day16
{
    class Tasks
    {
        const string inputPath = @"Day16/Input.txt";

        public static void Task1()
        {
            List<OpCodeTest> opcodes = new List<OpCodeTest>();
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

                    opcodes.Add(new OpCodeTest(instr, before, after));
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
 
        public static void Task2()
        {
            List<OpCodeTest> opcodes = new List<OpCodeTest>();
            int lineCounter = 0;
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line, line2, line3;

                while ((line = reader.ReadLine()) != null)
                {
                    line2 = reader.ReadLine();
                    line3 = reader.ReadLine();
                    reader.ReadLine(); // Skip empty line
                    lineCounter += 4;

                    if (string.IsNullOrEmpty(line) && string.IsNullOrEmpty(line2))
                    {
                        lineCounter -= 2;
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

                    opcodes.Add(new OpCodeTest(instr, before, after));
                }
            }

            Dictionary<int, List<OpCode.Codes>> operations = new Dictionary<int, List<OpCode.Codes>>();

            foreach (var o in opcodes)
            {
                if (!operations.ContainsKey(o.Instruction[0]))
                {
                    operations.Add(o.Instruction[0], new List<OpCode.Codes>());
                }

                List<OpCode.Codes> codes = operations[o.Instruction[0]];

                if (o.Addr())
                {
                    if(!codes.Contains(OpCode.Codes.addr)) codes.Add(OpCode.Codes.addr);
                }
                if (o.Addi())
                {
                    if (!codes.Contains(OpCode.Codes.addi)) codes.Add(OpCode.Codes.addi);
                }
                if (o.Mulr())
                {
                    if (!codes.Contains(OpCode.Codes.mulr)) codes.Add(OpCode.Codes.mulr);
                }
                if (o.Muli())
                {
                    if (!codes.Contains(OpCode.Codes.muli)) codes.Add(OpCode.Codes.muli);
                }
                if (o.Banr())
                {
                    if (!codes.Contains(OpCode.Codes.banr)) codes.Add(OpCode.Codes.banr);
                }
                if (o.Bani())
                {
                    if (!codes.Contains(OpCode.Codes.bani)) codes.Add(OpCode.Codes.bani);
                }
                if (o.Borr())
                {
                    if (!codes.Contains(OpCode.Codes.borr)) codes.Add(OpCode.Codes.borr);
                }
                if (o.Bori())
                {
                    if (!codes.Contains(OpCode.Codes.bori)) codes.Add(OpCode.Codes.bori);
                }
                if (o.Setr())
                {
                    if (!codes.Contains(OpCode.Codes.setr)) codes.Add(OpCode.Codes.setr);
                }
                if (o.Seti())
                {
                    if (!codes.Contains(OpCode.Codes.seti)) codes.Add(OpCode.Codes.seti);
                }
                if (o.Gtir())
                {
                    if (!codes.Contains(OpCode.Codes.gtir)) codes.Add(OpCode.Codes.gtir);
                }
                if (o.Gtri())
                {
                    if (!codes.Contains(OpCode.Codes.gtri)) codes.Add(OpCode.Codes.gtri);
                }
                if (o.Gtrr())
                {
                    if (!codes.Contains(OpCode.Codes.gtrr)) codes.Add(OpCode.Codes.gtrr);
                }
                if (o.Eqir())
                {
                    if (!codes.Contains(OpCode.Codes.eqir)) codes.Add(OpCode.Codes.eqir);
                }
                if (o.Eqri())
                {
                    if (!codes.Contains(OpCode.Codes.eqri)) codes.Add(OpCode.Codes.eqri);
                }
                if (o.Eqrr())
                {
                    if (!codes.Contains(OpCode.Codes.eqrr)) codes.Add(OpCode.Codes.eqrr);
                }
            }

            while(operations.Any(o => o.Value.Count > 1))
            {
                for(int i = 0; i < operations.Count; i++)
                {
                    if(operations[i].Count == 1)
                    {
                        for(int j = 0; j < operations.Count; j++)
                        {
                            if (j == i) continue;

                            if(operations[j].Contains(operations[i][0])) operations[j].Remove(operations[i][0]);
                        }
                    }
                }
            }

            int[] register = new int[] { 0, 0, 0, 0 };
            OpCode opCode = new OpCode(operations);

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                int lineCntr = 0;
                
                while ((line = reader.ReadLine()) != null)
                {
                    if(lineCntr < lineCounter)
                    {
                        lineCntr++;
                        continue;
                    }

                    string[] opStr = line.Split(' ');
                    int[] op = new int[] { int.Parse(opStr[0]), int.Parse(opStr[1]), int.Parse(opStr[2]), int.Parse(opStr[3]) };

                    opCode.Exec(register, op);
                }
            }

            Console.WriteLine(register[0]);
        }
        
    }

    internal class OpCodeTest
    {
        private int[] instruction;
        private int[] before;
        private int[] after;

        public OpCodeTest(int[] instruction, int[] before, int[] after)
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
