using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{

    // The King piece class. It knows it's position, value and score matrix used by the AI
    public class King : Piece
    {
        public King(uint x, uint y, string c) : base(x, y, 20000, c)
        {
            reward = new int[,] { { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -30, -40, -40, -50, -50, -40, -40, -30 },
                                             { -20, -30, -30, -40, -40, -30, -30, -20 },
                                             { -10, -20, -20, -20, -20, -20, -20, -10 },
                                             {  20,  20,   0,   0,   0,   0,  20,  20 },
                                             {  20,  30,  10,   0,   0,  10,  30,  20 }};
        }

        public King() : base() { }

        public override Piece getCopyPiece()
        {
            return new King();
        }
    }
}