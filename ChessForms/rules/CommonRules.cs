using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.rules
{
    class CommonRules
    {

        public static void checkFilter(ref List<Tuple<uint, uint>> moves, Board board, Piece piece)
        {
            string colour = piece.getColour();

            // Find king square
            Square kingSquare = null;
            for (uint j = 0; j < 8; j++)
            {
                for (uint i = 0; i < 8; i++)
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
                for (uint j = 0; j < 8; j++)
                {
                    for (uint i = 0; i < 8; i++)
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
                Piece p = threat;   // TODO: make nicer, could use threat
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

                // Seems like a bro
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

            // Make sure that no new check is created by a move
            if (board.getSquareAt(piece.getX(), piece.getY()).getEnemyCover(colour))
            {
                // Empty list of squares with cover of this square
                List<Square> threats = new List<Square>();

                // Get all squares with cover of self
                Square s;
                for (uint j = 0; j < 8; j++)
                {
                    for (uint i = 0; i < 8; i++)
                    {
                        s = board.getSquareAt(j, i);
                        if (s.getPiece() != null && s.getPiece().getColour() != colour)
                        {
                            // Remove impossible cases
                            if (s.getPiece() is King || s.getPiece() is Knight || s.getPiece() is Pawn)
                            {
                                continue;
                            }

                            // Enemy piece, check if threat
                            List<Tuple<uint, uint>> cover = Rules.getCover(board, s.getPiece());
                            foreach (Tuple<uint, uint> t in cover)
                            {
                                if (t.Item1 == piece.getX() && t.Item2 == piece.getY())
                                {
                                    // Threat
                                    threats.Add(s);
                                }
                            }
                        }
                    }
                }

                // Check each threat for possible new check
                foreach (Square square in threats)
                {
                    Piece p = square.getPiece();

                    int xMod = 0, yMod = 0;

                    if (p.getX() == xKing || p.getY() == yKing)
                    {
                        // Not diagonal
                        if ((int)p.getX() - (int)xKing > 0)
                            xMod = 1;
                        else if ((int)p.getX() - (int)xKing < 0)
                            xMod = -1;

                        if ((int)p.getY() - ((int)yKing) > 0)
                            yMod = 1;
                        else if ((int)p.getY() - ((int)yKing) < 0)
                            yMod = -1;
                    }
                    else
                    {
                        // Diagonal
                        xMod = (xKing > p.getX() ? -1 : 1);
                        yMod = (yKing > p.getY() ? -1 : 1);
                    }

                    int steps = (int)(xMod != 0 ? (Math.Abs(p.getX() - xKing)) : (Math.Abs(p.getY() - yKing)));
                    int x = (int)xKing;
                    int y = (int)yKing;
                    int numBlocks = 0;
                    bool foundSelf = false;
                    bool foundThreat = false;
                    bool oob = false;
                    for (int i = 0; i < steps - 1; i++)
                    {
                        x += xMod;
                        y += yMod;

                        if (x == piece.getX() && y == piece.getY())
                        {
                            foundSelf = true;
                            numBlocks++;
                        }
                        else if (x == square.getX() && y == square.getY())
                        {
                            foundThreat = true;
                        }
                        else
                        {
                            Square sq = board.getSquareAt((uint)x, (uint)y);
                            if (sq == null)
                            {
                                oob = true;
                                break;
                            }
                            if (sq.getPiece() != null)
                            {
                                numBlocks++;
                            }
                        }
                    }

                    // Must have at least two blocks to be a possible move
                    if (oob && !foundThreat)
                    {
                        // OK
                        return;
                    }
                    if (foundSelf && numBlocks < 2)
                    {
                        moves.Clear();
                    }
                }
            }
        }
    }
}
