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
            int score;
            Tuple<Tuple<uint, uint, uint, uint>, int> result;

            if ((max && activePlayer == "white") || (!max && activePlayer == "black"))
            {
                score = testBoard.getScore("white");
                result = new Tuple<Tuple<uint, uint, uint, uint>, int>(null, score);
                moves = testBoard.getWhiteMoves();
                Tuple<Tuple<uint, uint, uint, uint>, int> tmpResult;
                foreach (Tuple<uint, uint, uint, uint> move in moves)
                {
                    testBoard.Copy(board);
                    tmpResult = new Tuple<Tuple<uint, uint, uint, uint>, int>(move, score);
                    testBoard.makeMove("white", move.Item1, move.Item2, move.Item3, move.Item4);
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
                score = testBoard.getScore("black");
                result = new Tuple<Tuple<uint, uint, uint, uint>, int>(null, score);
                moves = testBoard.getBlackMoves();
                Tuple<Tuple<uint, uint, uint, uint>, int> tmpResult;
                foreach (Tuple<uint, uint, uint, uint> move in moves)
                {
                    testBoard.Copy(board);
                    tmpResult = new Tuple<Tuple<uint, uint, uint, uint>, int>(move, score);
                    testBoard.makeMove("black", move.Item1, move.Item2, move.Item3, move.Item4);
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
