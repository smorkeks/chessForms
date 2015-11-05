using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.src
{
    public class Pawn : Piece
    {

        public Pawn(uint x, uint y, string col) : base(x, y, 1, col) { }

        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF)
        {
            List<Tuple<uint, uint>> tmpList = new List<Tuple<uint, uint>>();
            short yMod;

            if (this.colour == "white")
                yMod = 1;
            else
                yMod = -1;

            // Take left
            if (withinBoard((int)getX() - 1, (int)getY() + yMod))
            {
                Square S = QF((uint)(getX() - 1), (uint)(getY() + yMod));
                Piece P = S.getPiece();
                if (P != null)
                {
                    if (!isSameColour(P))
                    {
                        tmpList.Add(new Tuple<uint, uint>((uint)(getX() - 1), (uint)(getY() + yMod)));
                    }
                }
            }

            //Take right
            if (withinBoard((int)getX() + 1, (int)getY() + yMod))
            {
                Square S = QF((uint)(getX() + 1), (uint)(getY() + yMod));
                Piece P = S.getPiece();
                if (P != null)
                {
                    if (!isSameColour(P))
                    {
                        tmpList.Add(new Tuple<uint, uint>((uint)(getX() + 1), (uint)(getY() + yMod)));
                    }
                }
            }

            //Move 1 
            if (withinBoard((int)getX(), (int)getY() + yMod))
            {
                Square S = QF(getX(), (uint)(getY() + yMod));
                Piece P = S.getPiece();
                if (P == null)
                {
                    tmpList.Add(new Tuple<uint, uint>(getX(), (uint)(getY() + yMod)));
                }
            }

            //Move 2 (Only first move)
            if (!movedFromInit())
            {
                if (withinBoard((int)getX(), (int)getY() + 2 * yMod))
                {
                    Square S = QF(getX(), (uint)(getY() + 2 * yMod));
                    Piece P = S.getPiece();
                    if (P == null)
                    {
                        tmpList.Add(new Tuple<uint, uint>(getX(), (uint)(getY() + 2 * yMod)));
                    }
                }
            }
            return tmpList;
        }
    }
}