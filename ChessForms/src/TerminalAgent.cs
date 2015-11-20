using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    // Agent used to get text input from human player
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

        // Takes input from textbox converts it to correctly formatted move
        public override Move getInput(Board B)
        {

            string inp = "";
            inp = read();
            if (inp.Length > 3)
            {
                uint xFrom = decodeInput(inp[0]);
                uint yFrom = decodeInput(inp[1]);
                uint xTo = decodeInput(inp[2]);
                uint yTo = decodeInput(inp[3]);

                return new Move(xFrom, yFrom, xTo, yTo);
            }
            else
            {
                return new Move(100, 100, 100, 100);
            }
        }

        // Gets input on form "A2A4" and converts it to the form "0103"
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