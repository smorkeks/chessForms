using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    // The Queen piece class. It knows it's position, value and score matrix used by the AI
    public class Queen : Piece
    {
        public Queen(uint x, uint y, string c) : base(x, y, 900, c)
        {
            reward = new int[,] { { -20, -10, -10, -5, -5, -10, -10, -20 },
                                             { -10,   0,   0,  0,  0,   0,   0, -10 },
                                             { -10,   0,   5,  5,  5,   5,   0, -10 },
                                             { -10,   0,   5,  5,  5,   5,   0, -10 },
                                             { -10,   0,   5,  5,  5,   5,   0, -10 },
                                             { -10,   5,   5,  5,  5,   5,   0, -10 },
                                             { -10,   0,   0,  0,  0,   0,   0, -10 },
                                             { -20, -10, -10, -5, -5, -10, -10, -20 }};
        }

        public Queen() : base() { }

        public override Piece getCopyPiece()
        {
            return new Queen();
        }
    }
}