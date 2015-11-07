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
            TreeNode initial = new TreeNode(0);
            return move.runMinMax(B, colour, 2,ref initial,true).Item1;
        }
    }
}
