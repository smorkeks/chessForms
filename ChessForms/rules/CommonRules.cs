﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    class CommonRules
    {

        // Filters out moves that would result in a check on the king.
        public static void checkFilter(ref List<Tuple<uint, uint>> moves, Board board, Piece piece)
        {
            string colour = piece.getColour();

            // Find king square
            Square kingSquare = null;
            for (uint j = 0; j < Board.BOARD_SIZE_Y; j++)
            {
                for (uint i = 0; i < Board.BOARD_SIZE_X; i++)
                {
                    kingSquare = board.getSquareAt(i, j);
                    if (kingSquare.getPiece() is King &&
                        kingSquare.getPiece().getColour() == colour)
                    {
                        break;
                    }
                }
                if (kingSquare.getPiece() is King &&
                        kingSquare.getPiece().getColour() == colour)
                {
                    break;
                }
            }

            // Get king position
            uint xKing = kingSquare.getX();
            uint yKing = kingSquare.getY();

            // Check if king has more than one threat
            if ((colour == "white" && kingSquare.getBlackCover() > 1) ||
                (colour == "black" && kingSquare.getWhiteCover() > 1))
            {
                moves.Clear();
            }
            else if ((colour == "white" && kingSquare.getBlackCover() == 1) ||
                     (colour == "black" && kingSquare.getWhiteCover() == 1))
            {
                Piece threat = null;

                // Get all squares with cover of king
                Piece possibleThreat;
                for (uint j = 0; j < Board.BOARD_SIZE_Y; j++)
                {
                    for (uint i = 0; i < Board.BOARD_SIZE_X; i++)
                    {
                        possibleThreat = board.getPieceAt(j, i);
                        if (possibleThreat != null && possibleThreat.getColour() != colour)
                        {
                            // Enemy piece, check if threat
                            List<Tuple<uint, uint>> cover = Rules.getCover(board, possibleThreat);
                            foreach (Tuple<uint, uint> t in cover)
                            {
                                if (t.Item1 == xKing && t.Item2 == yKing)
                                {
                                    // Threat
                                    threat = possibleThreat;
                                    break;
                                }
                            }
                        }
                        if (threat != null)
                            break;
                    }
                    if (threat != null)
                        break;
                }

                // Remove all moves that do not resolve the check
                List<Tuple<uint, uint>> tmpMoves = new List<Tuple<uint, uint>>(moves);
                Piece p = threat;
                if (p is Knight || p is Pawn)
                {
                    // Must take threat
                    foreach (Tuple<uint, uint> move in tmpMoves)
                    {
                        if (move.Item1 != threat.getX() ||
                            move.Item2 != threat.getY())
                        {
                            // Will not take, remove
                            moves.Remove(move);
                        }
                    }
                }

                tmpMoves = new List<Tuple<uint, uint>>(moves);
                if (p is Rook || p is Queen)
                {
                    // Must take or block threat
                    foreach (Tuple<uint, uint> move in tmpMoves)
                    {
                        if (xKing == p.getX())
                        {
                            // Same x
                            if (!(move.Item1 == xKing &&                                   // Move will move to same x
                                  ((yKing > move.Item2 && move.Item2 >= p.getY()) ||       // Move will block or take when King to the right of Rook
                                   (yKing < move.Item2 && move.Item2 <= p.getY()))))       // Move will block or take when King to the left of Rook
                            {
                                // Will not block or take
                                moves.Remove(move);
                            }
                        }
                        else if (yKing == p.getY())
                        {
                            // Same y
                            if (!(move.Item2 == yKing &&                                   // Move will move to same y
                                  ((xKing > move.Item1 && move.Item1 >= p.getX()) ||       // Move will block or take when King above Rook
                                   (xKing < move.Item1 && move.Item1 <= p.getX()))))       // Move will block or take when King below Rook
                            {
                                // Will not block or take
                                moves.Remove(move);
                            }
                        }
                    }
                }

                tmpMoves = new List<Tuple<uint, uint>>(moves);
                if ((p is Bishop || p is Queen) && !(xKing == p.getX() || yKing == p.getY()))
                {
                    // Must take or block threat
                    int xMod = (xKing > p.getX() ? -1 : 1);
                    int yMod = (yKing > p.getY() ? -1 : 1);

                    // Step from King to Threat
                    int steps = (int)Math.Abs((int)xKing - (uint)p.getX());
                    foreach (Tuple<uint, uint> move in tmpMoves)
                    {
                        // Search for any blocking or taking moves
                        int x = (int)xKing;
                        int y = (int)yKing;
                        bool foundGoodMove = false;
                        for (int i = 0; i < steps; i++)
                        {
                            x += xMod;
                            y += yMod;

                            if (move.Item1 == x && move.Item2 == y)
                            {
                                foundGoodMove = true;
                                break;
                            }
                        }

                        // Remove if no good move
                        if (!foundGoodMove)
                        {
                            moves.Remove(move);
                        }
                    }
                }
            }




            // ----------------------------------------------------------------------------------------



            // Make sure that no new check is created by a move
            if (board.getSquareAt(piece.getX(), piece.getY()).getEnemyCover(colour))
            {
                // Empty list of squares with cover of this square
                List<Piece> threats = new List<Piece>();

                // Get all squares with cover of self
                Piece p;
                for (uint j = 0; j < Board.BOARD_SIZE_X; j++)
                {
                    for (uint i = 0; i < Board.BOARD_SIZE_Y; i++)
                    {
                        p = board.getPieceAt(j, i);
                        if (p != null && p.getColour() != colour)
                        {
                            // Remove impossible cases
                            if (p is King || p is Knight || p is Pawn)
                            {
                                continue;
                            }

                            // Enemy piece, check if threat
                            List<Tuple<uint, uint>> cover = Rules.getCover(board, p);
                            foreach (Tuple<uint, uint> t in cover)
                            {
                                if (t.Item1 == piece.getX() && t.Item2 == piece.getY())
                                {
                                    // Threat
                                    threats.Add(p);
                                }
                            }
                        }
                    }
                }

                // Check each threat for possible new check
                Piece realThreat = null;
                int xMod = 0, yMod = 0;
                foreach (Piece threat in threats)
                {
                    int threatX = (int)threat.getX();
                    int threatY = (int)threat.getY();

                    int x = (int)piece.getX();
                    int y = (int)piece.getY();

                    // Find relative position of the threat
                    if (threatX > x) xMod = -1; // Threat to the right
                    else if (threatX == x) xMod = 0;  // Threat at same x
                    else xMod = 1;  // Treat to the left

                    if (threatY > y) yMod = -1; // Threat above
                    else if (threatY == y) yMod = 0;  // Threat at same y
                    else yMod = 1;  // Treat below

                    // Step from self away from threat.
                    // If king is the first piece found, this is the real threatening piece.
                    Piece firstPiece;
                    while (true)
                    {
                        x += xMod;
                        y += yMod;

                        if (!board.withinBoard(x, y))
                        {
                            break;
                        }

                        firstPiece = board.getPieceAt((uint)x, (uint)y);
                        if (firstPiece != null)
                        {
                            if (firstPiece is King && firstPiece.getColour() == piece.getColour())
                            {
                                // This is the friendly king.
                                realThreat = threat;
                            }
                            break;
                        }
                    }

                    if (realThreat != null)
                    {
                        // Real threat found.
                        break;
                    }
                }

                // If a real new threat is found; remove any moves that do not end on the
                // line between this and king.
                if (realThreat != null)
                {
                    int x = (int)realThreat.getX();
                    int y = (int)realThreat.getY();
                    List<Tuple<uint, uint>> possibleMoves = new List<Tuple<uint, uint>>();

                    // First get all the squares that are on a line between the threat piece and the king,
                    // including the threat, excluding the king.
                    while (true)
                    {
                        possibleMoves.Add(new Tuple<uint, uint>((uint)x, (uint)y));
                        x += xMod;
                        y += yMod;
                        if (x == xKing && y == yKing)
                        {
                            // End of possible moves.
                            break;
                        }
                    }

                    // Remove any moves not in possible list.
                    List<Tuple<uint, uint>> tmp = new List<Tuple<uint, uint>>(moves);
                    foreach (Tuple<uint, uint> move in tmp)
                    {
                        if (!possibleMoves.Contains(move))
                        {
                            moves.Remove(move);
                        }
                    }
                }
            }
        }
    }
}
