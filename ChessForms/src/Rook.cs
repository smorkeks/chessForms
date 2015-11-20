using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    // The Rook piece class. It knows it's position, value and score matrix used by the AI
    public class Rook : Piece
    {
        public Rook(uint x, uint y, string c)
            : base(x, y, 500, c)
        {
            this.reward = new int[,] { {  0,  0,  0,  0,  0,  0,  0, 0 },
                                       {  5, 10, 10, 10, 10, 10, 10, 5 },
                                       { -5,  0,  0,  0,  0,  0,  0,-5 },
                                       { -5,  0,  0,  0,  0,  0,  0,-5 },
                                       { -5,  0,  0,  0,  0,  0,  0,-5 },
                                       { -5,  0,  0,  0,  0,  0,  0,-5 },
                                       { -5,  0,  0,  0,  0,  0,  0,-5 },
                                       {  0,  0,  0,  5,  5,  0,  0, 0 },};
        }

        public Rook() : base() { }

        public override Piece getCopyPiece()
        {
            return new Rook();
        }
    }
}