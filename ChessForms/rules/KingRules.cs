using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    //Handles King specific rules
    class KingRules
    {
        public static List<Tuple<uint, uint>> getPossibleMoves(Board board, Piece piece)
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

            // Remove any moves that create new checks
            checkFilter(ref moves, board, piece);

            // Done, all moves found
            return moves;
        }

        private static void checkFilter(ref List<Tuple<uint,uint>> moves, Board board, Piece king)
        {
            uint kx = king.getX();
            uint ky = king.getY();
            
            List<Piece> threats = new List<Piece>();

            // Make sure no new checks are created by King moving.
            if (board.getSquareAt(kx, ky).getEnemyCover(king.getColour()))
            {    
                // King is in check, get all threats
                for (uint y = 0; y < Board.BOARD_SIZE_Y; y++)
                {
                    for (uint x = 0; x < Board.BOARD_SIZE_X; x++)
                    {
                        Piece p = board.getPieceAt(x, y);
                        if (p != null && p.getColour() != king.getColour())
                        {
                            List<Tuple<uint,uint>> cover = Rules.getCover(board, p);
                            if (cover.Contains(new Tuple<uint,uint>(kx, ky)))
                            {
                                threats.Add(p);
                            }
                        }
                    }
                } 
            }

            foreach (Piece threat in threats)
            {
                if (threat is Pawn || threat is King || threat is Knight)
                {
                    continue;
                }

                // Get relative position of threat
                uint tx = threat.getX();
                uint ty = threat.getY();
                int xMod;
                int yMod;
                
                if (tx > kx) xMod = -1;
                else if (tx < kx) xMod = 1;
                else xMod = 0;

                if (ty > ky) yMod = -1;
                else if (ty < ky) yMod = 1;
                else yMod = 0;

                // Get square on the other side of king relative to the threat.
                int x = (int) kx + xMod;
                int y = (int) ky + yMod;
                if (board.withinBoard(x, y))
                {
                    List<Tuple<uint, uint>> tmp = new List<Tuple<uint, uint>>(moves);
                    foreach (Tuple<uint,uint> move in tmp)
                    {
                        if (move.Item1 == x && move.Item2 == y)
                        {
                            moves.Remove(move);
                        }
                    }
                }
            }
        }

        public static List<Tuple<uint, uint>> getCover(src.Board board, src.Piece piece)
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

        // Special King function used to make sure the Kings can't move next to each other
        private static List<Tuple<uint, uint>> getEnemyKingReach(Board board, Piece piece)
        {
            // Find enemy king
            List<Tuple<uint, uint>> enemyKingReach = new List<Tuple<uint, uint>>();
            bool found = false;
            for (uint j = 0; j < Board.BOARD_SIZE_Y; j++)
            {
                for (uint i = 0; i < Board.BOARD_SIZE_X; i++)
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

        // Special King function used to make sure the Kings can't move next to each other
        public static List<Tuple<uint, uint>> getReach(Board board, Piece piece)
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

        //Checks if move is possible
        public static bool movePossible(Board board, Piece piece, Move move)
        {
            List<Tuple<uint, uint>> tmp;
            tmp = getPossibleMoves(board, piece);
            foreach (Tuple<uint, uint> item in tmp)
            {
                if ((item.Item1 == move.ToX) &&
                    (item.Item2 == move.ToY))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
