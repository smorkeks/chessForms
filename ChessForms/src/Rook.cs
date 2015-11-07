using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class Rook : Piece
    {
        public Rook(uint x, uint y, string c) : base(x, y, 5, c) { }

        public Rook() : base() { }


        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF, Board.ListFunc LF, uint turn)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();

            // Check down moves
            int x = (int)getX();
            int y = (int)getY();
            while (true)
            {
                y--;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check up moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                y++;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check right moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x++;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check left moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x--;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Filter for check situations
            checkFilter(ref moves, QF, LF);

            // Done, all moves found
            return moves;
        }

        public override List<Tuple<uint, uint>> getCover(Board.QueryFunc QF)
        {
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>() { };

            // Check left moves
            int x = (int)getX();
            int y = (int)getY();
            while (true)
            {
                x--;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
            }

            // Check right moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x++;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
            }

            // Check up moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                y++;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
            }

            // Check left, up moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                y--;
                if (!withinBoard(x, y))
                {
                    break;
                }

                Piece p = QF((uint)x, (uint)y).getPiece();
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
            }

            return cover;
        }

        public override Piece getCopyPiece()
        {
            return new Rook();
        }
    }
}