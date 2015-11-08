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
        MinMax.putScore put;

        public AiAgent(string col, uint diff, MinMax.putScore put)
            : base(col)
        {
            this.put = put;
            difficulty = diff;
        }

        public override Tuple<uint, uint, uint, uint> getInput(Board B)
        {
            uint diffMod = 0;
            if (B.getNumPieces() < 10)
            {
                diffMod = 2;
            }

            MinMax move = new MinMax();
            Tuple<uint,uint,uint,uint> bestMove = move.runMinMax(B, colour, difficulty + diffMod, true, MinMax.MINIMUM, MinMax.MAXIMUM, put).Item1;
            if (bestMove != null) {
                return bestMove;
            } else {
                return new Tuple<uint, uint, uint, uint>(10, 10, 10, 10);
            }
        }
    }
}
