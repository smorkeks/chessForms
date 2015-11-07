using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.AI;

namespace ChessForms.src
{
    class AiAgent : Agent
    {
        uint difficulty;
        public AiAgent(string col)
            : base(col)
        {
            difficulty = 1;
        }

        public override Tuple<uint, uint, uint, uint> getInput(Board B)
        {
            MinMax move = new MinMax();
            Tuple<uint,uint,uint,uint> bestMove = move.runMinMax(B, colour, 4, true).Item1;
            if (bestMove != null) {
                return bestMove;
            } else {
                return new Tuple<uint, uint, uint, uint>(10, 10, 10, 10);
            }
        }
    }
}
