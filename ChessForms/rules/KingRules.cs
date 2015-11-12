using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    class KingRules : PieceRules
    {
        public override List<Tuple<uint, uint>> getPossibleMoves(src.Board board, src.Piece piece)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();

            int x = (int)piece.getX();
            int y = (int)piece.getY();

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
                    if (board.withinBoard(nx, ny))
                    {
                        Square s = board.getSquareAt((uint)nx, (uint)ny);
                        if ((piece.getColour() == "white" && s.getBlackCover() == 0) ||
                            (piece.getColour() == "black" && s.getWhiteCover() == 0))
                        {
                            Piece p = s.getPiece();
                            if (p == null)
                            {
                                moves.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                            }
                            else
                            {
                                if (p.getColour() != piece.getColour())
                                {
                                    moves.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                                }
                            }
                        }
                    }
                }
            }

            // Castling
            Square tmp = board.getSquareAt(piece.getX(), piece.getY());
            if ((!piece.movedFromInit()) && (!tmp.getEnemyCover(piece.getColour())))
            {
                for (int i = 1; i < 5; i++)
                {
                    Square s = board.getSquareAt((uint)(x - i), (uint)y);
                    if ((i < 4) && ((s.getPiece() != null) || s.getEnemyCover(piece.getColour())))
                        break;
                    else if ((s.getPiece() != null) && (i == 4))
                        if ((s.getPiece() is Rook) && (!s.getPiece().movedFromInit()))
                            moves.Add(new Tuple<uint, uint>((uint)(x - 2), (uint)y));

                }
                for (int i = 1; i < 4; i++)
                {
                    Square s = board.getSquareAt((uint)(x + i), (uint)y);
                    if ((i < 3) && ((s.getPiece() != null) || s.getEnemyCover(piece.getColour())))
                        break;
                    else if ((s.getPiece() != null) && (i == 3))
                        if ((s.getPiece() is Rook) && (!s.getPiece().movedFromInit()))
                            moves.Add(new Tuple<uint, uint>((uint)(x + 2), (uint)y));

                }
            }

            // Remove squares with enemy king reach
            foreach (Tuple<uint, uint> reach in getEnemyKingReach(board, piece))
            {
                moves.Remove(reach);
            }

            // Done, all moves found
            return moves;
        }

        public override List<Tuple<uint, uint>> getCover(src.Board board, src.Piece piece)
        {
            // Get cover
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>();

            int x = (int)piece.getX();
            int y = (int)piece.getY();

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
                    if (board.withinBoard(nx, ny))
                    {
                        Square s = board.getSquareAt((uint)nx, (uint)ny);
                        if ((piece.getColour() == "white" && s.getBlackCover() == 0) ||
                            (piece.getColour() == "black" && s.getWhiteCover() == 0))
                        {
                            Tuple<uint, uint> t = new Tuple<uint, uint>((uint)nx, (uint)ny);
                            if (!getEnemyKingReach(board, piece).Contains(t))
                            {
                                cover.Add(t);
                            }
                        }
                    }
                }
            }

            return cover;
        }

        private List<Tuple<uint, uint>> getEnemyKingReach(Board board, Piece piece)
        {
            // Find enemy king
            List<Tuple<uint, uint>> enemyKingReach = new List<Tuple<uint, uint>>();
            bool found = false;
            for (uint j = 0; j < 8; j++)
            {
                for (uint i = 0; i < 8; i++)
                {
                    Piece p = board.getPieceAt(i, j);
                    if (p != null && p is King && p.getColour() != piece.getColour())
                    {
                        found = true;
                        enemyKingReach = getReach(board, p);
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

        public List<Tuple<uint, uint>> getReach(Board board, Piece piece)
        {
            List<Tuple<uint, uint>> reach = new List<Tuple<uint, uint>>();

            int x = (int)piece.getX();
            int y = (int)piece.getY();

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
                    if (board.withinBoard(nx, ny))
                    {
                        reach.Add(new Tuple<uint, uint>((uint)nx, (uint)ny));
                    }
                }
            }

            return reach;
        }
    }
}
