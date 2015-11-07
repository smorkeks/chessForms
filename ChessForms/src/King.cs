using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class King : Piece
    {
        public King(uint x, uint y, string c) : base(x, y, 100, c) { }

        public King() : base() { }

        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF, uint turn)
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
                        Square s = QF((uint)nx, (uint)ny);
                        if ((getColour() == "white" && s.getBlackCover() == 0) ||
                           (getColour() == "black" && s.getWhiteCover() == 0))
                        {
                            Piece p = s.getPiece();
                            if (p == null)
                            {
                                moves.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                            }
                            else
                            {
                                if (p.getColour() != getColour())
                                {
                                    moves.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                                }
                            }
                        }
                    }
                }
            }

            // Castling
            Square tmp = QF((uint)(x), (uint)y);
            if ((!hasMoved) && (!tmp.getEnemyCover(colour)))
            {
                for (int i = 1; i < 5; i++)
                {
                    Square s = QF((uint)(x - i), (uint)y);
                    if ((i < 3) && ((s.getPiece() != null) || s.getEnemyCover(colour)))
                        break;
                    else if ((s.getPiece() != null) && (i == 4))
                        if ((s.getPiece() is Rook) && (!s.getPiece().movedFromInit()))
                            moves.Add(new Tuple<uint, uint>((uint)(x - 2), (uint)y));

                }
                for (int i = 1; i < 4; i++)
                {
                    Square s = QF((uint)(x + i), (uint)y);
                    if ((i < 2) && ((s.getPiece() != null) || s.getEnemyCover(colour)))
                        break;
                    else if ((s.getPiece() != null) && (i == 3))
                        if ((s.getPiece() is Rook) && (!s.getPiece().movedFromInit()))
                            moves.Add(new Tuple<uint, uint>((uint)(x + 2), (uint)y));

                }


            }

            // Done, all moves found
            return moves;
        }

        public override List<Tuple<uint, uint>> getCover(Board.QueryFunc QF)
        {
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>() { };

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
                        Square s = QF((uint)nx, (uint)ny);
                        if ((getColour() == "white" && s.getBlackCover() == 0) ||
                           (getColour() == "black" && s.getWhiteCover() == 0))
                        {
                            cover.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                        }
                    }
                }
            }

            return cover;
        }

        public override Piece getCopyPiece()
        {
            return new King();
        }
    }
}