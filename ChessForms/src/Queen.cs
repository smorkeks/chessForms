using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class Queen : Piece
    {
        protected override int[,] reward = new int[,] { { -20, -10, -10, -5, -5, -10, -10, -20 },
                                                        { -10,   0,   0,  0,  0,   0,   0, -10 },
                                                        { -10,   0,   5,  5,  5,   5,   0, -10 },
                                                        { -10,   0,   5,  5,  5,   5,   0, -10 },
                                                        { -10,   0,   5,  5,  5,   5,   0, -10 },
                                                        { -10,   5,   5,  5,  5,   5,   0, -10 },
                                                        { -10,   0,   0,  0,  0,   0,   0, -10 },
                                                        { -20, -10, -10, -5, -5, -10, -10, -20 }};

        public Queen(uint x, uint y, string c) : base(x, y, 900, c) {}

        public Queen() : base() { }

        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF, uint turn)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();

            // Check down, left moves
            int x = (int)getX();
            int y = (int)getY();
            while (true)
            {
                x--;
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

            // Check down, right moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x++;
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

            // Check up, right moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x++;
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

            // Check left, up moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x--;
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

            // Check down moves
            x = (int) getX();
            y = (int) getY();
            while(true)
            {
                y--;
                if (!withinBoard(x,y))
                {
                    break;
                }

                Piece p = QF((uint) x, (uint) y).getPiece();
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint) x, (uint) y));
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
            x = (int) getX();
            y = (int) getY();
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
            x = (int) getX();
            y = (int) getY();
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
            x = (int) getX();
            y = (int) getY();
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
            checkFilter(ref moves, QF);

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

            // Check down, left moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x--;
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

            // Check down, right moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x++;
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

            // Check up, right moves
            x = (int)getX();
            y = (int)getY();
            while (true)
            {
                x++;
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
                x--;
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

            return cover;
        }

        public override Piece getCopyPiece()
        {
            return new Queen();
        }
    }
}