using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class Bishop : Piece
    {
        public Bishop(uint x, uint y, string c) : base(x, y, 330, c)
        {
            reward = new int[,] { { -20, -10, -10, -10, -10, -10, -10, -20 },
                                  { -10,   0,   0,   0,   0,   0,   0, -10 },
                                  { -10,   0,   5,  10,  10,   5,   0, -10 },
                                  { -10,   5,   5,  10,  10,   5,   5, -10 },
                                  { -10,   0,  10,  10,  10,  10,   0, -10 },
                                  { -10,  10,  10,  10,  10,  10,  10, -10 },
                                  { -10,   5,   0,   0,   0,   0,   5, -10 },
                                  { -20, -10, -10, -10, -10, -10, -10, -20 }};
        }


        public Bishop() : base() { }

        public override Piece getCopyPiece()
        {
            return new Bishop();
        }
    }
}