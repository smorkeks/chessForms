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

        public override Move getInput(Board B)
        {

            string inp = "";
            inp = read();
            if (inp.Length > 3)
            {
                //uint xFrom = (uint)inp[0] - 48 - 1;
                //uint yFrom = (uint)inp[1] - 48 - 1;
                //uint xTo = (uint)inp[2] - 48 - 1;
                //uint yTo = (uint)inp[3] - 48 - 1;

                uint xFrom = decodeInput(inp[0]);
                uint yFrom = decodeInput(inp[1]);
                uint xTo = decodeInput(inp[2]);
                uint yTo = decodeInput(inp[3]);

                return new Move(xFrom, yFrom, xTo, yTo);
            }
            else
            {
                return new Move(10, 10, 10, 10);
            }
        }

        private uint decodeInput(char input)
        {
            if ('1' <= input && input <= '8')
            {
                return (uint) (input - '0') - 1;
            }
            else if ('A' <= input && input <= 'H')
            {
                return (uint) (input - 'A');
            }
            else
            {
                return 10;
            }
        }
    }
}