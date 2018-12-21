using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AdventOfCode2018.Helper;

namespace AdventOfCode2018.Day21
{
    class Tasks
    {
        const string inputPath = @"Day21/Input.txt";
        internal enum Codes { addr, addi, mulr, muli, banr, bani, borr, bori, setr, seti, gtir, gtri, gtrr, eqir, eqri, eqrr, none }

        public static void Task1()
        {
            int ipIdx = 0;
            int instrPt = 0;
            int[] registers = new int[6];
            List<OpCode> instructions = new List<OpCode>();
            Dictionary<int, int> r0s = new Dictionary<int, int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line[0] == '#')
                    {
                        ipIdx = int.Parse(line[4].ToString());
                    }
                    else
                    {
                        string[] instr = line.Split(' ');
                        int[] nums = new int[]
                        {
                            int.Parse(instr[1]),
                            int.Parse(instr[2]),
                            int.Parse(instr[3])
                        };

                        instructions.Add(new OpCode(OpCode.GetCode(instr[0]), nums));
                    }
                }
            }

            registers = new int[6];
            instrPt = 0;

            while (instrPt < instructions.Count)
            {
                registers[ipIdx] = instrPt;

                if(instrPt == 28)
                {
                    break;
                }

                instructions[instrPt].Exec(registers);

                instrPt = registers[ipIdx];
                instrPt++;
            }           

            Console.WriteLine(registers[2]);
        }

        public static void Task2()
        {
            int ipIdx = 0;
            int instrPt = 0;
            int[] registers = new int[6];
            List<OpCode> instructions = new List<OpCode>();
            Dictionary<int, int> r0s = new Dictionary<int, int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line[0] == '#')
                    {
                        ipIdx = int.Parse(line[4].ToString());
                    }
                    else
                    {
                        string[] instr = line.Split(' ');
                        int[] nums = new int[]
                        {
                            int.Parse(instr[1]),
                            int.Parse(instr[2]),
                            int.Parse(instr[3])
                        };

                        instructions.Add(new OpCode(OpCode.GetCode(instr[0]), nums));
                    }
                }
            }

            registers = new int[6];
            instrPt = 0;
            HashSet<int> r2s = new HashSet<int>();

            while(true)
            {
                registers[ipIdx] = instrPt;

                if (instrPt == 28)
                {
                    if (r2s.Contains(registers[2])) break;
                    r2s.Add(registers[2]);
                }

                instructions[instrPt].Exec(registers);

                instrPt = registers[ipIdx];
                instrPt++;
            }

            Console.WriteLine(r2s.Last());
        }
    }
}
