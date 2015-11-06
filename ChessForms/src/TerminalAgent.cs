using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class TerminalAgent : Agent
    {
        //Methods
        public delegate string readString();
        readString read;

        public TerminalAgent(string col, readString read)
            : base(col)
        {
            this.read = read;
        }

        public override Tuple<uint, uint, uint, uint> getInput(Board B)
        {

            string inp = "";
            inp = read();
            if (inp.Length > 3)
            {
                uint xFrom = (uint)inp[0] - 48;
                uint yFrom = (uint)inp[1] - 48;
                uint xTo = (uint)inp[2] - 48;
                uint yTo = (uint)inp[3] - 48;

                return new Tuple<uint, uint, uint, uint>(xFrom, yFrom, xTo, yTo);
            }
            else
            {
                return new Tuple<uint, uint, uint, uint>(10, 10, 10, 10);
            }
        }
    }
}