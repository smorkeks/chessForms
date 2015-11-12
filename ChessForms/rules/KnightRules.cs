using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    class KnightRules : PieceRules
    {

        //Contains all possible jump locations, relative to piece
        private List<Tuple<int, int>> jmpList;

        public KnightRules() : base()
        {
            jmpList = new List<Tuple<int, int>>();
            jmpList.Add(new Tuple<int, int>(1, 2));
            jmpList.Add(new Tuple<int, int>(1, -2));
            jmpList.Add(new Tuple<int, int>(2, 1));
            jmpList.Add(new Tuple<int, int>(2, -1));
            jmpList.Add(new Tuple<int, int>(-1, 2));
            jmpList.Add(new Tuple<int, int>(-1, -2));
            jmpList.Add(new Tuple<int, int>(-2, 1));
            jmpList.Add(new Tuple<int, int>(-2, -1));
        }

        public override List<Tuple<uint, uint>> getPossibleMoves(Board board, Piece piece)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();
            int px = (int) piece.getX();
            int py = (int) piece.getY();

            // Loops over jump locations
            foreach (Tuple<int, int> item in jmpList)
            {
                int jmpX = item.Item1 + px;
                int jmpY = item.Item2 + py;
                if (board.withinBoard(jmpX, jmpY))
                {
                    Piece P = board.getPieceAt((uint) jmpX, (uint) jmpY);
                    if (P != null)
                    {
                        if (!piece.isSameColour(P))
                        {
                            moves.Add(new Tuple<uint, uint>((uint)jmpX, (uint)jmpY));
                        }
                    }
                    else
                    {
                        moves.Add(new Tuple<uint, uint>((uint)jmpX, (uint)jmpY));
                    }
                }
            }

            // Filter for check situations
            checkFilter(ref moves, board, piece);

            return moves;
        }

        public override List<Tuple<uint, uint>> getCover(Board board, Piece piece)
        {
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>();
            int px = (int)piece.getX();
            int py = (int)piece.getY();

            // Loops over jump locations
            foreach (Tuple<int, int> item in jmpList)
            {
                int jmpX = item.Item1 + px;
                int jmpY = item.Item2 + py;
                if (board.withinBoard(jmpX, jmpY))
                {
                    cover.Add(new Tuple<uint, uint>((uint)jmpX, (uint)jmpY));
                }
            }

            return cover;
        }
    }
}
