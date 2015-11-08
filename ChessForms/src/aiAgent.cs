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
        GUI gui;

        public AiAgent(string col, MinMax.putScore put)
            : base(col)
        {
            this.gui = gui;
            this.put = put;
            difficulty = 1;
        }

        public override Tuple<uint, uint, uint, uint> getInput(Board B)
        {
            MinMax move = new MinMax();
            Tuple<uint,uint,uint,uint> bestMove = move.runMinMax(B, colour, 4, true, MinMax.MINIMUM, MinMax.MAXIMUM, put).Item1;
            if (bestMove != null) {
                return bestMove;
            } else {
                return new Tuple<uint, uint, uint, uint>(10, 10, 10, 10);
            }
        }
    }
}
