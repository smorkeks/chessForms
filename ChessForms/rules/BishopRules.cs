using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    class BishopRules
    {
        public static List<Tuple<uint, uint>> getPossibleMoves(Board board, Piece piece)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();

            // Check down, left moves
            int x = (int)piece.getX();
            int y = (int)piece.getY();
            while (true)
            {
                x--;
                y--;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != piece.getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check down, right moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x++;
                y--;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != piece.getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check up, right moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x++;
                y++;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != piece.getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check left, up moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x--;
                y++;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    if (p.getColour() != piece.getColour())
                    {
                        moves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Filter for check situations
            CommonRules.checkFilter(ref moves, board, piece);

            // Done, all moves found
            return moves;
        }

        public static List<Tuple<uint, uint>> getCover(Board board, Piece piece)
        {
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>() { };

            // Check down, left moves
            int x = (int)piece.getX();
            int y = (int)piece.getY();
            while (true)
            {
                x--;
                y--;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
            }

            // Check down, right moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x++;
                y--;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
            }

            // Check up, right moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x++;
                y++;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
            }

            // Check left, up moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x--;
                y++;

                if (!board.withinBoard(x, y))
                {
                    break;
                }

                Piece p = board.getPieceAt((uint)x, (uint)y);
                if (p == null)
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                }
                else
                {
                    cover.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                    break;
                }
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
