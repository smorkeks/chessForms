using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessForms.src
{
    // The agent used by human players playing with a graphical interface
    class GraphicsAgent : Agent
    {
        public delegate Move readMove();
        readMove read;

        public GraphicsAgent(string col, readMove read)
            : base(col)
        {
            this.read = read;
        }

        public override Move getInput(Board B)
        {
            return read();
        }
    }
}
