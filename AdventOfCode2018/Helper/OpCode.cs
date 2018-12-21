using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Helper
{
    public class OpCode
    {
        public enum Codes { addr, addi, mulr, muli, banr, bani, borr, bori, setr, seti, gtir, gtri, gtrr, eqir, eqri, eqrr, none }

        Codes code;
        int[] instruction;
        Dictionary<int, Codes> opCodes;

        public OpCode(Codes code, int[] instruction)
        {
            Code = code;
            Instruction = instruction;
        }

        public OpCode(Dictionary<int, List<Codes>> opCodes)
        {
            OpCodes = new Dictionary<int, Codes>();

            foreach (var op in opCodes)
            {
                OpCodes.Add(op.Key, op.Value[0]);
            }
        }

        public int[] Instruction { get => instruction; set => instruction = value; }
        internal Codes Code { get => code; set => code = value; }
        public Dictionary<int, Codes> OpCodes { get => opCodes; set => opCodes = value; }

        internal void Exec(int[] register)
        {
            if (Code == Codes.addr) Addr(register);
            else if (Code == Codes.addi) Addi(register);
            else if (Code == Codes.mulr) Mulr(register);
            else if (Code == Codes.muli) Muli(register);
            else if (Code == Codes.banr) Banr(register);
            else if (Code == Codes.bani) Bani(register);
            else if (Code == Codes.borr) Borr(register);
            else if (Code == Codes.bori) Bori(register);
            else if (Code == Codes.setr) Setr(register);
            else if (Code == Codes.seti) Seti(register);
            else if (Code == Codes.gtir) Gtir(register);
            else if (Code == Codes.gtri) Gtri(register);
            else if (Code == Codes.gtrr) Gtrr(register);
            else if (Code == Codes.eqir) Eqir(register);
            else if (Code == Codes.eqri) Eqri(register);
            else if (Code == Codes.eqrr) Eqrr(register);
        }

        internal void Exec(int[] register, int[] operation)
        {
            Codes c = OpCodes[operation[0]];

            if (c == Codes.addr) Addr(register, operation);
            else if (c == Codes.addi) Addi(register, operation);
            else if (c == Codes.mulr) Mulr(register, operation);
            else if (c == Codes.muli) Muli(register, operation);
            else if (c == Codes.banr) Banr(register, operation);
            else if (c == Codes.bani) Bani(register, operation);
            else if (c == Codes.borr) Borr(register, operation);
            else if (c == Codes.bori) Bori(register, operation);
            else if (c == Codes.setr) Setr(register, operation);
            else if (c == Codes.seti) Seti(register, operation);
            else if (c == Codes.gtir) Gtir(register, operation);
            else if (c == Codes.gtri) Gtri(register, operation);
            else if (c == Codes.gtrr) Gtrr(register, operation);
            else if (c == Codes.eqir) Eqir(register, operation);
            else if (c == Codes.eqri) Eqri(register, operation);
            else if (c == Codes.eqrr) Eqrr(register, operation);
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
            int i = Instruction[0];
            int r = Instruction[1];
            int outr = Instruction[2];

            register[outr] = (i > register[r]) ? 1 : 0;
        }

        private void Gtri(int[] register)
        {
            int r = Instruction[0];
            int i = Instruction[1];
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

        private void Addr(int[] register, int[] operation)
        {
            int r1 = operation[1];
            int r2 = operation[2];
            int outr = operation[3];

            register[outr] = register[r1] + register[r2];
        }

        private void Addi(int[] register, int[] operation)
        {
            int r = operation[1];
            int i = operation[2];
            int outr = operation[3];

            register[outr] = register[r] + i;
        }

        private void Mulr(int[] register, int[] operation)
        {
            int r1 = operation[1];
            int r2 = operation[2];
            int outr = operation[3];

            register[outr] = register[r1] * register[r2];
        }

        private void Muli(int[] register, int[] operation)
        {
            int r = operation[1];
            int i = operation[2];
            int outr = operation[3];

            register[outr] = register[r] * i;
        }

        private void Banr(int[] register, int[] operation)
        {
            int r1 = operation[1];
            int r2 = operation[2];
            int outr = operation[3];

            register[outr] = register[r1] & register[r2];
        }

        private void Bani(int[] register, int[] operation)
        {
            int r = operation[1];
            int i = operation[2];
            int outr = operation[3];

            register[outr] = register[r] & i;
        }

        private void Borr(int[] register, int[] operation)
        {
            int r1 = operation[1];
            int r2 = operation[2];
            int outr = operation[3];

            register[outr] = register[r1] | register[r2];
        }

        private void Bori(int[] register, int[] operation)
        {
            int r = operation[1];
            int i = operation[2];
            int outr = operation[3];

            register[outr] = register[r] | i;
        }

        private void Setr(int[] register, int[] operation)
        {
            int r = operation[1];
            int outr = operation[3];

            register[outr] = register[r];
        }

        private void Seti(int[] register, int[] operation)
        {
            int i = operation[1];
            int outr = operation[3];

            register[outr] = i;
        }

        private void Gtir(int[] register, int[] operation)
        {
            int r = operation[1];
            int i = operation[2];
            int outr = operation[3];

            register[outr] = (i > register[r]) ? 1 : 0;
        }

        private void Gtri(int[] register, int[] operation)
        {
            int i = operation[1];
            int r = operation[2];
            int outr = operation[3];

            register[outr] = (register[r] > i) ? 1 : 0;
        }

        private void Gtrr(int[] register, int[] operation)
        {
            int r1 = operation[1];
            int r2 = operation[2];
            int outr = operation[3];

            register[outr] = (register[r1] > register[r2]) ? 1 : 0;
        }

        private void Eqir(int[] register, int[] operation)
        {
            int i = operation[1];
            int r = operation[2];
            int outr = operation[3];

            register[outr] = (i == register[r]) ? 1 : 0;
        }

        private void Eqri(int[] register, int[] operation)
        {
            int r = operation[1];
            int i = operation[2];
            int outr = operation[3];

            register[outr] = (i == register[r]) ? 1 : 0;
        }

        private void Eqrr(int[] register, int[] operation)
        {
            int r1 = operation[1];
            int r2 = operation[2];
            int outr = operation[3];

            register[outr] = (register[r1] == register[r2]) ? 1 : 0;
        }

        public static Codes GetCode(string code)
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
    }
}
