using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    // Contains Pawn specific rules
    class PawnRules
    {

        // Returns a list of all possible moves
        public static List<Tuple<uint, uint>> getPossibleMoves(Board board, Piece piece)
        {
            List<Tuple<uint, uint>> tmpList = new List<Tuple<uint, uint>>();
            short yMod;
            short passantRow;
            if (piece.getColour() == "white")
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
            if (board.withinBoard((int) piece.getX() - 1, (int) piece.getY() + yMod))
            {
                Piece P = board.getPieceAt((uint)(piece.getX() - 1), (uint)(piece.getY() + yMod));
                if (P != null)
                {
                    if (!piece.isSameColour(P))
                    {
                        tmpList.Add(new Tuple<uint, uint>((uint)(piece.getX() - 1), (uint)(piece.getY() + yMod)));
                    }
                }
            }

            //Take right
            if (board.withinBoard((int)piece.getX() + 1, (int)piece.getY() + yMod))
            {
                Piece P = board.getPieceAt((uint)(piece.getX() + 1), (uint)(piece.getY() + yMod));
                if (P != null)
                {
                    if (!piece.isSameColour(P))
                    {
                        tmpList.Add(new Tuple<uint, uint>((uint)(piece.getX() + 1), (uint)(piece.getY() + yMod)));
                    }
                }
            }

            //Move 1 
            if (board.withinBoard((int)piece.getX(), (int)piece.getY() + yMod))
            {
                Piece P = board.getPieceAt(piece.getX(), (uint)(piece.getY() + yMod));
                if (P == null)
                {
                    tmpList.Add(new Tuple<uint, uint>(piece.getX(), (uint)(piece.getY() + yMod)));
                }
            }

            //Move 2 (Only first move)
            if (!piece.movedFromInit())
            {
                if (board.withinBoard((int)piece.getX(), (int)piece.getY() + 2 * yMod))
                {
                    Piece P = board.getPieceAt(piece.getX(), (uint)(piece.getY() + 2 * yMod));
                    Piece P2 = board.getPieceAt(piece.getX(), (uint)(piece.getY() + yMod));
                    if (P == null && P2 == null)
                    {
                        tmpList.Add(new Tuple<uint, uint>(piece.getX(), (uint)(piece.getY() + 2 * yMod)));
                        //((Pawn)piece).setDoubleStepTurn(board.getTurn());
                    }
                }
            }

            //En Passant
            if (piece.getY() == passantRow)
            {
                if ((piece.getX() + 1) < Board.BOARD_SIZE_X)
                {
                    Piece P1 = board.getPieceAt(piece.getX() + 1, piece.getY());

                    if (P1 != null)
                    {
                        if (P1 is Pawn)
                            if (((Pawn)P1).getDoubleStepTurn() == board.getTurn() - 1)
                                tmpList.Add(new Tuple<uint, uint>(piece.getX() + 1, (uint)(piece.getY() + yMod)));
                    }
                }

                if ((int)piece.getX() - 1 >= 0)
                {
                    Piece P2 = board.getPieceAt(piece.getX() - 1, piece.getY());
                    if (P2 != null)
                    {
                        if (P2 is Pawn)
                            if (((Pawn)P2).getDoubleStepTurn() == board.getTurn() - 1)
                                tmpList.Add(new Tuple<uint, uint>(piece.getX() - 1, (uint)(piece.getY() + yMod)));
                    }
                }

            }

            // Filter for check situations
            CommonRules.checkFilter(ref tmpList, board, piece);

            return tmpList;
        }

        // Returns a list of all covered squares
        public static List<Tuple<uint, uint>> getCover(Board board, Piece piece)
        {
            int x, y;
            int yMod = (piece.getColour() == "white" ? 1 : -1);
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>();

            x = (int)piece.getX() - 1;
            y = (int)piece.getY() + yMod;
            if (board.withinBoard(x, y))
            {
                cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
            }

            x = (int)piece.getX() + 1;
            if (board.withinBoard(x, y))
            {
                cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
            }

            return cover;
        }

        //Checks if move is possible
        public static bool movePossible(Board board, Piece piece, Move move)
        {
            List<Tuple<uint, uint>> tmp;
            tmp = getPossibleMoves(board, piece);
            foreach (Tuple<uint, uint> item in tmp)
            {
                if ((item.Item1 == move.ToX) &&
                    (item.Item2 == move.ToY))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
