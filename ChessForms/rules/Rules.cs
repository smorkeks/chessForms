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
        PawnRules pawnRules;
        RookRules rookRules;
        KnightRules knightRules;
        BishopRules bishopRules;
        QueenRules queenRules;
        KingRules kingRules;

        public Rules()
        {
            pawnRules = new PawnRules();
            rookRules = new RookRules();
            knightRules = new KnightRules();
            bishopRules = new bishopRules();
            queenRules = new QueenRules();
            kingRules = new KingRules();
        }


        public bool movePossible(Board board, Tuple<uint,uint,uint,uint> move, string activePlayer)
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
                return pawnRules.movePossible(board, p, move);
            }
            else if (p is Rook)
            {
                return rookRules.movePossible(board, p, move);
            }
            else if (p is Knight)
            {
                return knightRules.movePossible(board, p, move);
            }
            else if (p is Bishop)
            {
                return bishopRules.movePossible(board, p, move);
            }
            else if (p is Queen)
            {
                return queenRules.movePossible(board, p, move);
            }
            else if (p is King)
            {
                return kingRules.movePossible(board, p, move);
            }
            
            // Should not get here ever...
            return false;
        }
        
    }
}
