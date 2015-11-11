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

            }
            else if (p is Rook)
            {

            }
            else if (p is Knight)
            {

            }
            else if (p is Bishop)
            {

            }
            else if (p is Queen)
            {

            }
            else if (p is King)
            {

            }
            else
            {
                // Should not get here ever...
                return false;
            }

            return false;
        }
        
    }
}
