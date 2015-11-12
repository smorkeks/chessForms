using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    class Rules
    {
        // Check if a move is possible on a specific board.
        public static bool movePossible(Board board, Tuple<uint,uint,uint,uint> move, string activePlayer)
        {
            // Check if piece at move.from
            Piece p = board.getPieceAt(move.Item1, move.Item2);
            if (p == null)
            {
                return false;
            }

            // Check if correct colour
            if (p.getColour() != activePlayer)
            {
                return false;
            }

            // Check if move is possible
            if (p is Pawn)
            {
                return PawnRules.movePossible(board, p, move);
            }
            else if (p is Rook)
            {
                return RookRules.movePossible(board, p, move);
            }
            else if (p is Knight)
            {
                return KnightRules.movePossible(board, p, move);
            }
            else if (p is Bishop)
            {
                return BishopRules.movePossible(board, p, move);
            }
            else if (p is Queen)
            {
                return QueenRules.movePossible(board, p, move);
            }
            else if (p is King)
            {
                return KingRules.movePossible(board, p, move);
            }
            
            // Should not get here ever...
            return false;
        }

        // Get all possible moves for a piece.
        public static List<Tuple<uint, uint>> getPossibleMoves(Board board, Piece piece)
        {
            if (piece is Pawn)
            {
                return PawnRules.getPossibleMoves(board, piece);
            }
            else if (piece is Rook)
            {
                return RookRules.getPossibleMoves(board, piece);
            }
            else if (piece is Knight)
            {
                return KnightRules.getPossibleMoves(board, piece);
            }
            else if (piece is Bishop)
            {
                return BishopRules.getPossibleMoves(board, piece);
            }
            else if (piece is Queen)
            {
                return QueenRules.getPossibleMoves(board, piece);
            }
            else if (piece is King)
            {
                return KingRules.getPossibleMoves(board, piece);
            }

            // Should not get here!
            return new List<Tuple<uint, uint>>();
        }

        // Get cover for a piece.
        public static List<Tuple<uint, uint>> getCover(Board board, Piece piece)
        {
            if (piece is Pawn)
            {
                return PawnRules.getCover(board, piece);
            }
            else if (piece is Rook)
            {
                return RookRules.getCover(board, piece);
            }
            else if (piece is Knight)
            {
                return KnightRules.getCover(board, piece);
            }
            else if (piece is Bishop)
            {
                return BishopRules.getCover(board, piece);
            }
            else if (piece is Queen)
            {
                return QueenRules.getCover(board, piece);
            }
            else if (piece is King)
            {
                return KingRules.getCover(board, piece);
            }

            // Should not get here!
            return new List<Tuple<uint, uint>>();
        }

        // Get all moves of a specific colour.
        private static List<Tuple<uint, uint, uint, uint>> getMoves(Board board, string col)
        {
            List<Tuple<uint, uint, uint, uint>> moves = new List<Tuple<uint, uint, uint, uint>>();

            for (uint y = 0; y < Board.BOARD_SIZE_Y; y++)
            {
                for (uint x = 0; x < Board.BOARD_SIZE_X; x++)
                {
                    Piece p = board.getPieceAt(x, y);
                    if (p != null && p.getColour() == col)
                    {
                        List<Tuple<uint, uint>> newMoves = getPossibleMoves(board, p);

                        foreach (Tuple<uint, uint> t in newMoves)
                        {
                            moves.Add(new Tuple<uint, uint, uint, uint>(x, y, t.Item1, t.Item2));
                        }
                    }
                }
            }

            return moves;
        }

        // Get all possible white moves, in the format x1,y1, x2,y2.
        public static List<Tuple<uint, uint, uint, uint>> getWhiteMoves(Board board)
        {
            return getMoves(board, "white");
        }

        // Get all possible black moves, in the format x1,y1, x2,y2.
        public static List<Tuple<uint, uint, uint, uint>> getBlackMoves(Board board)
        {
            return getMoves(board, "black");
        }

        // Check if a player has lost.
        public static bool playerLost(Board board, string col)
        {
            List<Tuple<uint, uint, uint, uint>> moves = getMoves(board, col);
            return (moves.Count == 0 && getCheck(board, col));
        }

        // Returns true if cover on king of colour "col".
        public static bool getCheck(Board board, string col)
        {
            Square kingSquare = null;
            for (uint j = 0; j < 8; j++)
            {
                for (uint i = 0; i < 8; i++)
                {
                    kingSquare = board.getSquareAt(i, j);
                    if (kingSquare.getPiece() is King &&
                        kingSquare.getPiece().getColour() == col)
                    {
                        break;
                    }
                }
                if (kingSquare.getPiece() is King &&
                        kingSquare.getPiece().getColour() == col)
                {
                    break;
                }
            }
            return (kingSquare.getEnemyCover(col));
        }

        // Check if remi
        public static bool remi(Board board)
        {
            List<Tuple<uint, uint, uint, uint>> movesW = getWhiteMoves(board);
            List<Tuple<uint, uint, uint, uint>> movesB = getBlackMoves(board);
            return ((movesW.Count == 0 && !getCheck(board, "white") || (movesB.Count == 0 && !getCheck(board, "black"))));
        }
    }
}
