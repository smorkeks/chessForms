using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    // The Knight piece class. It knows it's position, value and score matrix used by the AI
    public class Knight : Piece
    {

        public Knight(uint x, uint y, string col)
            : base(x, y, 320, col)
        {
            reward = new int[,] { { -50, -40, -30, -30, -30, -30, -40, -50 },
                                             { -40, -20,   0,   0,   0,   0, -20, -40 },
                                             { -30,   0,  10,  15,  15,  10,   0, -30 },
                                             { -30,   5,  15,  20,  20,  15,   5, -30 },
                                             { -30,   5,  15,  20,  20,  15,   5, -30 },
                                             { -30,   0,  10,  15,  15,  10,   0, -30 },
                                             { -40, -20,   0,   5,   5,   0, -20, -40 },
                                             { -50, -40, -30, -30, -30, -30, -40, -50 }, };
        }

        public Knight() : base() { }

        public override Piece getCopyPiece()
        {
            return new Knight();
        }
    }
}