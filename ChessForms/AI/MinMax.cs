using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.AI
{
    class MinMax
    {
        public Tuple<Tuple<uint, uint, uint, uint>, int> runMinMax(Board board, string activePlayer, uint depth, ref TreeNode parent, bool max)
        {
            List<Tuple<uint, uint, uint, uint>> moves = new List<Tuple<uint, uint, uint, uint>>();
            Board testBoard = new Board();
            int score = 0;
            Tuple<Tuple<uint, uint, uint, uint>, int> result;
            result = new Tuple<Tuple<uint, uint, uint, uint>, int>(null, score);

            if ((max && activePlayer == "white") || (!max && activePlayer == "black"))
            {
                bool first = true;
                moves = board.getWhiteMoves();
                Tuple<Tuple<uint, uint, uint, uint>, int> tmpResult;
                foreach (Tuple<uint, uint, uint, uint> move in moves)
                {
                    if (first)
                    {
                        result = new Tuple<Tuple<uint, uint, uint, uint>, int>(move, score);
                        first = false;
                    }
                    testBoard.Copy(board);
                    testBoard.makeMove("white", move.Item1, move.Item2, move.Item3, move.Item4);
                    score = testBoard.getScore("white");
                    tmpResult = new Tuple<Tuple<uint, uint, uint, uint>, int>(move, score); 
                    TreeNode child = new TreeNode(score, parent, move, testBoard);
                    parent.addChild(child);//?
                    if (depth > 0)
                    {
                        tmpResult = runMinMax(testBoard, activePlayer, depth - 1, ref child, !max);
                    }
                    if (tmpResult.Item2 > result.Item2 && max)
                        result = tmpResult;
                    else if (tmpResult.Item2 < result.Item2 && !max)
                        result = tmpResult;
                }
            }
            else
            {
                bool first = true;
                moves = board.getBlackMoves();
                Tuple<Tuple<uint, uint, uint, uint>, int> tmpResult;
                foreach (Tuple<uint, uint, uint, uint> move in moves)
                {
                    if (first)
                    {
                        result = new Tuple<Tuple<uint, uint, uint, uint>, int>(move, score);
                        first = false;
                    }
                    testBoard.Copy(board);
                    testBoard.makeMove("black", move.Item1, move.Item2, move.Item3, move.Item4);
                    score = testBoard.getScore("black");
                    tmpResult = new Tuple<Tuple<uint, uint, uint, uint>, int>(move, score);
                    TreeNode child = new TreeNode(score, parent, move, testBoard);
                    parent.addChild(child);//?
                    if (depth > 0)
                    {
                        tmpResult = runMinMax(testBoard, activePlayer, depth - 1, ref child, !max);
                    }
                    if (tmpResult.Item2 > result.Item2 && max)
                        result = tmpResult;
                    else if (tmpResult.Item2 < result.Item2 && !max)
                        result = tmpResult;
                }
            }


            return result;
        }




    }
}
