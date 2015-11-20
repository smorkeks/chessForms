using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public abstract class Piece
    {
        // fields
        protected uint xCoord;
        protected uint yCoord;
        protected uint score;                   //Value of the piece
        protected bool hasMoved;                //Determines if the piece has moved from initial pos
        protected string colour;                //White or black
        protected int[,] reward;         //Determines how good a square is to be on


        // methods
        public Piece(uint x, uint y, uint val, string col)
        {
            this.xCoord = x;
            this.yCoord = y;
            this.score = val;
            this.colour = col;
            this.hasMoved = false;
        }

        public Piece() { }

        public void move(uint x, uint y)
        {
            this.xCoord = x;
            this.yCoord = y;
            if (!hasMoved)
                hasMoved = true;
        }

        public bool movedFromInit()
        {
            return hasMoved;
        }

        //Checks if square is inside the bounds of the board
        public bool withinBoard(int x, int y)
        {
            if ((x <= 7) & (x >= 0) & (y <= 7) & (y >= 0))
                return true;
            else
                return false;
        }

        //checks if same colour
        public bool isSameColour(Piece P)
        {
            if (P.getColour() == this.colour)
                return true;
            else
                return false;
        }

        public string getColour()
        {
            return this.colour;
        }

        public uint getX()
        {
            return xCoord;
        }

        public uint getY()
        {
            return yCoord;
        }

        public uint getScore()
        {
            return score;
        }

        public void Copy(Piece oldPiece)
        {
            xCoord = oldPiece.xCoord;
            yCoord = oldPiece.yCoord;
            hasMoved = oldPiece.hasMoved;
            colour = oldPiece.colour;
            score = oldPiece.score;
            reward = oldPiece.reward;
        }

        public abstract Piece getCopyPiece();

        // Returns the value of the piece added to the score of the square it's standing on
        public int getValue()
        {
            if (colour == "white")
                return (int)score + reward[xCoord, yCoord];
            else
                return (int)score + reward[xCoord, 7 - yCoord];
        }

        // Used by load.
        public void setHasMoved(bool mov)
        {
            hasMoved = mov;
        }
    }
}