using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode2018.Day19
{
    class Tasks
    {
        const string inputPath = @"Day19/Input.txt";
        internal enum Codes { addr, addi, mulr, muli, banr, bani, borr, bori, setr, seti, gtir, gtri, gtrr, eqir, eqri, eqrr, none }

        public static void Task1()
        {
            int ipIdx = 0;
            int instrPt = 0;
            int[] registers = new int[6];
            List<OpCode> instructions = new List<OpCode>();

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

                        instructions.Add(new OpCode(GetCode(instr[0]), nums));
                    }
                }
            }

            while (instrPt < instructions.Count)
            {
                registers[ipIdx] = instrPt;

                instructions[instrPt].Exec(registers);

                instrPt = registers[ipIdx];
                instrPt++;
            }

            Console.WriteLine(registers[0]);
        }

        public static void Task2()
        {
            int ipIdx = 0;
            int instrPt = 0;
            int[] registers = new int[6];
            registers[0] = 1;
            List<OpCode> instructions = new List<OpCode>();
            List<int> register4 = new List<int>();
            const int PATTERN_TRESHOLD = 200;

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

                        instructions.Add(new OpCode(GetCode(instr[0]), nums));
                    }
                }
            }

            for (int i = 0; i < PATTERN_TRESHOLD; i++)
            {
                registers[ipIdx] = instrPt;

                instructions[instrPt].Exec(registers);
                register4.Add(registers[4]);

                instrPt = registers[ipIdx];
                instrPt++;
            }

            var r4s = register4.GroupBy(x => x).Select(group => new { Number = group.Key, Count = group.Count() });
            var r4 = r4s.Aggregate((l, r) => l.Count > r.Count ? l : r).Number;

            var primes = FindFactors(r4);

            int r0 = CalculateRegister0(primes);

            Console.WriteLine(r0);
        }

        private static Codes GetCode(string code)
        {
            if (code == "addr") return Codes.addr;
            else if (code == "addi") return Codes.addi;
            else if (code == "mulr") return Codes.mulr;
            else if (code == "muli") return Codes.muli;
            else if (code == "banr") return Codes.banr;
            else if (code == "bani") return Codes.bani;
            else if (code == "borr") return Codes.borr;
            else if (code == "bori") return Codes.bori;
            else if (code == "setr") return Codes.setr;
            else if (code == "seti") return Codes.seti;
            else if (code == "gtir") return Codes.gtir;
            else if (code == "gtri") return Codes.gtri;
            else if (code == "gtrr") return Codes.gtrr;
            else if (code == "eqir") return Codes.eqir;
            else if (code == "eqri") return Codes.eqri;
            else if (code == "eqrr") return Codes.eqrr;
            else return Codes.none;
        }

        private static int CalculateRegister0(List<int> primes)
        {
            int r0 = 1;

            foreach(int p in primes)
            {
                r0 += p;
            }

            for (int i = 0; i < primes.Count - 1; i++)
            {
                int m = primes[i];
                for(int x = 1; x < primes.Count - i; x++)
                {
                    m *= primes[x];
                }

                r0 += m;
            }

            return r0;
        }

        private static List<int> FindFactors(int num)
        {
            List<int> result = new List<int>();

            while (num % 2 == 0)
            {
                result.Add(2);
                num /= 2;
            }

            int factor = 3;
            while (factor * factor <= num)
            {
                if (num % factor == 0)
                {
                    result.Add(factor);
                    num /= factor;
                }
                else
                {
                    factor += 2;
                }
            }

            if (num > 1) result.Add(num);

            return result;
        }
    }

    internal class OpCode
    {
        Tasks.Codes code;
        int[] instruction;

        public OpCode(Tasks.Codes code, int[] instruction)
        {
            Code = code;
            Instruction = instruction;
        }

        public int[] Instruction { get => instruction; set => instruction = value; }
        internal Tasks.Codes Code { get => code; set => code = value; }

        internal void Exec(int[] register)
        {
            if (Code == Tasks.Codes.addr) Addr(register);
            else if (Code == Tasks.Codes.addi) Addi(register);
            else if (Code == Tasks.Codes.mulr) Mulr(register);
            else if (Code == Tasks.Codes.muli) Muli(register);
            else if (Code == Tasks.Codes.banr) Banr(register);
            else if (Code == Tasks.Codes.bani) Bani(register);
            else if (Code == Tasks.Codes.borr) Borr(register);
            else if (Code == Tasks.Codes.bori) Bori(register);
            else if (Code == Tasks.Codes.setr) Setr(register);
            else if (Code == Tasks.Codes.seti) Seti(register);
            else if (Code == Tasks.Codes.gtir) Gtir(register);
            else if (Code == Tasks.Codes.gtri) Gtri(register);
            else if (Code == Tasks.Codes.gtrr) Gtrr(register);
            else if (Code == Tasks.Codes.eqir) Eqir(register);
            else if (Code == Tasks.Codes.eqri) Eqri(register);
            else if (Code == Tasks.Codes.eqrr) Eqrr(register);
        }

        private void Addr(int[] register)
        {
            int r1 = Instruction[0];
            int r2 = Instruction[1];
            int outr = Instruction[2];

            register[outr] = register[r1] + register[r2];
        }

        private void Addi(int[] register)
        {
            int r = Instruction[0];
            int i = Instruction[1];
            int outr = Instruction[2];

            register[outr] = register[r] + i;
        }

        private void Mulr(int[] register)
        {
            int r1 = Instruction[0];
            int r2 = Instruction[1];
            int outr = Instruction[2];

            register[outr] = register[r1] * register[r2];
        }

        private void Muli(int[] register)
        {
            int r = Instruction[0];
            int i = Instruction[1];
            int outr = Instruction[2];
        
            register[outr] = register[r] * i;
        }

        private void Banr(int[] register)
        {
            int r1 = Instruction[0];
            int r2 = Instruction[1];
            int outr = Instruction[2];

            register[outr] = register[r1] & register[r2];
        }

        private void Bani(int[] register)
        {
            int r = Instruction[0];
            int i = Instruction[1];
            int outr = Instruction[2];

            register[outr] = register[r] & i;
        }

        private void Borr(int[] register)
        {
            int r1 = Instruction[0];
            int r2 = Instruction[1];
            int outr = Instruction[2];

            register[outr] = register[r1] | register[r2];
        }

        private void Bori(int[] register)
        {
            int r = Instruction[0];
            int i = Instruction[1];
            int outr = Instruction[2];

            register[outr] = register[r] | i;
        }

        private void Setr(int[] register)
        {
            int r = Instruction[0];
            int outr = Instruction[2];

            register[outr] = register[r];
        }

        private void Seti(int[] register)
        {
            int i = Instruction[0];
            int outr = Instruction[2];

            register[outr] = i;
        }

        private void Gtir(int[] register)
        {
            int r = Instruction[0];
            int i = Instruction[1];
            int outr = Instruction[2];

            register[outr] = (i > register[r]) ? 1 : 0;
        }

        private void Gtri(int[] register)
        {
            int i = Instruction[0];
            int r = Instruction[1];
            int outr = Instruction[2];

            register[outr] = (register[r] > i) ? 1 : 0;
        }

        private void Gtrr(int[] register)
        {
            int r1 = Instruction[0];
            int r2 = Instruction[1];
            int outr = Instruction[2];

            register[outr] = (register[r1] > register[r2]) ? 1 : 0;
        }

        private void Eqir(int[] register)
        {
            int i = Instruction[0];
            int r = Instruction[1];
            int outr = Instruction[2];

            register[outr] = (i == register[r]) ? 1 : 0;
        }

        private void Eqri(int[] register)
        {
            int r = Instruction[0];
            int i = Instruction[1];
            int outr = Instruction[2];

            register[outr] = (i == register[r]) ? 1 : 0;
        }

        private void Eqrr(int[] register)
        {
            int r1 = Instruction[0];
            int r2 = Instruction[1];
            int outr = Instruction[2];

            register[outr] = (register[r1] == register[r2]) ? 1 : 0;
        }
    }
}
