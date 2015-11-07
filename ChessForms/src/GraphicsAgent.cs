using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessForms.src
{
    class GraphicsAgent : Agent
    {
        public delegate Tuple<int,int,int,int> readMove();
        readMove read;

        public GraphicsAgent(string col, readMove read)
            : base(col)
        {
            this.read = read;
        }

        public override Tuple<uint, uint, uint, uint> getInput(Board B)
        {
            Tuple<int, int, int, int> move = read();
            if (move.Item1 != -1 && move.Item2 != -1 && move.Item3 != -1 && move.Item4 != -1)
            {
                return new Tuple<uint, uint, uint, uint>((uint)move.Item1,
                                                         (uint)move.Item2,
                                                         (uint)move.Item3,
                                                         (uint)move.Item4);
            }
            else
            {
                return new Tuple<uint, uint, uint, uint>(10, 10, 10, 10);
            }
        }
    }
}
