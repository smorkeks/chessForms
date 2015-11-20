using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.AI;

namespace ChessForms.src
{
    // AI agent, does an alpha-beta search of the board and returns the best possible move
    class AiAgent : Agent
    {
        uint difficulty; // Search depth
        uint repetitionMod = 0; // Used in late game to break out of repeated moves
        bool lateGameMod = false;
        Move lastMove;
        Move secondToLastMove;

        MinMax.putScore put;

        public AiAgent(string col, uint diff, MinMax.putScore put)
            : base(col)
        {
            this.put = put;
            difficulty = diff;
        }

        // Searches for a new best move
        public override Move getInput(Board B)
        {
            MinMax minMaxSearch = new MinMax();
            Move bestMove;

            put(0);

            bestMove = minMaxSearch.runMinMax(B, colour, getSearchDepth(B), true, MinMax.MINIMUM, MinMax.MAXIMUM, put).Item1;
            
            if (bestMove != null)
            {
                if (bestMove.Equals(secondToLastMove))
                {
                    repetitionMod += 1;
                }
                else
                {
                    repetitionMod = 0;
                }

                secondToLastMove = lastMove;
                lastMove = bestMove;

                return bestMove;
            }
            else
            {
                repetitionMod += 1;
                return new Move();
            }
        }

        // Returns the depth of the search
        private uint getSearchDepth(Board B)
        {
            if (!lateGameMod && B.getNumPieces() < 10)
            {
                lateGameMod = true;
            }

            uint depth = difficulty;
            if (lateGameMod)
            {
                depth += 1;
            }

            depth += repetitionMod;

            return depth;
        }


        // Returns search depth
        public uint getDifficulty()
        {
            return difficulty;
        }
    }
}
