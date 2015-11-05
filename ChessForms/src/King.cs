using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.src
{
    public class King : Piece
    {
        public King(uint x, uint y, string c) : base(x, y, 100, c) { }


        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();

            // Check down moves
            int x = (int)getX();
            int y = (int)getY();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nx = x + i;
                    int ny = y + j;
                    if (withinBoard(nx, ny))
                    {
                        Square s = QF((uint) nx, (uint) ny);
                        if((getColour() == "white" && s.getBlackCover() == 0) ||
                           (getColour() == "black" && s.getWhiteCover() == 0))
                        {
                            Piece p = s.getPiece();
                            if (p == null)
                            {
                                moves.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                            }
                            else
                            {
                                if (p.getColour() != getColour()) {
                                    moves.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                                }
                            }
                        }
                    }
                }
            }

            // Done, all moves found
            return moves;
        }
    }
}