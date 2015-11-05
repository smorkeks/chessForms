using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.src
{
    public class TerminalAgent : Agent
    {
        //Methods
        public TerminalAgent(string col) : base(col) {}

        public override Tuple<uint, uint, uint, uint> getInput(Board B, string inp)
        {
            uint xFrom = (uint)inp[0];
            uint yFrom = (uint)inp[1];
            uint xTo = (uint)inp[2];
            uint yTo = (uint)inp[3];
            return new Tuple<uint, uint, uint, uint>(xFrom, yFrom, xTo, yTo);
        }

    }
}