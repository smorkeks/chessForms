using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    // The Pawn piece class. It knows it's position, value and score matrix used by the AI
    public class Pawn : Piece
    {
        uint didDoubleStepTurn; // Used only by pawn in en passant check

        public Pawn(uint x, uint y, string col)
            : base(x, y, 100, col)
        {
            didDoubleStepTurn = 0;
            reward = new int[,] { { 0,   0,  0,  0,  0,  0,  0,  0 },
                                             { 50, 50, 50, 50, 50, 50, 50, 50 },
                                             { 10, 10, 20, 30, 30, 20, 10, 10 },
                                             {  5,  5, 10, 25, 25, 10,  5,  5 },
                                             {  0,  0,  0, 20, 20,  0,  0,  0 },
                                             {  5, -5,-10,  0,  0,-10, -5,  5 },
                                             {  5, 10, 10,-20,-20, 10, 10,  5 },
                                             {  0,  0,  0,  0,  0,  0,  0,  0 } };
        }

        public Pawn() : base() { }

        public uint getDoubleStepTurn()
        {
            return didDoubleStepTurn;
        }

        public void setDoubleStepTurn(uint turn)
        {
            didDoubleStepTurn = turn;
        }


        public override Piece getCopyPiece()
        {
            return new Pawn();
        }

    }
}