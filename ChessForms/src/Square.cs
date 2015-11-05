using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.src
{
    public class Square
    {
        private uint posX, posY;
        private int reward;
        private uint whiteCover;
        private uint blackCover;
        private Piece piece;

        public Square(uint x, uint y, int r)
        {
            posX = x;
            posY = y;
            reward = r;
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
        public int getReward()
        {
            return reward;
        }
        public uint getWhiteCover()
        {
            return whiteCover;
        }
        public uint getBlackCover()
        {
            return blackCover;
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
        public void setPiece(Piece p)
        {
            piece = p;
        }
        public void removePiece()
        {
            piece = null;
        }
    }
}