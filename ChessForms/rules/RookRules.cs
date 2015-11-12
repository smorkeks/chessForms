﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    class RookRules : PieceRules
    {
        public override List<Tuple<uint, uint>> getPossibleMoves(src.Board board, src.Piece piece)
        {
            List<Tuple<uint, uint>> moves = new List<Tuple<uint, uint>>();

            // Check down moves
            int x = (int)piece.getX();
            int y = (int)piece.getY();
            while (true)
            {
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

            // Check up moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
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

            // Check right moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x++;
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

            // Check left moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x--;
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
            checkFilter(ref moves, board, piece);

            // Done, all moves found
            return moves;
        }

        public override List<Tuple<uint, uint>> getCover(src.Board board, src.Piece piece)
        {
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>() { };

            // Check left moves
            int x = (int)piece.getX();
            int y = (int)piece.getY();
            while (true)
            {
                x--;
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

            // Check right moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
                x++;
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

            // Check up moves
            x = (int)piece.getX();
            y = (int)piece.getY();
            while (true)
            {
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

            return cover;
        }
        
    }
}
