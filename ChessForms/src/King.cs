using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class King : Piece
    {
        public King(uint x, uint y, string c) : base(x, y, 20000, c)
        {
            reward = new int[,] { { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -20, -30, -30, -40, -40, -30, -30, -20 },
                                             { -10, -20, -20, -20, -20, -20, -20, -10 },
                                             {  20,  20,   0,   0,   0,   0,  20,  20 },
                                             {  20,  30,  10,   0,   0,  10,  30,  20 }};
        }

        public King() : base() { }

        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF, uint turn)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();

            int x = (int)getX();
            int y = (int)getY();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        // Standing still is not a move
                        continue;
                    }
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
            Square tmp = QF(getX(), getY());
            if ((!hasMoved) && (!tmp.getEnemyCover(colour)))
            {
                for (int i = 1; i < 5; i++)
                {
                    Square s = QF((uint)(x - i), (uint)y);
                    if ((i < 4) && ((s.getPiece() != null) || s.getEnemyCover(colour)))
                        break;
                    else if ((s.getPiece() != null) && (i == 4))
                        if ((s.getPiece() is Rook) && (!s.getPiece().movedFromInit()))
                            moves.Add(new Tuple<uint, uint>((uint)(x - 2), (uint)y));

                }
                for (int i = 1; i < 4; i++)
                {
                    Square s = QF((uint)(x + i), (uint)y);
                    if ((i < 3) && ((s.getPiece() != null) || s.getEnemyCover(colour)))
                        break;
                    else if ((s.getPiece() != null) && (i == 3))
                        if ((s.getPiece() is Rook) && (!s.getPiece().movedFromInit()))
                            moves.Add(new Tuple<uint, uint>((uint)(x + 2), (uint)y));

                }
            }

            // Remove squares with enemy king reach
            foreach (Tuple<uint, uint> reach in getEnemyKingReach(QF))
            {
                moves.Remove(reach);
            }

            // Done, all moves found
            return moves;
        }

        private List<Tuple<uint, uint>> getEnemyKingReach(Board.QueryFunc QF)
        {
            // Find enemy king
            List<Tuple<uint, uint>> enemyKingReach = new List<Tuple<uint, uint>>();
            bool found = false;
            for (uint j = 0; j < 8; j++)
            {
                for (uint i = 0; i < 8; i++)
                {
                    Piece p = QF(i, j).getPiece();
                    if (p != null && p is King && p.getColour() != colour)
                    {
                        found = true;
                        enemyKingReach = ((King)p).getReach(QF);
                        break;
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }
            return enemyKingReach;
        }

        public override List<Tuple<uint, uint>> getCover(Board.QueryFunc QF)
        {
            // Get cover
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>();

            int x = (int)getX();
            int y = (int)getY();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        // Do not cover self
                        continue;
                    }
                    int nx = x + i;
                    int ny = y + j;
                    if (withinBoard(nx, ny))
                    {
                        Square s = QF((uint)nx, (uint)ny);
                        if ((getColour() == "white" && s.getBlackCover() == 0) ||
                            (getColour() == "black" && s.getWhiteCover() == 0))
                        {
                            Tuple<uint, uint> t = new Tuple<uint, uint>((uint)nx, (uint)ny);
                            if (!getEnemyKingReach(QF).Contains(t))
                            {
                                cover.Add(t);
                            }
                        }
                    }
                }
            }

            return cover;
        }

        // Returns a list of all reeachable squares, disregarding cover.
        public List<Tuple<uint, uint>> getReach(Board.QueryFunc QF)
        {
            List<Tuple<uint, uint>> reach = new List<Tuple<uint, uint>>();

            int x = (int)getX();
            int y = (int)getY();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        // Do not have reach to self
                        continue;
                    }
                    int nx = x + i;
                    int ny = y + j;
                    if (withinBoard(nx, ny))
                    {
                        reach.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                    }
                }
            }

            return reach;
        }

        public override Piece getCopyPiece()
        {
            return new King();
        }
    }
}