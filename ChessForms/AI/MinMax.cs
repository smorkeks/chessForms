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
        public delegate void putScore(int score);
        public const int MINIMUM = -10000000;
        public const int MAXIMUM = 10000000;

        public Tuple<Move, int> runMinMax(Board board, string activePlayer, uint depth, bool max, int alpha, int beta, putScore put)
        {
            // TODO: Test, remove when done
            put(1);

            string nonActivePlayer = "";
            if (activePlayer == "white")
            {
                nonActivePlayer = "black";
            }
            else
            {
                nonActivePlayer = "white";
            }

            // First check if this is leaf node
            if (depth <= 0)
            {
                return new Tuple<Move, int>(null, (max ? board.getScore(activePlayer) : board.getScore(nonActivePlayer)));
            }

            // Not a leaf node, branch out

            List<Move> moves = new List<Move>();

            

            // Get moves from active player
            if (activePlayer == "white")
            {
                moves = ChessForms.rules.Rules.getWhiteMoves(board);
            }
            else
            {
                moves = ChessForms.rules.Rules.getBlackMoves(board);
            }

            // Check if no legal moves
            if (moves.Count == 0)
            {
                return new Tuple<Move, int>(null, (max ? MINIMUM : MAXIMUM));
            }

            // Assume horrible best
            int bestScore = (max ? MINIMUM : MAXIMUM);
            Tuple<Move, int> bestResult = new Tuple<Move, int>(null, bestScore);

            // Loop through all moves and minmax them
            Board nextBoard = new Board(board.getTurn());
            Tuple<Move, int> nextResult;
            foreach (Move move in moves)
            {
                nextBoard.Copy(board);
                nextBoard.makeMove(activePlayer, move.FromX, move.FromY, move.ToX, move.ToY);
                nextResult = runMinMax(nextBoard, nonActivePlayer, depth - 1, !max, alpha, beta, put);

                // If new best result, then change bestResult.
                if ((nextResult.Item2 > bestResult.Item2 && max) ||
                    (nextResult.Item2 < bestResult.Item2 && !max))
                {
                    bestResult = new Tuple<Move, int>(move, nextResult.Item2);
                }

                // Alpha-Beta pruning
                if (max)
                {
                    alpha = Math.Max(alpha, bestResult.Item2);
                }
                else
                {
                    beta = Math.Min(beta, bestResult.Item2);
                }
                if (beta <= alpha)
                {
                    // Need not explore subtree.
                    break;
                }
               
                // Run events to not stall the entire application while searching.
                Application.DoEvents();
            }

            //put(bestResult.Item2);

            return bestResult;
        }




    }
}
