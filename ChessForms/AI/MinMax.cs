using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;
using System.Windows.Forms;

namespace ChessForms.AI
{
    class MinMax
    {
        public Tuple<Tuple<uint, uint, uint, uint>, int> runMinMax(Board board, string activePlayer, uint depth, bool max)
        {
            // First check if this is leaf node
            if (depth <= 0)
            {
                return new Tuple<Tuple<uint, uint, uint, uint>, int> (null, board.getScore(activePlayer));
            }

            // Not a leaf node, branch out

            List<Tuple<uint, uint, uint, uint>> moves = new List<Tuple<uint, uint, uint, uint>>();
            
            string nonActivePlayer = "";

            // Get moves from active player
            if (activePlayer == "white")
            {
                nonActivePlayer = "black";
                moves = board.getWhiteMoves();
            }
            else
            {
                nonActivePlayer = "white";
                moves = board.getBlackMoves();
            }

            // Check if no legal moves
            if (moves.Count == 0)
            {
                return new Tuple<Tuple<uint, uint, uint, uint>, int>(null, board.getScore(activePlayer));
            }

            // Assume horrible best
            int bestScore = (max ? -100000 : 100000);
            Tuple<Tuple<uint, uint, uint, uint>, int> bestResult = new Tuple<Tuple<uint, uint, uint, uint>, int>(null, bestScore);

            // Loop through all moves and minmax them
            Board nextBoard = new Board();
            Tuple<Tuple<uint, uint, uint, uint>, int> nextResult;
            foreach (Tuple<uint, uint, uint, uint> move in moves)
            {
                nextBoard.Copy(board);
                nextBoard.makeMove(activePlayer, move.Item1, move.Item2, move.Item3, move.Item4);
                nextResult = runMinMax(nextBoard, nonActivePlayer, depth - 1, !max);
                
                // If max and more or min and less, then change bestResult.
                if ((nextResult.Item2 > bestResult.Item2 && max) ||
                    (nextResult.Item2 < bestResult.Item2 && !max))
                {
                    bestResult = new Tuple<Tuple<uint,uint,uint,uint>,int>(move, bestResult.Item2);
                }

                Application.DoEvents();
            }

            return bestResult;
        }




    }
}
