using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class Square
    {
        private uint posX, posY;
        private uint whiteCover;
        private uint blackCover;
        private Piece piece;

        public Square(uint x, uint y, int r)
        {
            posX = x;
            posY = y;
            whiteCover = 0;
            blackCover = 0;
        }

        // Getters
        public uint getX()
        {
            return posX;
        }
        public uint getY()
        {
            return posY;
        }

        public uint getWhiteCover()
        {
            return whiteCover;
        }
        public uint getBlackCover()
        {
            return blackCover;
        }

        //returns true if enemy of piece on square has cover there
        public bool getEnemyCover(string col)
        {
            return (((col == "black") && (getWhiteCover() > 0)) || ((col == "white") && (getBlackCover() > 0)));
        }
        public Piece getPiece()
        {
            return piece;
        }

        // Cover modifiers
        public void addWhiteCover(){
            whiteCover++;
        }
        public void removeWhiteCover()
        {
            whiteCover--;
        }
        public void addBlackCover()
        {
            blackCover++;
        }
        public void removeBlackCover()
        {
            blackCover--;
        }
        public void resetCover()
        {
            whiteCover = 0;
            blackCover = 0;
        }

        // Piece
        public void setPiece(Piece p)
        {
            piece = p;
        }
        public void removePiece()
        {
            piece = null;
        }

        

        public void Copy(Square oldSquare)
        {
            posX = oldSquare.posX;
            posY = oldSquare.posY;
            whiteCover = oldSquare.whiteCover;
            blackCover = oldSquare.blackCover;
            if (oldSquare.piece != null)
            {
                piece = oldSquare.piece.getCopyPiece();
                piece.Copy(oldSquare.piece);
            }
            else
            {
                piece = null;
            }
                
        }
    }
}