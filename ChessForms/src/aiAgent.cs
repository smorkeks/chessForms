﻿using System;
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
        uint repetitionMod = 0;
        bool lateGameMod = false;
        Tuple<uint, uint, uint, uint> lastMove;
        Tuple<uint, uint, uint, uint> secondToLastMove;

        MinMax.putScore put;

        public AiAgent(string col, uint diff, MinMax.putScore put)
            : base(col)
        {
            this.put = put;
            difficulty = diff;
        }

        public override Tuple<uint, uint, uint, uint> getInput(Board B)
        {
            MinMax move = new MinMax();
            Tuple<uint, uint, uint, uint> bestMove;

            put(0);

            bestMove = move.runMinMax(B, colour, getSearchDepth(B), true, MinMax.MINIMUM, MinMax.MAXIMUM, put).Item1;
            
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
                return new Tuple<uint, uint, uint, uint>(10, 10, 10, 10);
            }
        }

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

        public uint getDifficulty()
        {
            return difficulty;
        }
    }
}
