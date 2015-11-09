using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class Pawn : Piece
    {
        uint didDoubleStepTurn;

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

        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF, Board.ListFunc LF, uint turn)
        {
            List<Tuple<uint, uint>> tmpList = new List<Tuple<uint, uint>>();
            short yMod;
            short passantRow;
            if (this.colour == "white")
            {
                yMod = 1;
                passantRow = 4;
            }
            else
            {
                yMod = -1;
                passantRow = 3;
            }

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
                    Square S2 = QF(getX(), (uint)(getY() + yMod));
                    Piece P = S.getPiece();
                    Piece P2 = S2.getPiece();
                    if (P == null && P2 == null)
                    {
                        tmpList.Add(new Tuple<uint, uint>(getX(), (uint)(getY() + 2 * yMod)));
                        didDoubleStepTurn = turn;
                    }
                }
            }

            //En Passant
            if (getY() == passantRow)
            {
                if ((getX() + 1) < 8)
                {
                    Piece P1 = QF(getX() + 1, getY()).getPiece();

                    if (P1 != null)
                    {
                        if (P1 is Pawn)
                            if (((Pawn)P1).getDoubleStepTurn() == turn - 1)
                                tmpList.Add(new Tuple<uint, uint>(getX() + 1, (uint)(getY() + yMod)));
                    }
                }

                if ((int)getX() - 1 >= 0)
                {
                    Square S2 = QF(getX() - 1, getY());
                    Piece P2 = S2.getPiece();
                    if (P2 != null)
                    {
                        if (P2 is Pawn)
                            if (((Pawn)P2).getDoubleStepTurn() == turn - 1)
                                tmpList.Add(new Tuple<uint, uint>(getX() - 1, (uint)(getY() + yMod)));
                    }
                }

            }

            // Filter for check situations
            checkFilter(ref tmpList, QF, LF);

            return tmpList;
        }

        public override List<Tuple<uint, uint>> getCover(Board.QueryFunc QF)
        {
            int x, y;
            int yMod = (colour == "white" ? 1 : -1);
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>();

            x = (int)getX() - 1;
            y = (int)getY() + yMod;
            if (withinBoard(x, y))
            {
                cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
            }

            x = (int)getX() + 1;
            if (withinBoard(x, y))
            {
                cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
            }

            return cover;
        }

        public uint getDoubleStepTurn()
        {
            return didDoubleStepTurn;
        }

        public override Piece getCopyPiece()
        {
            return new Pawn();
        }

    }
}